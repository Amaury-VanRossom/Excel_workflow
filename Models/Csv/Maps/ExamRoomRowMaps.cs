using CsvHelper.Configuration;

namespace excel_workflow.Models.Csv.Maps
{
    public sealed class ExamRoomRowMaps : ClassMap<ExamRoomRow>
    {
        public ExamRoomRowMaps() {
            Map(r => r.Name).Index(0);
            Map(r => r.Capacity).Index(1);
            Map(r => r.MaxUsage).Index(2).Convert(s => float.Parse((s.Row.GetField(2) ?? "100%")[..^1]) / 100f).Default(1.0,true);
            Map(r => r.RealCapacity).Index(3);
        }
    }
}
