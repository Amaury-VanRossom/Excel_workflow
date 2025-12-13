using excel_workflow.Models.Enums;
using System.Runtime.CompilerServices;

namespace excel_workflow.Models
{
    public class Student
    {
        private string _name;
        private int _studentNumber;
        private HashSet<Olod> _olods;
        private Measure _measures;

        public Student(string name, int studentNumber, HashSet<Olod>? olods)
        {
            _name = name;
            _studentNumber = studentNumber;
            _olods = olods ?? new HashSet<Olod>();
            _measures = 0b0;
        }

        public string Name { get => _name; set => _name = value; }
        public int StudentNumber { get => _studentNumber; set => _studentNumber = value; }
        public HashSet<Olod> Olods { get => _olods; set => _olods = value; }
        public Measure Measures { get => _measures; set => _measures = value; }

        public bool isIOEM()
        {
            return Measures != 0b0;
        }

        public bool HasExtraTime(ExamType examType)
        {
            return examType.Equals(ExamType.WRITTEN) && Measures.HasFlag(Measure.TimeWritten);
        }
    }
}
