using excel_workflow.Models.Enums;

namespace excel_workflow.Models.Csv
{
    public class StudentRow(string course, string educationCode, Traject traject, bool exemption, int number, string studentName, string classGroup)
    {
        public string Course = course;
        public string EducationCode = educationCode;
        public Traject Traject = traject;
        public bool Exemption = exemption;
        public int number = number;
        public string studentName = studentName;
        public string classGroup = classGroup;
    }
}
