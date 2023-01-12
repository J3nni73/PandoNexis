using AutoMapper;
using JetBrains.Annotations;
using Litium.Accelerator.Builders;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Extensions;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Common;
using Litium.Customers;
using Litium.Runtime.AutoMapper;
using Litium.Security;
using Litium.Web.Models.Websites;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels
{
    public class GenericGridViewModel : IAutoMapperConfiguration, IViewModel
    {
        public IList<string> DataSources { get; set; }
        public int PageSize { get; set; }
        public bool HasMegaMenu { get; set; }
        public bool EnableDropZone { get; set; }
        public bool EnableFieldConfigurator { get; set; }
        //[UsedImplicitly]
        public void Configure(IMapperConfigurationExpression cfg) => cfg.CreateMap<PageModel, GenericGridViewModel>().ForMember(x => x.DataSources, m => m.MapFromField(Constants.GenericGridView_PageFieldNameConstants.DataSource))
            .ForMember(x => x.PageSize, m => m.MapFrom(genericGridViewPage => genericGridViewPage.GetValue<int>(PageFieldNameConstants.PageSize)))
            .ForMember(x => x.HasMegaMenu, m => m.MapFrom(genericGridViewPage => genericGridViewPage.GetValue<bool>(Constants.GenericGridView_PageFieldNameConstants.HasMegaMenu)))
            .ForMember(x => x.EnableDropZone, m => m.MapFrom(genericGridViewPage => genericGridViewPage.GetValue<bool>(Constants.GenericGridView_PageFieldNameConstants.EnableDropZone)))
            //.ForMember(x => x.EnableFieldConfigurator, m => m.MapFrom<FieldConfiguratorResolver>())
            ;
        //[UsedImplicitly]
        private class FieldConfiguratorResolver : IValueResolver<PageModel, GenericGridViewModel, bool>
        {
            private readonly SecurityContextService _securityContextService;
            private readonly SettingService _settingService;
            private readonly RequestModelAccessor _requestModelAccessor;
            private readonly PersonStorage _personStorage;
            private readonly PersonService _personService;
            private readonly Guid _fieldConfiguratorGroupId;

            public FieldConfiguratorResolver(SecurityContextService securityContextService, GroupService groupService, SettingService settingService, RequestModelAccessor requestModelAccessor,
                 PersonStorage personStorage)
            {
                _securityContextService = securityContextService;
                _settingService = settingService;
                _requestModelAccessor = requestModelAccessor;
                _personStorage = personStorage;
                _fieldConfiguratorGroupId = groupService.Get<Group>("FieldConfigurator")?.SystemId ?? Guid.Empty;
            }

            public bool Resolve(PageModel source, GenericGridViewModel destination, bool destMember, ResolutionContext context)
            {
                bool hasPermission = false;
                var personSystemId = _securityContextService.GetIdentityUserSystemId();
                if (personSystemId != null)
                {
                    var currentPerson = _personService.Get(personSystemId.Value);
                    if (currentPerson != null)
                    {
                        hasPermission = currentPerson.GroupLinks.Any(i => i.GroupSystemId == _fieldConfiguratorGroupId);
                    }
                }

                // ensure defualt fields
                if (_personStorage.CurrentSelectedOrganization != null)
                {
                    var types = source.Fields.GetValue<IList<string>>(Constants.GenericGridView_PageFieldNameConstants.DataSource);
                }

                return hasPermission;
            }
        }
    }
}