using Litium.Accelerator.Services;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.Services.PNStockService;
using Solution.Extensions.PNPilot.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(PilotCustomerService))]
    public class PilotCustomerService
    {
        public List<PilotCustomer> GetCustomers()
        {
            var customers = new List<PilotCustomer>
            {
                new PilotCustomer
                {
                    Name = "Dixcon",
                    Description = "hej hopp",
                    ProjectName = "Dixcon",
                    AddOns = new List<string>()
                    {
                        "PNCollectionPage",
                        "PNInfiniteScroll"
                    }
                },
                new PilotCustomer
                {
                    Name = "Pando Nexis",
                    Description = "hej hopp", 
                    ProjectName ="PandoNexis",
                    AddOns = new List<string>()
                    {
                        "PNStockService",
                        "PNWebsiteSelector"
                    }
                },
            };


            return customers;
        }
    }
}
