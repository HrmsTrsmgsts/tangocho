using Marimo.LinqToDejizo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Marimo.Tangocho.ViewModels;
using Marimo.Tangocho.Services;
using Newtonsoft.Json;
using Marimo.Tangocho.DomainModels;

namespace Marimo.Tangocho.Controllers
{
    public class HomeController : Controller
    {
        HomeService service = new HomeService();

        public IActionResult Index(HomePageViewModel model)
        {
            var s = HttpContext.Session.GetString("Home");
            if (s != null)
            {
                if (string.IsNullOrEmpty(model.Sentence))
                {
                    model.Sentence = JsonConvert.DeserializeObject<HomePageViewModel>(s).Sentence;
                }

                model.Tangocho = JsonConvert.DeserializeObject<HomePageViewModel>(s).Tangocho;
            }
            return View(model);
        }

        public IActionResult Translate(HomePageViewModel model)
        {
            service.Translate(model);
            HttpContext.Session.SetString("Home", JsonConvert.SerializeObject(model));
            return View("Index", model);
        }

        public IActionResult Delete(HomePageViewModel model,string word)
        {
            var s = HttpContext.Session.GetString("Home");
            if (s != null)
            {
                if (string.IsNullOrEmpty(model.Sentence))
                {
                    model.Sentence = JsonConvert.DeserializeObject<HomePageViewModel>(s).Sentence;
                }

                model.Tangocho = JsonConvert.DeserializeObject<HomePageViewModel>(s).Tangocho;
            }
            model.Tangocho =
                (from item in model.Tangocho
                where item.Word != word
                 select item
                 ).ToArray();
            HttpContext.Session.SetString("Home", JsonConvert.SerializeObject(model));
            return View("Index", model);
        }

        public IActionResult StartPractice()
        {
            var s = HttpContext.Session.GetString("Home");
            if(s == null)
            {
                return View();
            }

            var practiceState =
                new PracticeState
                {
                    Questions =
                        from item in JsonConvert.DeserializeObject<HomePageViewModel>(s).Tangocho
                        select new DomainModels.Item { Learned = false, Word = item.Word, Meaning = item.Meaning }
                };
                

            HttpContext.Session.SetString("Practice", JsonConvert.SerializeObject(practiceState));

            return RedirectToAction("Index", "Practice");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
