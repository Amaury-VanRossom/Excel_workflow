namespace excel_workflow.Models.Csv
{
    public class SignatureListRow
    {
        public string Naam { get; set; }
        public string Groep { get; set; }
        public string IOEM { get; set; }
        public string ExtraTijd { get; set; }
        public string? Handtekening { get; set; }
        public string? UurIngediend { get; set; }
        public string Lokaal { get; set; }

        public SignatureListRow() { }

        public SignatureListRow(string naam, string groep, bool iOEM, bool extraTijd, string lokaal)
        {
            Naam = naam;
            Groep = groep;
            IOEM = iOEM ? "Ja" : "Nee";
            ExtraTijd = extraTijd ? "Ja" : "Nee";
            Lokaal = lokaal;
        }
    }

}
