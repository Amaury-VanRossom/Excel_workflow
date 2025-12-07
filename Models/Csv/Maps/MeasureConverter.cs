using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using excel_workflow.Models.Enums;
using System.Reflection;

namespace excel_workflow.Models.Csv.Maps
{
    public class MeasureConverter : DefaultTypeConverter
    {
        private static readonly Dictionary<string, Measure> _map = new()
        {
            ["Afzonderlijk examenlokaal"] = Measure.SeperateRoom,
            ["Computer met compenserende software"] = Measure.Computer,
            ["Digitaal cursusmateriaal"] = Measure.DigitalCursus,
            ["Examen - alternatieve evaluatievorm"] = Measure.ExAlternateEvaluation,
            ["Examen - mondeling overlopen van de antwoorden"] = Measure.ExOralReview,
            ["Examen - plaats examenlokaal"] = Measure.ExSpecificSeat,
            ["Extra tijd - mondeling"] = Measure.TimeOral,
            ["Extra tijd - schriftelijke evaluatie (15 min)"] = Measure.TimeWritten,
            ["Gewettigde afwezigheid - les met evaluatie (speciaal statuut)"] = Measure.LawfulAbscenceEvaluation,
            ["Gewettigde afwezigheid (speciaal statuut)"] = Measure.LawfulAbscence,
            ["Hulpmiddelen - oordopjes, niet-aangesloten koptelefoon"] = Measure.Deafening,
            ["Leslokaal - plaats"] = Measure.LessonSpecificSeat,
            ["Lokaal verlaten - statuut"] = Measure.LeaveClass,
            ["Spelling - taal"] = Measure.SpellingLanguage,
            ["Spelling en zinsconstructies (geen taal)"] = Measure.SpellingSentence,
            ["Verplaatsen deadline taken"] = Measure.MoveDeadline,
            ["Verplaatsen examen - binnen examenperiode"] = Measure.MoveExam,
            ["Zinsconstructies (geen taal)"] = Measure.Sentence
        };

        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Measure.None;

            if (_map.TryGetValue(text, out var measure))
                return measure;

            throw new CsvHelper.TypeConversion.TypeConverterException(
                this, memberMapData, text, row.Context,
                $"Unknown measure keyword '{text}'");
        }

        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            var measure = (Measure)(value ?? Measure.None);

            // If multiple flags are set, join their keywords
            var keywords = _map
                .Where(kvp => measure.HasFlag(kvp.Value))
                .Select(kvp => kvp.Key);

            return string.Join(";", keywords);
        }
    }
}
