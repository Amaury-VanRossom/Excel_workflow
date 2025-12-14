using CsvHelper;
using CsvHelper.Configuration;
using excel_workflow.Models;
using excel_workflow.Models.Csv;
using excel_workflow.Models.Csv.Maps;
using excel_workflow.Models.Enums;
using System.Globalization;
using System.IO.Compression;
using System.Runtime.InteropServices.Marshalling;

namespace excel_workflow.Services
{
    public class CsvParsingService
    {
        private CsvReader GetCsvReader<T>(Stream csvStream) where T : ClassMap
        {
            var reader = new StreamReader(csvStream);
            var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            csv.Context.RegisterClassMap<T>();
            return csv;
        }

        private CsvWriter GetCsvWriter<T>(Stream stream) where T : ClassMap
        {
            var writer = new StreamWriter(stream, leaveOpen: true);
            var csv = new CsvWriter(writer, CultureInfo.CurrentCulture);
            csv.Context.RegisterClassMap<T>();
            return csv;
        }

        public byte[] GenerateSignatureList(IEnumerable<SignatureListRow> entries)
        {
            using var memory = new MemoryStream();
            using var csv = GetCsvWriter<SignatureListRowMap>(memory);
            csv.WriteRecords(entries);
            csv.Flush();

            return memory.ToArray();
        }
        public byte[] GenerateSignatureZip(IEnumerable<SignatureListRow> entries)
        {
            using var memory = new MemoryStream();
            using (var zip = new ZipArchive(memory, ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach (var group in entries.GroupBy(e => e.Lokaal))
                {
                    var csvBytes = GenerateSignatureList(group);

                    var entry = zip.CreateEntry($"{group.Key}.csv");
                    using var entryStream = entry.Open();
                    entryStream.Write(csvBytes, 0, csvBytes.Length);
                }
            }

            return memory.ToArray();
        }

        public async IAsyncEnumerable<Student> ParseStudents(Stream csvStream)
        {
            using var csv = GetCsvReader<StudentRowMaps>(csvStream);

            await foreach (var group in csv.GetRecordsAsync<StudentRow>()
                .GroupBy(r => new { r.Number, r.StudentName }))
            {
                yield return new Student(
                    group.Key.StudentName,
                    group.Key.Number,
                    group.Select(r => new Olod(r.Course, r.Exemption, r.Traject, r.ClassGroup)).ToHashSet()
                );
            }
        }


        public async IAsyncEnumerable<(int, Measure)> ParseIOEM(Stream csvStream)
        {
            using var csv = GetCsvReader<IOEMRowMaps>(csvStream);

            await foreach (var group in csv.GetRecordsAsync<IOEMRow>()
                .AggregateBy(r => r.Number, Measure.None, (acc, r) => acc | r.Measure))
            {
                yield return (group.Key, group.Value);
            };
        }

        public async IAsyncEnumerable<ExamRoom> ParseExamRooms(Stream csvStream)
        {
            using var csv = GetCsvReader<ExamRoomRowMaps>(csvStream);

            await foreach (var group in csv.GetRecordsAsync<ExamRoomRow>())
            {
                City city = group.Name.StartsWith("GAARB",true, null) ? City.Aalst : City.Gent;
                yield return new ExamRoom(group.Name, group.Capacity, group.MaxUsage, group.RealCapacity, city);
            }
        
        }
    }
}
