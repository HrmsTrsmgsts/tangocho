using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.Commons
{
    public class ViewModel<TInputModel> where TInputModel : new()
    {
        public TInputModel 入力 { get; set; } = new TInputModel();
    }
}
