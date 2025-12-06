using excel_workflow.Models.Enums;
using System.Security.Cryptography.X509Certificates;

namespace excel_workflow.Models.Csv
{
    public class IOEMRow(string name, int number, Measure measure)
    {
        public string Name = name;
        public int Number = number;
        public Measure Measure = measure;
    }
}
