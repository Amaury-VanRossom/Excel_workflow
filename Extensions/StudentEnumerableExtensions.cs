using excel_workflow.Models;
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
                    : (olod.Traject.Equals(Traject.TI) || olod.Traject.Equals(Traject.TIAO)) || (olod.VCCity?.Equals(City.Gent) ?? false));
                });
        }

        public static IEnumerable<(Student, Olod)> GetStudentsWithOlod(this IEnumerable<Student> students, string olodName)
        {
            Console.WriteLine(olodName);
            var values = students.Select(s => (s, s.Olods.FirstOrDefault(o => o.Name.Equals(olodName))))
                .Where(t => t.Item2 is not null).Cast<(Student, Olod)>();
            Console.WriteLine(values.Count());
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

        public static IEnumerable<Student> HaveNoAssignedRoom(this IEnumerable<Student> students)
        {
            return students.Where(s => s.AssignedRoom is null);
        }

    }
}
