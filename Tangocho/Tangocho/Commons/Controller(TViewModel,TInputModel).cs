using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.Commons
{
    public class Controller<TViewModel, TInputModel> : Controller where TViewModel : ViewModel<TInputModel>, new() where TInputModel : new()
    {
        public TViewModel Getモデル(TInputModel 入力)
        {
            var モデル = Getセッション<TViewModel>();
            モデル.入力 = 入力;
            return モデル;
        }

        public T Getセッション<T>() where T : new()
        {
            var str = HttpContext.Session.GetString(typeof(T).FullName);
            if (str == null)
            {
                return new T();
            }
            return JsonConvert.DeserializeObject<T>(str);
        }

        public void Setセッション<T>(T value)
        {
            HttpContext.Session.SetString(typeof(T).FullName, JsonConvert.SerializeObject(value));
        }
    }
}
