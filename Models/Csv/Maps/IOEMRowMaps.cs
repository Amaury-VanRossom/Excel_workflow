using CsvHelper.Configuration;

namespace excel_workflow.Models.Csv.Maps
{
    public sealed class IOEMRowMaps : ClassMap<IOEMRow>
    {
        public IOEMRowMaps() {
            Map(m => m.Name).Index(0);
            Map(m => m.Number).Index(1);
            Map(m => m.Measure).Index(2).TypeConverter<MeasureConverter>();
        }
    }
}
