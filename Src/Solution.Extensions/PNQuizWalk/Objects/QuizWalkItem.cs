using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Objects
{
    public class QuizWalkItem
    {
        public Guid SystemId { get; set; }
        public string Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
