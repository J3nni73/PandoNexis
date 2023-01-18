using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Objects
{
    public class PilotCustomer
    {
        public Guid SystemId { get; set; }
        public string Name { get; set; }
        public List<PilotProject> Projects { get; set; }
    }
}
