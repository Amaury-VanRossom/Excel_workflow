using excel_workflow.Models.Enums;
using System.Security.Cryptography.X509Certificates;

namespace excel_workflow.Models.Csv
{
    public class IOEMRow
    {
        public string Name;
        public int Number;
        public Measure Measure;

        public IOEMRow(string name, int number, Measure measure)
        {
            Name = name;
            Number = number;
            Measure = measure;
        }

        public IOEMRow() 
        {
            
        }
    }
}
