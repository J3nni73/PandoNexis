using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Object
{
    public class PilotCustomer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> AddOns { get; set; }
        public string ProjectName { get; set; }
    }
}
