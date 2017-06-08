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
using Marimo.Tangocho.InputModels;

namespace Marimo.Tangocho.Controllers
{
    public class ホームController : Controller
    {
        ホームService service = new ホームService();

        public IActionResult 索引(ホームInputModel 入力)
        {
            var sessionString = HttpContext.Session.GetString("ホーム");
            var モデル = new ホームViewModel();
            if (sessionString != null)
            {
                if (string.IsNullOrEmpty(入力.英文))
                {
                    モデル.入力.英文 = JsonConvert.DeserializeObject<ホームViewModel>(sessionString).入力.英文;
                }

                モデル.単語帳 = JsonConvert.DeserializeObject<ホームViewModel>(sessionString).単語帳;
            }
            return View(モデル);
        }

        public IActionResult 辞書を引く(ホームInputModel 入力)
        {
            var モデル = service.Translate(入力);
            HttpContext.Session.SetString("ホーム", JsonConvert.SerializeObject(モデル));
            return View("索引", モデル);
        }

        public IActionResult 削除する(ホームInputModel 入力, string word)
        {
            var s = HttpContext.Session.GetString("ホーム");
            var モデル = new ホームViewModel();
            if (s != null)
            {
                if (string.IsNullOrEmpty(入力.英文))
                {
                    入力.英文 = JsonConvert.DeserializeObject<ホームViewModel>(s).入力.英文;
                }

                モデル.単語帳 = JsonConvert.DeserializeObject<ホームViewModel>(s).単語帳;
            }
            モデル.単語帳 =
                (from item in モデル.単語帳
                where item.単語 != word
                 select item
                 ).ToArray();
            HttpContext.Session.SetString("ホーム", JsonConvert.SerializeObject(モデル));
            return View("索引", モデル);
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
                    問題 =
                        from item in JsonConvert.DeserializeObject<ホームViewModel>(s).単語帳
                        select new DomainModels.問題 { 正解済み = false, 単語 = item.単語, 意味 = item.意味 }
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
