using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Objects
{
    public class ItemStatus
    {
        public Guid SystemId { get; set; }
        public Guid ItemTypeSystemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
