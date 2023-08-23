using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(ServiceType = typeof(IGenericDataViewProcessor), Lifetime = DependencyLifetime.Transient, NamedService = true)]

    public interface IGenericDataViewProcessor
    {
        Guid _currentPageSystemId { get; set; }
        Task<GenericDataView> GetDataView(Guid pageSystemId, string data);
        GenericDataViewSettings GetDataViewSettings(Guid pageSystemId);
        Task<GenericDataContainer> UpdateField(GenericDataField fieldData);
        Task<object> GetGridViewForExport(string data);
        GenericDataContainer GetFields(string templateId);
        Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data);
    }
}