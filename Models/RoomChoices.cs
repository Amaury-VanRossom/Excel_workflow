using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class RoomChoices
    {
        private string _SB;
        private ExamRoom? _gentSelectedRoom;
        private ExamRoom? _aalstSelectedRoom;
        private ExamRoom? _gentTIAOVC;

        public RoomChoices(ExamType examType, ExamRoom? gentSelectedRoom = null, ExamRoom? aalstSelectedRoom = null, ExamRoom? gentTIAOVC = null )
        {
            _gentSelectedRoom = gentSelectedRoom;
            _aalstSelectedRoom = aalstSelectedRoom;
            _gentTIAOVC = gentTIAOVC;
            _SB = examType == ExamType.WRITTEN ? "apart SB+" : "NVT";
        }

        public RoomChoices() : this(ExamType.WRITTEN) { }

        public string SB { get => _SB; set => _SB = value; }
        public ExamRoom? GentSelectedRoom { get => _gentSelectedRoom; set => _gentSelectedRoom = value; }
        public ExamRoom? AalstSelectedRoom { get => _aalstSelectedRoom; set => _aalstSelectedRoom = value; }
        public ExamRoom? GentTIAOVC { get => _gentTIAOVC; set => _gentTIAOVC = value; }
    }
}
