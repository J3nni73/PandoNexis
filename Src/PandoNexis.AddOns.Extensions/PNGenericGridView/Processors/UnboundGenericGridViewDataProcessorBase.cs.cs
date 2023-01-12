using Litium.FieldFramework;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Processors
{
    public abstract class UnboundGenericGridViewDataProcessorBase : IGridViewDataProcessor
    {
        public virtual void CalculateFields(object rowObject, object helperObject)
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
