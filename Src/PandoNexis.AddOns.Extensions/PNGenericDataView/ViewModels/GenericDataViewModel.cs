using AutoMapper;
using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Websites;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.ViewModels
{
    public class GenericDataViewModel : IAutoMapperConfiguration, IViewModel
    {
        public Guid PageSystemId { get; set; }
        //public string DataArea { get; set; }
        //public string AreaSource { get; set; }

        //public bool HasMegaMenu { get; set; }
        //public bool EnableDropZone { get; set; }
        //public bool EnableFieldConfigurator { get; set; }

        public IList<string> DisplayTypes { get; set; } = new List<string>();
        //[UsedImplicitly]
        public void Configure(IMapperConfigurationExpression cfg) => cfg.CreateMap<PageModel, GenericDataViewModel>()
            //.ForMember(x => x.PageSystemId, m => m.)
            //.ForMember(x => x.AreaSource, m => m.MapFromField(PageFieldNameConstants.AreaSource))
            //.ForMember(x => x.DisplayTypes, m => m.MapFromField(PageFieldNameConstants.DisplayTypes))
            //.ForMember(x => x.HasMegaMenu, m => m.MapFrom(genericDataViewPage => genericDataViewPage.GetValue<bool>(PageFieldNameConstants.HasMegaMenu)))
            //.ForMember(x => x.EnableDropZone, m => m.MapFrom(genericDataViewPage => genericDataViewPage.GetValue<bool>(PageFieldNameConstants.EnableDropZone)))
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