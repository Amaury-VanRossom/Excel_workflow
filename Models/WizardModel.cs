using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class WizardModel
    {
        private string? _olod;
        private ExamType? _examType;
        private DistanceLearningClassroomType? _distanceLearningClassroomType;
        //keys komen uit IOEM
        private Dictionary<Measure, MeasureTaken> _measuresTaken;
        private Dictionary<int, Student> _students;
        private List<ExamRoom> _rooms;
        //lokaal kiezen voor Gent en Aalst voor "ander te selecteren lokaal" en TIAO/VC
        private RoomChoices? _roomChoices;
        private Dictionary<Student, ExamRoom> _assignedStudents;
        public string? Olod { get => _olod; set => _olod = value; }
        public ExamType? ExamType { get => _examType; set => _examType = value; }
        public DistanceLearningClassroomType? DistanceLearningClassroomType { get => _distanceLearningClassroomType; set => _distanceLearningClassroomType = value; }
        public Dictionary<Measure, MeasureTaken> MeasuresTaken { get => _measuresTaken; set => _measuresTaken = value; }
        public Dictionary<int, Student> Students { get => _students; set => _students = value; }
        public List<ExamRoom> Rooms { get => _rooms; set => _rooms = value; }
        public RoomChoices? RoomChoices { get => _roomChoices; set => _roomChoices = value; }
        public Dictionary<Student, ExamRoom> AssignedStudents { get => _assignedStudents; set => _assignedStudents = value; }

        public WizardModel()
        {
            _measuresTaken = new Dictionary<Measure, MeasureTaken>();
            _students = new Dictionary<int, Student>();
            _rooms = new List<ExamRoom>();
            _assignedStudents = new Dictionary<Student, ExamRoom>();
        }

        public IEnumerable<Student> GetStudentsFromCity(City city)
        {
            return Students.Values
                .Where(s => {
                    Olod? olod = s.Olods.FirstOrDefault(o => o.Name == Olod);
                    return olod is not null && !olod.Exemption && (city == City.Aalst 
                    ? (olod.Traject.Equals(Traject.Aalst) || (olod.VCCity?.Equals(City.Aalst) ?? false)) 
                    : (!olod.Traject.Equals(Traject.Aalst)) || (olod.VCCity?.Equals(City.Gent) ?? false));
                });

        }

        public IEnumerable<(Student, Olod)> GetStudentsWithOlod(string olodName)
        {
            Console.WriteLine(olodName);
            var values = Students.Values.Select(s => (s, s.Olods.FirstOrDefault(o => o.Name.Equals(olodName))))
                .Where(t => t.Item2 is not null).Cast<(Student, Olod)>();
            Console.WriteLine(values.Count());
            return values;
        }

        public bool NeedRoom(Student student, MeasureTaken measureTaken)
        {
            foreach (var measure in Enum.GetValues<Measure>())
            {
                if (!measure.Equals(Measure.None) && student.Measures.HasFlag(measure) && MeasuresTaken[measure].Equals(measureTaken))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
