using excel_workflow.Models;
using excel_workflow.Models.Csv;
using excel_workflow.Models.Enums;

namespace excel_workflow.Extensions
{
    public static class StudentEnumerableExtensions
    {
        public static IEnumerable<(Student, Olod)> GetStudentsFromCity(this IEnumerable<(Student,Olod)> students, City city)
        {
            return students
                .Where(t => {
                    Olod olod = t.Item2;
                    return olod is not null && !olod.Exemption && (city == City.Aalst 
                    ? (olod.Traject.Equals(Traject.Aalst) || (olod.VCCity?.Equals(City.Aalst) ?? false)) 
                    : (olod.Traject.Equals(Traject.TI) || (olod.VCCity?.Equals(City.Gent) ?? false)));
                });
        }

        public static IEnumerable<(Student, Olod)> GetStudentsWithOlod(this IEnumerable<Student> students, string olodName)
        {
            var values = students.Select(s => (s, s.Olods.FirstOrDefault(o => o.Name.Equals(olodName))))
                .Where(t => t.Item2 is not null && !t.Item2.Exemption).Cast<(Student, Olod)>();
            return values;
        }

        public static IEnumerable<(Student, Olod)> GetTIAOVCStudents(this IEnumerable<(Student, Olod)> students)
        {
            return students.Where(t => t.Item2.Traject.Equals(Traject.TIAO) || t.Item2.Traject.Equals(Traject.VC));
        }

        public static IEnumerable<Student> DiscardOlods(this IEnumerable<(Student, Olod)> students)
        {
            return students.Select(t => t.Item1);
        }

        public static IEnumerable<SignatureListRow> GetSignatureListRows(this IEnumerable<(Student, Olod, ExamRoom)> students, ExamType? examType) {
            return students.Select(s =>
                new SignatureListRow(s.Item1.Name, s.Item2.Subgroup, s.Item1.isIOEM(), s.Item1.HasExtraTime((ExamType)examType!), s.Item3.Name));
        }
        public static IEnumerable<(Student, Olod, ExamRoom)> GetStudentInfoFromCity(this Dictionary<int, Student> studentDict, Dictionary<int, ExamRoom> examRoomDict, City city, Func<IEnumerable<(Student, Olod, ExamRoom)>, IEnumerable<(Student, Olod, ExamRoom)>> sortingFunc, string olod)
        {
            var assignedRooms = examRoomDict.Where(t => t.Value.City.Equals(city)).Select(t => (studentDict[t.Key], t.Value));
            return sortingFunc(assignedRooms.Select(t => t.Item1)
                .GetStudentsWithOlod(olod)
                .Zip(assignedRooms.Select(t => t.Item2), (q, e) => (q.Item1, q.Item2, e)));
        }
    }
}
