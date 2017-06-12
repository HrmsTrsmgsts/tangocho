using Marimo.LinqToDejizo;
using Marimo.Tangocho.DomainModels;
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

        public ホームViewModel 辞書を引く(ホームViewModel モデル)
        {
            if (string.IsNullOrWhiteSpace(モデル.入力.英文))
            {
                return モデル;
            }

            モデル.単語帳 = 
                (from 未処理単語 in モデル.入力.英文.Split(' ')
                let 単語 = 未処理単語.Replace(".", "").Replace(",", "").ToLower()
                where !string.IsNullOrWhiteSpace(単語)
                let 意味 =
                        (from item in 辞書.EJdict
                        where item.HeaderText == 単語
                        select item.BodyText)
                        .FirstOrDefault()
                where 意味 != null
                select new 辞書項目 { 単語 = 単語, 意味 = 意味 }).Distinct(x => x.単語);
            return モデル;
        }
    }
}