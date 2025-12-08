using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class ExamRoom
    {
        private string _name;
        private int _capacity;
        private double _maxUsage;
        private int _realCapacity;
        private City _city;
        private HashSet<string>? _overseers;
        private ExamRoomNotes _examRoomNotes;
        private bool _chosen;
        public ExamRoom(string name, int capacity, double maxUsage, int realCapacity, City city, HashSet<string>? overseers = null, ExamRoomNotes examRoomNotes = ExamRoomNotes.None, bool chosen = true)
        {
            _name = name;
            _capacity = capacity;
            _maxUsage = maxUsage;
            _realCapacity = realCapacity;
            _city = city;
            _overseers = overseers;
            _examRoomNotes = examRoomNotes;
            _chosen = chosen;
        }

        public string Name { get => _name; set => _name = value; }
        public int Capacity { get => _capacity; set => _capacity = value; }
        public double MaxUsage { get => _maxUsage; set => _maxUsage = value; }
        public int RealCapacity { get => _realCapacity; set => _realCapacity = value; }
        public City City { get => _city; set => _city = value; }
        public ExamRoomNotes ExamRoomNotes { get => _examRoomNotes; set => _examRoomNotes = value; }
        public bool Chosen { get => _chosen; set => _chosen = value; }
    }
}
