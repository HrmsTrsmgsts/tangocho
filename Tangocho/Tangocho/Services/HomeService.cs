using Marimo.LinqToDejizo;
using Marimo.Tangocho.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.Services
{
    public class HomeService
    {
        DejizoSource dictionaries = new DejizoSource();

        public void Translate(HomePageViewModel model)
        {
            if(model.Sentence.IsEmpty())
            {
                return;
            }

            var tangocho =
                (from word in model.Sentence.Split(' ')
                let modifiedWord = word.Replace(".", "").Replace(",", "").ToLower()
                where !string.IsNullOrWhiteSpace(modifiedWord)
                let meaning =
                        (from item in dictionaries.EJdict
                         where item.HeaderText == modifiedWord
                         select item.BodyText)
                         .FirstOrDefault()
                where meaning != null
                select new Item { Word = modifiedWord, Meaning = meaning }).Distinct(x => x.Word);

            model.Tangocho = tangocho;
        }
    }
}