using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.ViewModels
{
    public class 辞書項目
    {
        public string 単語 { get; set; }
        public string 意味 { get; set; }
    }
    public class ホームViewModel
    {
        public string 英文 { get; set; }

        public string 削除される問題 { get; set; }

        public IEnumerable<辞書項目> 単語帳 { get; set; } = new 辞書項目[] { };
    }
}
