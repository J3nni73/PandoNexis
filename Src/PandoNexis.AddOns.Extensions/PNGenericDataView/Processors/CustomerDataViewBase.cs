using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(ServiceType = typeof(CustomerDataViewBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class CustomerDataViewBase : IGenericDataViewProcessor
    {
        public Task<GenericDataContainer> ButtonClick( GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDataForm(string data)
        {
            throw new NotImplementedException();
        }

        public Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            throw new NotImplementedException();
        }

        public GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            throw new NotImplementedException();
        }

        public GenericDataContainer GetFields(string templateId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

        public Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateRow(string data)
        {
            throw new NotImplementedException();
        }
    }
}
