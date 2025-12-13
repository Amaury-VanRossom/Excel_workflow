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

        public void AssignStudents(City city, double percentage)
        {
            string message = string.Empty;
            // studenten vinden die vak volgen in city: var students = WizardModel.Students.Values.Where(s => s.Olods.(o => o.Name.Equals(WizardModel.Olod)));
            // Reset assignments
            var students = WizardModel.Students.Values.GetStudentsWithOlod(WizardModel.Olod!).GetStudentsFromCity(city);
            foreach (var (s,_) in students)
            {
                WizardModel.AssignedStudents.Remove(s);
            }

            // Get rooms for this city
            var cityRooms = WizardModel.Rooms
                .Where(r => r.Chosen && r.City == city);

            if (!cityRooms.Any())
            {
                // No rooms available
                throw new InvalidOperationException($"There are no rooms available for {city}");
            }

            var SBRoom = WizardModel.Rooms.FirstOrDefault(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.SB));
            foreach (var student in students.DiscardOlods().Where(s => WizardModel.NeedRoom(s, MeasureTaken.SB)))
            {
                if (SBRoom is not null)
                {
                    WizardModel.AssignedStudents.Add(student, SBRoom);
                    SBRoom.CurrentCapacityUsed++;
                }
            }

            //IOEM seperated students
            var specialRooms = cityRooms.Where(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.IOEM));
            int amountOfIOEMStudentsThatDidNotGetRoom = 0;
            foreach (var student in students.DiscardOlods().Where(s => WizardModel.NeedRoom(s, MeasureTaken.SeperateRoom)))
            {
                var specialRoom = specialRooms.FirstOrDefault(r => !r.IsFull(percentage));
                if (specialRoom is not null)
                {
                    WizardModel.AssignedStudents.Add(student, specialRoom);
                    specialRoom.CurrentCapacityUsed++;
                }
                else
                {
                    amountOfIOEMStudentsThatDidNotGetRoom++;
                }

            }
            if (amountOfIOEMStudentsThatDidNotGetRoom > 0)
            {
                throw new InvalidOperationException($"{amountOfIOEMStudentsThatDidNotGetRoom} IOEM students did not get assigned a room because all seperate rooms are full.");
            }

            //TIAO/VC students
            if (WizardModel.DistanceLearningClassroomType.Equals(DistanceLearningClassroomType.Seperated))
            {
                int amountOfTIAOStudentsThatDidNotGetRoom = 0;
                var seperatedRooms = cityRooms.Where(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.TIAOVC));
                foreach (var student in students.GetTIAOVCStudents().DiscardOlods())
                {
                    var seperatedRoom = seperatedRooms.FirstOrDefault(r => !r.IsFull(percentage));
                    if (seperatedRoom is not null)
                    {
                        WizardModel.AssignedStudents.Add(student, seperatedRoom);
                    }
                    else
                    {
                        amountOfTIAOStudentsThatDidNotGetRoom++;
                    }
                }

                if (amountOfTIAOStudentsThatDidNotGetRoom > 0)
                {
                    throw new InvalidOperationException($"{amountOfTIAOStudentsThatDidNotGetRoom} TIAO students did not get assigned a room because all seperate rooms are full.");
                }
            }

            // 2. Assign remaining students
            foreach (var student in students.DiscardOlods())
            {
                if (!WizardModel.AssignedStudents.TryGetValue(student, out var examRoom) || examRoom is null)
                {
                    var room = cityRooms.First(r => !r.IsFull(percentage)) ?? throw new InvalidOperationException("All rooms are full, increase the limit or assign more rooms");
                    WizardModel.AssignedStudents.Add(student, room);
                    room.CurrentCapacityUsed++;
                }
            }
        }

    }
}
