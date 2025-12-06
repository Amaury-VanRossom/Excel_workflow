using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using excel_workflow.Models.Enums;
using System.Reflection;

namespace excel_workflow.Models.Csv.Maps
{
    public class MeasureConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return (Measure) (text switch
            {
                "afzonderlijk examenlokaal" => 0b1,
                "Computer met compenserende software" => 0b10,
                "Digitaal cursusmateriaal" => 0b100,
                "Examen - alternatieve evaluatievorm" => 0b1000,
                "Examen - mondeling overlopen van de antwoorden" => 0b1_0000,
                "Examen - plaats examenlokaal" => 0b10_0000,
                "Extra tijd - mondeling" => 0b100_0000,
                "Extra tijd -schriftelijke evaluatie(15 min)" => 0b1000_0000,
                "Gewettigde afwezigheid - les met evaluatie (speciaal statuut)" => 0b1_0000_0000,
                "Gewettigde afwezigheid(speciaal statuut)" => 0b10_0000_0000,
                "Hulpmiddelen - oordopjes, niet - aangesloten koptelefoon" => 0b100_0000_0000,
                "Leslokaal - plaats" => 0b1000_0000_0000,
                "Lokaal verlaten -statuut" => 0b1_0000_0000_0000,
                "Spelling - taal" => 0b10_0000_0000_0000,
                "Spelling en zinsconstructies(geen taal)" => 0b100_0000_0000_0000,
                "Verplaatsen deadline taken" => 0b1000_0000_0000_0000,
                "Verplaatsen examen - binnen examenperiode" => 0b1_0000_0000_0000_0000,
                "Zinsconstructies(geen taal)" => 0b10_0000_0000_0000_0000,
                _ => 0b0
            });
        }

        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            Measure measure = (Measure) (value ?? 0b0);
            return measure.ToString(); //TODO: betere implementatie van Measure.ToString() maken (extension method??)
        }
    }
}
