using excel_workflow.Models.Enums;
using System.Runtime.CompilerServices;

namespace excel_workflow.Models
{
    public class Student
    {
        private string _name;
        private string _educationCode;
        private int _studentNumber;
        private Traject _traject;
        private HashSet<Olod> _olods;
        private Measure _measures;

        public Student(string name, string educationCode, int studentNumber, Traject traject, HashSet<Olod>? olods)
        {
            _name = name;
            _educationCode = educationCode;
            _studentNumber = studentNumber;
            _traject = traject;
            _olods = olods ?? new HashSet<Olod>();
            _measures = 0b0;
        }

        public string Name { get => _name; set => _name = value; }
        public int StudentNumber { get => _studentNumber; set => _studentNumber = value; }
        public string EducationCode { get => _educationCode; set => _educationCode = value; }
        public Traject Traject { get => _traject; set => _traject = value; }
        public HashSet<Olod> Olods { get => _olods; set => _olods = value; }
        public Measure Measures { get => _measures; set => _measures = value; }

        public void AddMeasure(int i)
        {
            Measures |= (Measure) i;
        }
        public void AddMeasure(Measure measure)
        {
            Measures |= measure;
        }
    }
}
