using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.DomainModels
{
    public class 単語帳学習状態
    {
        public IEnumerable<問題> 残りの問題 =>
            from item in 問題
            where !item.正解済み
            select item;

        public IEnumerable<問題> 問題 { get; set; } = new 問題[] { };
    }
}
