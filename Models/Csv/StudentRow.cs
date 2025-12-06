using excel_workflow.Models.Enums;

namespace excel_workflow.Models.Csv
{
    public class StudentRow
    {
        public string Course;
        public Traject Traject;
        public bool Exemption;
        public int Number;
        public string StudentName;
        public string ClassGroup;

        public StudentRow(string course, Traject traject, bool exemption, int number, string studentName, string classGroup)
        {
            Course = course;
            Traject = traject;
            Exemption = exemption;
            Number = number;
            StudentName = studentName;
            ClassGroup = classGroup;
        }
        public StudentRow()
        {

        }
    }
}
