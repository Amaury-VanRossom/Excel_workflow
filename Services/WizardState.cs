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
            foreach (var student in WizardModel.Students.Values)
            {
                WizardModel.AssignedStudents.Remove(student);
            }

            // Get rooms for this city
            var cityRooms = WizardModel.Rooms
                .Where(r => r.Chosen && r.City == city);

            if (!cityRooms.Any())
            {
                // No rooms available
                throw new InvalidOperationException($"There are no rooms available for {city}");
            }

            var students = WizardModel.GetStudentsFromCity(city);

            // 1. Assign special students first


            var SBRoom = WizardModel.Rooms.First(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.SB));
            foreach (var student in students.Where(WizardModel.NeedSeperateRoom))
            {
                if (SBRoom is not null)
                {
                    student.AssignedRoom = SBRoom;
                    SBRoom.CurrentCapacityUsed++;
                }
            }

            var specialRooms = cityRooms.Where(r => r.ExamRoomNotes.HasFlag(ExamRoomNotes.IOEM));
            int amountOfIOEMStudentsThatDidNotGetRoom = 0;
            foreach (var student in students.Where(WizardModel.NeedSeperateRoom))
            {
                var specialRoom = specialRooms.First(r => r.CurrentCapacityUsed < r.RealCapacity);
                if (specialRoom != null)
                {
                    student.AssignedRoom = specialRoom;
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

            // 2. Assign remaining students
            foreach (var student in WizardModel.Students.Values.Where(s => s.AssignedRoom == null))
            {
                var room = cityRooms.First(r => r.CurrentCapacityUsed/r.RealCapacity < percentage) ?? throw new InvalidOperationException("All rooms are full, increase the limit or assign more rooms");
                student.AssignedRoom = room;
                room.CurrentCapacityUsed++;
            }
        }

    }
}
