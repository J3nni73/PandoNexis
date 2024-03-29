﻿using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNHeadModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNHeadModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNHeadViewModel model);

    }
}
