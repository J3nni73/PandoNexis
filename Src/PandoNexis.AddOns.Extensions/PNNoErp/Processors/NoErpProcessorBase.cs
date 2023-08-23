using Litium.Accelerator.Utilities;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Processors
{
    [Service(ServiceType = typeof(NoErpProcessorBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class NoErpProcessorBase : IGenericDataViewProcessor
    {
        private readonly GenericDataViewService _genericDataViewService;
        private readonly PersonStorage _personStorage;
        private readonly string _baseUrl;
        private readonly string _token;
        public Guid _currentPageSystemId { get; set; }

        public NoErpProcessorBase(PersonStorage personStorage,
            GenericDataViewService genericDataViewService)
        {
            _personStorage = personStorage;
            if (_personStorage?.CurrentSelectedOrganization != null)
            {
                _baseUrl = _personStorage.CurrentSelectedOrganization.Fields.GetValue<string>(NoErpOrderAdminConstants.BaseUrl);
                _token = _personStorage.CurrentSelectedOrganization.Fields.GetValue<string>(NoErpOrderAdminConstants.Authorization);
                _genericDataViewService = genericDataViewService;
            }
        }



        public abstract Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data);

        public abstract Task<GenericDataView> GetDataView(Guid pageSystemId, string data);

        public GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            return _genericDataViewService.GetDataViewSettings(pageSystemId);
        }

        public GenericDataContainer GetFields(string templateId)
        {
            throw new NotImplementedException();
        }

        public abstract Task<object> GetGridViewForExport(string data);

        public abstract Task<GenericDataContainer> UpdateField(GenericDataField fieldData);


    }
}
