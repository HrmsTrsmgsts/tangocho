using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.DomainModels
{
    public class Item
    {
        public bool Learned { get; set; } = false;
        public string Word { get; set; }
        public string Meaning { get; set; }
    }

    public class PracticeState
    {
        public IEnumerable<Item> RestQuestions =>
            from item in Questions
            where !item.Learned
            select item;

        public IEnumerable<Item> Questions { get; set; } = new Item[] { };
    }
}
