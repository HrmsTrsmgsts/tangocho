using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.ViewModels
{
    public class Item
    {
        public string Word { get; set; }
        public string Meaning { get; set; }
    }
    public class ホームViewModel
    {
        public string Sentence { get; set; }

        public string DeletedWord { get; set; }

        public IEnumerable<Item> Tangocho { get; set; } = new Item[] { };
    }
}
