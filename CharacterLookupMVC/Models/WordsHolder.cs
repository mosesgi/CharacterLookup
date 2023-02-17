using CharacterLookupMVC.Models;

namespace CharacterLookup.Models
{
    public static class WordsHolder
    {
        public static Dictionary<char, Word> Words { get; set; }

        static WordsHolder()
        {
            Words = new Dictionary<char, Word>();
            Initialize();
        }

        static void Initialize()
        {
            string[] lines = System.IO.File.ReadAllLines(@"primaryschool.txt");

            Grades currGrade = Grades.One;
            Semister currSemister = Semister.First;
            string articlePrefix = string.Empty;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.Contains("年级") && line.EndsWith("册"))
                {
                    (currGrade, currSemister) = ExtractGrade(line);
                }
                else if (line == "识字" || line == "课文" || line == "园地")
                {
                    articlePrefix = line;
                }
                else
                {
                    int charStartPos = 0;
                    string article = articlePrefix;
                    while (char.IsDigit(line[charStartPos]))
                    {
                        article += line[charStartPos++];
                    }
                    for (int i = charStartPos; i < line.Length; i++)
                    {
                        Word word = new Word()
                        {
                            Article = article,
                            Grade = currGrade,
                            Semister = currSemister,
                            Character = line[i]
                        };
                        if (Words.ContainsKey(line[i]))
                        {
                            Console.WriteLine($"Something is wrong here: {word}");
                            continue;
                        }
                        Words[line[i]] = word;
                    }
                }
            }
        }

        static (Grades, Semister) ExtractGrade(string line)
        {
            var gradeInChinese = line.Substring(0, 1);
            var semisterInChinese = line.Substring(line.Length - 2, 1);
            Grades grade;
            Semister semister;
            switch (gradeInChinese)
            {
                case "一":
                    grade = Grades.One;
                    break;
                case "二":
                    grade = Grades.Two;
                    break;
                case "三":
                    grade = Grades.Three;
                    break;
                case "四":
                    grade = Grades.Four;
                    break;
                case "五":
                    grade = Grades.Five;
                    break;
                case "六":
                    grade = Grades.Six;
                    break;
                default:
                    throw new ArgumentException("Invalid grade!");
            }
            if (semisterInChinese == "上")
            {
                semister = Semister.First;
            }
            else
            {
                semister = Semister.Second;
            }
            return (grade, semister);
        }
    }
}
