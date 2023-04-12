using Litium.Accelerator.Builders;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Database.Services;
using PandoNexis.Accelerator.Extensions.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNHeaderModelService))]
    public abstract class PNHeaderModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNHeaderViewModel model);

    }
}
