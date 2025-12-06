using CsvHelper.Configuration;
using System.Runtime.InteropServices;

namespace excel_workflow.Models.Csv.Maps
{
    public sealed class StudentRowMaps : ClassMap<StudentRow>
    {
        public StudentRowMaps() {
            Map(m => m.Course).Index(0);
            Map(m => m.Traject).Index(2);
            Map(m => m.Exemption).Index(4).Convert(r => r.Row.GetField(4) switch
            {
                "Ja" => true,
                _ => false,
            });
            Map(m => m.Number).Index(6);
            Map(m => m.StudentName).Index(7);
            Map(m => m.ClassGroup).Index(8);
        } 
    }
}
