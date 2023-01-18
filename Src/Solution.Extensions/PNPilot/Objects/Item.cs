using PandoNexis.Accelerator.Extensions.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Objects
{
    public class Item
    {
        public Guid SystemId { get; set; }
        public Guid OrganizationSystemId { get; set; }
        public Guid ParentSystemId { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDescription { get; set; }
        public string ItemType { get; set; }
        public string ItemStatus { get; set; }
        public DateTime DueDateTime { get; set; }
        public DateTime CreatedDateTime { get; set;}
        public Guid CreatedBy { get; set; } 
        public DateTime UpdatedDateTime { get; set;}
        public Guid UpdatedBy { get; set; }
        public DateTime DeletedDateTime { get; set; }
        public Guid DeletedBy { get; set; }
        public PNFields Fields { get; set; }
    }
}
