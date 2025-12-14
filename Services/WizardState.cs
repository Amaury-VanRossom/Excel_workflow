using excel_workflow.Exceptions;
using excel_workflow.Extensions;
using excel_workflow.Models;
using excel_workflow.Models.Enums;

namespace excel_workflow.Services
{
    public class WizardState
    {
        private WizardModel _wizardModel;
        private int _step;
        private bool _isHydrated;
        public WizardState()
        {
            _wizardModel = new WizardModel();
            _step = 1;
            _isHydrated = false;
        }

        public WizardModel WizardModel { get => _wizardModel; set => _wizardModel = value; }
        public int Step { get => _step; set => _step = value; }
        public bool IsHydrated { get => _isHydrated; set => _isHydrated = value; }

        public void ToggleStepDone()
        {
            Step++;
        }

        public int CanAccessStep(int step)
        {
            Console.WriteLine($"Diff is {step} - {Step}");
            return step - Step;
        }

        public void AssignStudents(City city, bool sortByStudentName, double percentage)
        {
            string message = string.Empty;
            var students = WizardModel.Students.Values.GetStudentsWithOlod(WizardModel.Olod!).GetStudentsFromCity(city).OrderBy(s => sortByStudentName ? s.Item1.Name : s.Item2.Subgroup).ToList();
            foreach (var (s,_) in students)
            {
                WizardModel.AssignedStudents.Remove(s.StudentNumber);
            }

            var cityRooms = WizardModel.Rooms
                .Where(r => r.Chosen && r.City == city && !r.Name.Equals("SB+")).OrderBy(r => r.Name).ToList();
            Console.WriteLine(cityRooms.Count);

            if (cityRooms.Count == 0)
            {
                throw new ExamRoomAssignmentException($"Er zijn geen beschikbare lokalen in {city}. Selecteer meer lokalen.");
            }

            var SBRoom = WizardModel.Rooms.FirstOrDefault(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.SB));
            foreach (var student in students.DiscardOlods().Where(s => WizardModel.NeedRoom(s, MeasureTaken.SB)))
            {
                if (SBRoom is not null)
                {
                    WizardModel.AssignedStudents.Add(student.StudentNumber, SBRoom);
                }
            }

            var specialRooms = cityRooms.Where(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.IOEM));
            int amountOfIOEMStudentsThatDidNotGetRoom = 0;
            foreach (var student in students.DiscardOlods().Where(s => !WizardModel.AssignedStudents.ContainsKey(s.StudentNumber) && WizardModel.NeedRoom(s, MeasureTaken.SeperateRoom)))
            {
                var specialRoom = specialRooms.FirstOrDefault(r => !r.IsFull(WizardModel.GetUsedCapacity(r), percentage));
                if (specialRoom is not null)
                {
                    WizardModel.AssignedStudents.Add(student.StudentNumber, specialRoom);
                }
                else
                {
                    amountOfIOEMStudentsThatDidNotGetRoom++;
                }

            }
            if (amountOfIOEMStudentsThatDidNotGetRoom > 0)
            {
                throw new ExamRoomAssignmentException($"{amountOfIOEMStudentsThatDidNotGetRoom} IOEM studenten hebben geen lokaal toegewezen gekregen omdat alle IOEM lokalen vol zitten.");
            }

            if (WizardModel.DistanceLearningClassroomType.Equals(DistanceLearningClassroomType.Apart))
            {
                int amountOfTIAOStudentsThatDidNotGetRoom = 0;

                var seperatedRooms = cityRooms.Where(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.TIAOVC));
                foreach (var student in students.GetTIAOVCStudents().DiscardOlods().Where(s => !WizardModel.AssignedStudents.ContainsKey(s.StudentNumber)))
                {
                    var seperatedRoom = seperatedRooms.FirstOrDefault(r => !r.IsFull(WizardModel.GetUsedCapacity(r), percentage));
                    if (seperatedRoom is not null)
                    {
                        WizardModel.AssignedStudents.Add(student.StudentNumber, seperatedRoom);
                    }
                    else
                    {
                        amountOfTIAOStudentsThatDidNotGetRoom++;
                    }
                }

                if (amountOfTIAOStudentsThatDidNotGetRoom > 0)
                {
                    throw new ExamRoomAssignmentException($"{amountOfTIAOStudentsThatDidNotGetRoom} TIAO/VC studenten hebben geen lokaal toegewezen gekregen omdat alle aparte lokalen vol zitten. Verhoog het limiet of selecteer meer lokalen");
                }
            }

            foreach (var student in students.DiscardOlods().Where(s => !WizardModel.AssignedStudents.ContainsKey(s.StudentNumber)))
            {
                if (!WizardModel.AssignedStudents.TryGetValue(student.StudentNumber, out var examRoom) || examRoom is null)
                {
                    var room = cityRooms.FirstOrDefault(r => !r.IsFull(WizardModel.GetUsedCapacity(r), percentage) && r.OtherStudents) ?? throw new ExamRoomAssignmentException("Alle lokalen zijn vol, verhoog het limiet of selecteer meer lokalen.");
                    WizardModel.AssignedStudents.Add(student.StudentNumber, room);
                }
            }
        }

    }
}
