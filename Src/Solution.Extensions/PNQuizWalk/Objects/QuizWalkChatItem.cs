using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Objects
{
    public class QuizWalkChatItem
    {
        public Guid SystemId { get; set; } = Guid.NewGuid();
        public Guid OrganizationSystemId{ get; set; }
        public Guid PersonSystemId { get; set; }
        public string Chat { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime DeletedDateTime { get; set; }
        public Guid DeletedBy { get; set; }

    }
}
