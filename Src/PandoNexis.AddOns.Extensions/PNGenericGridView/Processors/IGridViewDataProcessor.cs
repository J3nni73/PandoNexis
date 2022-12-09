using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Processors
{
    [Service(ServiceType = typeof(IGridViewDataProcessor), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    
    public interface IGridViewDataProcessor
    {
        Task<object> GetGridView(string data);

        Task<object> GetGridForm(string data);
        Task<object> UpdateRow(string data);
        Task<object> HandleFormData(string data);
        void CalculateFields(Variant variant, MultiFieldItem customerData/*, List<FieldConfigurationField> fields*/);
        Task<object> GetGridViewForExport(string data);
    }
}