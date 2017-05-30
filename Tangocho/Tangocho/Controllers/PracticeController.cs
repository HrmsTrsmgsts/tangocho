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
    public class PracticeController : Controller
    {
        public IActionResult Index(PracticePageViewModel model)
        {
            var s = HttpContext.Session.GetString("Practice");
            var state = JsonConvert.DeserializeObject<PracticeState>(s);

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

        public IActionResult Answer(PracticePageViewModel model, string questionWord, string answerWord)
        {
            var s = HttpContext.Session.GetString("Practice");
            var state = JsonConvert.DeserializeObject<PracticeState>(s);

            if(questionWord == answerWord)
            {
                (from item in state.Questions
                 where item.Word == questionWord
                 select item
                 ).Single().Learned = true;
            }

            if(state.RestQuestions.IsEmpty())
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetString("Practice", JsonConvert.SerializeObject(state));

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

            return View("Index", model);
        }
    }
}
