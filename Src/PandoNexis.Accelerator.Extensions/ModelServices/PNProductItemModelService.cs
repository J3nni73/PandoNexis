using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNProductItemModelService))]
    public abstract class PNProductItemModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNProductItemViewModel model);

    }
}
