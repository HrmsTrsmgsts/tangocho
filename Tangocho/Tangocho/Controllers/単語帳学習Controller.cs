using Marimo.Tangocho.DomainModels;
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
    public class 単語帳学習Controller : Controller
    {
        public IActionResult 索引(単語帳学習ViewModel model)
        {
            var s = HttpContext.Session.GetString("単語帳学習");
            var state = JsonConvert.DeserializeObject<単語帳学習状態>(s);

            model.QuestionWord =
                (from item in state.RestQuestions
                 let guid = Guid.NewGuid()
                 orderby guid
                 select item
                ).First();

            model.Answers =
                from item in
                    (from item in state.RestQuestions
                     where item != model.QuestionWord
                     let guid = Guid.NewGuid()
                     orderby guid
                     select item
                    ).Take(4)
                    .Concat(new[] { model.QuestionWord })
                let guid = Guid.NewGuid()
                orderby guid
                select item;

            model.TotalCount = state.Questions.Count();
            model.RestQuestionsCount = state.RestQuestions.Count();

            return View(model);
        }

        public IActionResult 答える(単語帳学習ViewModel model, string questionWord, string answerWord)
        {
            var s = HttpContext.Session.GetString("単語帳学習");
            var state = JsonConvert.DeserializeObject<単語帳学習状態>(s);

            if(questionWord == answerWord)
            {
                (from item in state.Questions
                 where item.Word == questionWord
                 select item
                 ).Single().Learned = true;
            }

            if(state.RestQuestions.IsEmpty())
            {
                return RedirectToAction("索引", "ホーム");
            }

            HttpContext.Session.SetString("単語帳学習", JsonConvert.SerializeObject(state));

            model.QuestionWord =
                (from item in state.RestQuestions
                 let guid = Guid.NewGuid()
                 orderby guid
                 select item
                ).First();

            model.Answers =
                from item in
                    (from item in state.RestQuestions
                     where item != model.QuestionWord
                     let guid = Guid.NewGuid()
                     orderby guid
                     select item
                    ).Take(4)
                    .Concat(new[] { model.QuestionWord })
                let guid = Guid.NewGuid()
                orderby guid
                select item;

            model.TotalCount = state.Questions.Count();
            model.RestQuestionsCount = state.RestQuestions.Count();

            return View("索引", model);
        }
    }
}
