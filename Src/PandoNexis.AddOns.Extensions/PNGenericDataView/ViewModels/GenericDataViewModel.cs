using AutoMapper;
using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Websites;
using Litium.Web.Models.Blocks;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.ViewModels
{
    public class GenericDataViewModel : IAutoMapperConfiguration, IViewModel
    {
        public Guid PageSystemId { get; set; }
        public Dictionary<string, List<BlockModel>> Blocks { get; set; }
        public IList<string> DisplayTypes { get; set; } = new List<string>();
        //[UsedImplicitly]
        public void Configure(IMapperConfigurationExpression cfg) => cfg.CreateMap<PageModel, GenericDataViewModel>()
          
            .ForMember(x => x.PageSystemId, m => m.MapFrom<SystemIdResolver>())
            ;
        //[UsedImplicitly]
        private class SystemIdResolver : IValueResolver<PageModel, GenericDataViewModel, Guid>
        {
           public Guid Resolve(PageModel source, GenericDataViewModel destination, Guid destMember, ResolutionContext context)
            {
                return source.SystemId;
            }
        }
    }
}