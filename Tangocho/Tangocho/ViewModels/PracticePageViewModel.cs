using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marimo.Tangocho.ViewModels
{
    public class PracticePageViewModel
    {
        public int TotalCount { get; set; }
        public int RestQuestionsCount { get; set; }

        public DomainModels.Item QuestionWord { get; set; }
        public IEnumerable<DomainModels.Item> Answers { get; set; } = new DomainModels.Item[] { };
    }
}
