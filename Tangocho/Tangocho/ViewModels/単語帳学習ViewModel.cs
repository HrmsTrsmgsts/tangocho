using Marimo.Tangocho.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marimo.Tangocho.InputModels;
using Marimo.Tangocho.Commons;

namespace Marimo.Tangocho.ViewModels
{
    public class 単語帳学習ViewModel : ViewModel<単語帳学習InputModel>
    {
        public int 問題の総数 { get; set; }
        public int 残った問題数 { get; set; }

        public 問題 出題された問題 { get; set; }
        public IEnumerable<問題> 選択肢 { get; set; } = new 問題[] { };
    }
}
