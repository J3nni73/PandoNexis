using Litium.FieldFramework;
using Litium.Products;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Processors
{
    public abstract class GridViewDataProcessorBase : IGridViewDataProcessor
    {
        public virtual void CalculateFields(Variant variant, MultiFieldItem customerData)
        {
            throw new NotImplementedException();
        }

        public abstract Task<object> GetGridView(string data);
        public abstract Task<object> GetGridForm(string data);

        public abstract Task<object> GetGridViewForExport(string data);

        public virtual Task<object> UpdateRow(string data)
        {
            throw new NotImplementedException();
        }

        public abstract Task<object> HandleFormData(string data);


    }
}
