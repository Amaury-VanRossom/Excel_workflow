using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class RoomChoices
    {
        private string _SB;
        private string _gentSelectedRoom;
        private string _aalstSelectedRoom;
        private string _gentTIAOVC;

        public RoomChoices(ExamType examType, string gentSelectedRoom, string aalstSelectedRoom, string gentTIAOVC)
        {
            _gentSelectedRoom = gentSelectedRoom;
            _aalstSelectedRoom = aalstSelectedRoom;
            _gentTIAOVC = gentTIAOVC;
            _SB = examType == ExamType.WRITTEN ? "apart SB+" : "NVT";
        }

        public string SB { get => _SB; set => _SB = value; }
        public string GentSelectedRoom { get => _gentSelectedRoom; set => _gentSelectedRoom = value; }
        public string AalstSelectedRoom { get => _aalstSelectedRoom; set => _aalstSelectedRoom = value; }
        public string GentTIAOVC { get => _gentTIAOVC; set => _gentTIAOVC = value; }
    }
}
