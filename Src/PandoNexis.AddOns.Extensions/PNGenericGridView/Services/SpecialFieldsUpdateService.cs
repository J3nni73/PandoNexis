using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litium.Accelerator.GridView.Services
{
    [Service(ServiceType = typeof(SpecialFieldsUpdateService), Lifetime = DependencyLifetime.Singleton)]
    public class SpecialFieldsUpdateService
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly PersonStorage _personStorage;

        public SpecialFieldsUpdateService(RequestModelAccessor requestModelAccessor, PersonStorage personStorage)
        {
            _requestModelAccessor = requestModelAccessor;
            _personStorage = personStorage;
        }

        public void UpdateSpecialField()
        {

        }
      

    }
}
