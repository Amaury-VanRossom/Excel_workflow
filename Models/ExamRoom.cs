using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class ExamRoom
    {
        private string _name;
        private int _capacity;
        private float _maxUsage;
        private int _realCapacity;
        private City _city;
        private HashSet<string>? _overseers;
        private ExamRoomNotes _examRoomNotes;
        private bool _chosen;
        public ExamRoom(string name, int capacity, float maxUsage, int realCapacity, City city, HashSet<string>? overseers = null, ExamRoomNotes examRoomNotes = ExamRoomNotes.None, bool chosen = true)
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
    }
}
