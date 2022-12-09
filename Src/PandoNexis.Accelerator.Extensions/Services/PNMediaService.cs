using Litium.Accelerator.ViewModels;
using Litium.Blocks;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Media;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Websites;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Services
{
    [Service(ServiceType = typeof(PNMediaService), Lifetime = DependencyLifetime.Scoped)]
    [RequireServiceImplementation]
    public class PNMediaService
    {
        private readonly FieldDefinitionService _fieldDefinitionService;

        public PNMediaService(FieldDefinitionService fieldDefinitionService)
        {
            _fieldDefinitionService = fieldDefinitionService;
        }
      
    }
}
