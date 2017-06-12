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
using Marimo.Tangocho.Commons;

namespace Marimo.Tangocho.Controllers
{
    public class ホームController : Controller<ホームViewModel, ホームInputModel>
    {
        ホームService service = new ホームService();

        public IActionResult 索引(ホームInputModel 入力)
        {
            var モデル = Getモデル(入力);
            Setセッション(モデル);
            return View(モデル);
        }

        public IActionResult 辞書を引く(ホームInputModel 入力)
        {
            var モデル = service.辞書を引く(Getモデル(入力));
            Setセッション(モデル);
            return View("索引", モデル);
        }

        public IActionResult 削除する(ホームInputModel 入力)
        {
            var モデル = Getモデル(入力);
            モデル.入力 = 入力;
            モデル.単語帳 =
                (from item in モデル.単語帳
                where item.単語 != 入力.削除される単語
                 select item
                 ).ToArray();
            Setセッション(モデル);
            return View("索引", モデル);
        }

        public IActionResult 単語帳学習を始める()
        {
            var モデル = Getセッション<ホームViewModel>();
            if(モデル == null)
            {
                return View();
            }

            Setセッション(
                new 単語帳学習状態
                {
                    問題 =
                            from item in モデル.単語帳
                            select new 問題 { 正解済み = false, 単語 = item.単語, 意味 = item.意味 }
                });

            return RedirectToAction("索引", "単語帳学習");
        }
    }
}
