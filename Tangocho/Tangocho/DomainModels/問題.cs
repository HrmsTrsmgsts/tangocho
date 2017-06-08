using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.DomainModels
{
    public class 問題
    {
        public bool 正解済み { get; set; } = false;
        public string 単語 { get; set; }
        public string 意味 { get; set; }
    }
}
