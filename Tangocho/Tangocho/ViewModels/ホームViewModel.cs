using Marimo.Tangocho.Commons;
using Marimo.Tangocho.DomainModels;
using Marimo.Tangocho.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.ViewModels
{

    public class ホームViewModel : ViewModel<ホームInputModel>
    {
        public IEnumerable<辞書項目> 単語帳 { get; set; } = new 辞書項目[] { };
    }
}
