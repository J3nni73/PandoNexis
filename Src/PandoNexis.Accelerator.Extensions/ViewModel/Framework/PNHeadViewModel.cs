using JetBrains.Annotations;
using Litium.Runtime.AutoMapper;
using Litium.Accelerator.ViewModels.Framework;
using AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Framework
{
    public class PNHeadViewModel : HeadViewModel, IAutoMapperConfiguration
    {
        public List<string> PNHeadDataList { get; set; } = new List<string>();
        public string CssFileName { get; set; } = string.Empty;

        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<HeadViewModel, PNHeadViewModel>();
        }
    }
}
