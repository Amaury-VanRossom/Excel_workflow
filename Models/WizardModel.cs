using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class WizardModel
    {
        private string? _olod;
        private ExamType? _examType;
        private DistanceLearningClassroomType? _distanceLearningClassroomType;
        //keys komen uit IOEM
        private Dictionary<Measure, MeasureTaken?> _measuresTaken;
        private Dictionary<int, Student> _students;
        private List<ExamRoom> _rooms;
        //lokaal kiezen voor Gent en Aalst voor "ander te selecteren lokaal" en TIAO/VC
        private RoomChoices? _roomChoices;
        public string? Olod { get => _olod; set => _olod = value; }
        public WizardModel()
        {
            _measuresTaken = new Dictionary<Measure, MeasureTaken?>();
            _students = new Dictionary<int, Student>();
            _rooms = new List<ExamRoom>();
        }

    }
}
