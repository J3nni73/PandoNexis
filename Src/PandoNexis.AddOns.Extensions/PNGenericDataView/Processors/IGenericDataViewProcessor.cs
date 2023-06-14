using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(ServiceType = typeof(IGenericDataViewProcessor), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    
    public interface IGenericDataViewProcessor
    {
        Task<GenericDataView> GetDataView(Guid pageSystemId, string data);
        GenericDataViewSettings GetDataViewSettings(Guid pageSystemId);
        Task<object> GetDataForm(string data);
        Task<GenericDataContainer> UpdateField(GenericDataField fieldData);
        Task<object> HandleFormData(string data);
        Task<object> GetGridViewForExport(string data);
        GenericDataContainer GetFields(string templateId);
        Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data);

    }
}