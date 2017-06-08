using Marimo.LinqToDejizo;
using Marimo.Tangocho.InputModels;
using Marimo.Tangocho.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.Services
{
    public class ホームService
    {
        DejizoSource 辞書 = new DejizoSource();

        public ホームViewModel Translate(ホームInputModel 入力)
        {
            var モデル = new ホームViewModel();
            if (入力.英文.IsEmpty())
            {
                return モデル;
            }

            var tangocho =
                (from word in 入力.英文.Split(' ')
                let modifiedWord = word.Replace(".", "").Replace(",", "").ToLower()
                where !string.IsNullOrWhiteSpace(modifiedWord)
                let meaning =
                        (from item in 辞書.EJdict
                         where item.HeaderText == modifiedWord
                         select item.BodyText)
                         .FirstOrDefault()
                where meaning != null
                select new 辞書項目 { 単語 = modifiedWord, 意味 = meaning }).Distinct(x => x.単語);

            モデル.単語帳 = tangocho;
            return モデル;
        }
    }
}