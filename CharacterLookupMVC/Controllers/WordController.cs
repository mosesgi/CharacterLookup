using CharacterLookupMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharacterLookupMVC.Controllers
{
    public class WordController : Controller
    {
        static Dictionary<char, Word> words = new Dictionary<char, Word>();

        public WordController()
        {
            Initialize();
        }

        // GET: WordController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WordController/Details/5
        public ActionResult Details(string id)
        {
            id = id.Trim();
            if (string.IsNullOrWhiteSpace(id) || id.Length != 1)
            {
                return View();
            }

            if (!words.ContainsKey(id[0]))
            {
                return View();
            }

            var word = words[id[0]];

            return View(word);
        }

        void Initialize()
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
                        if (words.ContainsKey(line[i]))
                        {
                            Console.WriteLine($"Something is wrong here: {word}");
                            continue;
                        }
                        words[line[i]] = word;
                    }
                }
            }
        }

        (Grades, Semister) ExtractGrade(string line)
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


        public ActionResult BaiduLink([Bind(Prefix = "id")] string id)
        {
            var escaped = Uri.EscapeDataString(id);
            return Redirect(string.Concat("https://", $"hanyu.baidu.com/zici/s?wd={escaped}&query={escaped}"));
        }


        // GET: WordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WordController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
