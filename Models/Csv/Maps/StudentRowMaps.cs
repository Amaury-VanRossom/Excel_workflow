using CsvHelper.Configuration;
using System.Runtime.InteropServices;

namespace excel_workflow.Models.Csv.Maps
{
    public sealed class StudentRowMaps : ClassMap<StudentRow>
    {
        public StudentRowMaps() {
            Map(m => m.Course).Index(0);
            Map(m => m.Course).Index(1);
            Map(m => m.Traject).Index(2);
            Map(m => m.Exemption).Index(4).Convert(r => r.Row.GetField(4) switch
            {
                "Ja" => true,
                _ => false,
            });
            Map(m => m.number).Index(6);
            Map(m => m.studentName).Index(7);
            Map(m => m.classGroup).Index(8);
        } 
    }
}
