using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class ExamRoom
    {
        private string _name;
        private int _capacity;
        private double _maxUsage;
        private int _realCapacity;
        private int _currentCapacityUsed = 0;
        private City _city;
        private HashSet<string>? _overseers;
        private ExamRoomNotes _examRoomNotes;
        private bool _regularStudents;
        private bool _chosen;
        public ExamRoom(string name, int capacity, double maxUsage, int realCapacity, City city, HashSet<string>? overseers = null, ExamRoomNotes examRoomNotes = ExamRoomNotes.None, bool otherStudents = true, bool chosen = true)
        {
            _name = name;
            _capacity = capacity;
            _maxUsage = maxUsage;
            _realCapacity = realCapacity;
            _city = city;
            _overseers = overseers;
            _examRoomNotes = examRoomNotes;
            _regularStudents = otherStudents;
            _chosen = chosen;
        }

        public string Name { get => _name; set => _name = value; }
        public int Capacity { get => _capacity; set => _capacity = value; }
        public double MaxUsage { get => _maxUsage; set => _maxUsage = value; }
        public int RealCapacity { get => _realCapacity; set => _realCapacity = value; }
        public City City { get => _city; set => _city = value; }
        public ExamRoomNotes ExamRoomNotes { get => _examRoomNotes; set => _examRoomNotes = value; }
        public bool Chosen { get => _chosen; set => _chosen = value; }
        public HashSet<string>? Overseers { get => _overseers; set => _overseers = value; }
        public bool OtherStudents { get => _regularStudents; set => _regularStudents = value; }
        public int CurrentCapacityUsed { get => _currentCapacityUsed; set => _currentCapacityUsed = value; }

        public void ToggleExamRoomNote(ExamRoomNotes note, bool isChecked)
        {
            if (isChecked)
            {
                ExamRoomNotes |= note;
            }
            else
            {
                ExamRoomNotes &= ~note;
            }
        }

        public bool IsFull(double percentage)
        {
            return (CurrentCapacityUsed / RealCapacity) < percentage;
        }
    }
}
