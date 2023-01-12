
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    public abstract class GridSpecialFieldDataBase : IGridSpecialFieldData
    {
        public GridSpecialFieldDataBase()
        {
        }

        public virtual string GetGridViewFieldType()
        {
            return "string";
        }

        public virtual GenericGridViewFieldSettings GetSettings(FieldConfigurationField field)
        {
            return new GenericGridViewFieldSettings() { ReadOnly = !field.IsEditable };
        }

        public abstract string GetValue(FieldConfigurationField field, Variant variant);
        //public abstract string GetValue(FieldConfigurationField field, Order order);

        public virtual object GetData(string type, string query)
        {
            return null;
        }

        public virtual object SetData(string type, FieldUpdateData fieldData)
        {
            return null;
        }
    }
}