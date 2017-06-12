using Marimo.Tangocho.Commons;
using Marimo.Tangocho.DomainModels;
using Marimo.Tangocho.InputModels;
using Marimo.Tangocho.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.Controllers
{
    public class 単語帳学習Controller : Controller<単語帳学習ViewModel, 単語帳学習InputModel>
    {
        public IActionResult 索引(単語帳学習InputModel 入力)
        {
            var モデル = Getモデル(入力);
            var 回答状態 = Getセッション<単語帳学習状態>();

            モデル.出題された問題 =
                (from item in 回答状態.残りの問題
                 let guid = Guid.NewGuid()
                 orderby guid
                 select item
                ).First();

            モデル.選択肢 =
                from item in
                    (from item in 回答状態.残りの問題
                     where item != モデル.出題された問題
                     let guid = Guid.NewGuid()
                     orderby guid
                     select item
                    ).Take(4)
                    .Concat(new[] { モデル.出題された問題 })
                let guid = Guid.NewGuid()
                orderby guid
                select item;

            モデル.問題の総数 = 回答状態.問題.Count();
            モデル.残った問題数 = 回答状態.残りの問題.Count();

            return View(モデル);
        }

        public IActionResult 答える(単語帳学習InputModel 入力)
        {
            var 回答状態 = Getセッション<単語帳学習状態>();

            if (入力.問題の単語 == 入力.答えに対応する単語)
            {
                (from item in 回答状態.問題
                 where item.単語 == 入力.問題の単語
                 select item
                 ).Single().正解済み = true;
            }

            if(回答状態.残りの問題.IsEmpty())
            {
                return RedirectToAction("索引", "ホーム");
            }

            Setセッション(回答状態);

            var モデル = Getモデル(入力);

            モデル.出題された問題 =
                (from item in 回答状態.残りの問題
                 let guid = Guid.NewGuid()
                 orderby guid
                 select item
                ).First();

            モデル.選択肢 =
                from item in
                    (from item in 回答状態.残りの問題
                     where item != モデル.出題された問題
                     let guid = Guid.NewGuid()
                     orderby guid
                     select item
                    ).Take(4)
                    .Concat(new[] { モデル.出題された問題 })
                let guid = Guid.NewGuid()
                orderby guid
                select item;

            モデル.問題の総数 = 回答状態.問題.Count();
            モデル.残った問題数 = 回答状態.残りの問題.Count();

            return View("索引", モデル);
        }
    }
}
