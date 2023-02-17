using CharacterLookup.Models;
using CharacterLookupMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharacterLookupMVC.Controllers
{
    [AllowAnonymous]
    public class WordController : Controller
    {
        
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

            if (!WordsHolder.Words.ContainsKey(id[0]))
            {
                return View();
            }

            var word = WordsHolder.Words[id[0]];

            return View(word);
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
