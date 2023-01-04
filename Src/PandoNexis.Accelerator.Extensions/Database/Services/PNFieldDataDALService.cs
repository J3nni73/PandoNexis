using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Database.Services
{
    [Service(ServiceType = typeof(PNFieldDataDALService))]
    public class PNFieldDataDALService
    {

        public PNFieldData Get()
        {
            return new PNFieldData();
        }
        public bool AddOrUpdate()
        { 
            return false; 
        }
    }
}
