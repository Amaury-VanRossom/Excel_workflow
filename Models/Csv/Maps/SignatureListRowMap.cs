using CsvHelper.Configuration;

namespace excel_workflow.Models.Csv.Maps
{
    public sealed class SignatureListRowMap : ClassMap<SignatureListRow>
    {
        public SignatureListRowMap()
        {
            Map(m => m.Naam).Index(0).Name("Naam");
            Map(m => m.Groep).Index(1).Name("Groep");
            Map(m => m.IOEM).Index(2).Name("IOEM?");
            Map(m => m.ExtraTijd).Index(3).Name("Extra tijd?");
            Map(m => m.Handtekening).Index(4).Name("Handtekening");
            Map(m => m.UurIngediend).Index(5).Name("Uur ingediend");
        }
    }
}
