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
    public class ホームController : Controller
    {
        ホームService service = new ホームService();

        public IActionResult 索引(ホームViewModel model)
        {
            var s = HttpContext.Session.GetString("ホーム");
            if (s != null)
            {
                if (string.IsNullOrEmpty(model.Sentence))
                {
                    model.Sentence = JsonConvert.DeserializeObject<ホームViewModel>(s).Sentence;
                }

                model.Tangocho = JsonConvert.DeserializeObject<ホームViewModel>(s).Tangocho;
            }
            return View(model);
        }

        public IActionResult 辞書を引く(ホームViewModel model)
        {
            service.Translate(model);
            HttpContext.Session.SetString("ホーム", JsonConvert.SerializeObject(model));
            return View("索引", model);
        }

        public IActionResult 削除する(ホームViewModel model,string word)
        {
            var s = HttpContext.Session.GetString("ホーム");
            if (s != null)
            {
                if (string.IsNullOrEmpty(model.Sentence))
                {
                    model.Sentence = JsonConvert.DeserializeObject<ホームViewModel>(s).Sentence;
                }

                model.Tangocho = JsonConvert.DeserializeObject<ホームViewModel>(s).Tangocho;
            }
            model.Tangocho =
                (from item in model.Tangocho
                where item.Word != word
                 select item
                 ).ToArray();
            HttpContext.Session.SetString("ホーム", JsonConvert.SerializeObject(model));
            return View("索引", model);
        }

        public IActionResult 単語帳学習を始める()
        {
            var s = HttpContext.Session.GetString("ホーム");
            if(s == null)
            {
                return View();
            }

            var practiceState =
                new 単語帳学習状態
                {
                    Questions =
                        from item in JsonConvert.DeserializeObject<ホームViewModel>(s).Tangocho
                        select new DomainModels.Item { Learned = false, Word = item.Word, Meaning = item.Meaning }
                };
                

            HttpContext.Session.SetString("単語帳学習", JsonConvert.SerializeObject(practiceState));

            return RedirectToAction("索引", "単語帳学習");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
