namespace CharacterLookupMVC.Models
{
    public class Word
    {
        public char Character { get; set; }
        public Grades Grade { get; set; }
        public Semister Semister { get; set; }
        public string? Article { get; set; }

        public string GradeStr { get => EnumsConverter.GradeToString(Grade); }
        public string SemisterStr { get => EnumsConverter.SemisterToString(Semister); }

        public override string? ToString()
        {
            return $"{Character} 学于 {Grade} {Semister} {Article}";
        }
    }

    public enum Grades
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    public enum Semister
    {
        First,
        Second
    }

    public static class EnumsConverter
    {
        static Dictionary<Grades, string> gradeDict = new Dictionary<Grades, string>()
        {
            [Grades.One] = "一年级",
            [Grades.Two] = "二年级",
            [Grades.Three] = "三年级",
            [Grades.Four] = "四年级",
            [Grades.Five] = "五年级",
            [Grades.Six] = "六年级"
        };

        public static string GradeToString(Grades grade)
        {
            return gradeDict[grade];
        }

        public static string SemisterToString(Semister semister)
        {
            if (semister == Semister.First)
            {
                return "上学期";
            }
            else
            {
                return "下学期";
            }
        }
    }
}
