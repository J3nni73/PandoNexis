using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium.Products;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.FieldTypes
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

        public virtual GenericGridViewFieldSettings GetSettings(GenericGridViewField field)
        {
            return new GenericGridViewFieldSettings() { ReadOnly = field.Settings.Editable == true };
        }

        public abstract string GetValue(GenericGridViewField field, Variant variant);
        //public abstract string GetValue(FieldConfigurationField field, Order order);

        public virtual object GetData(string type, string query)
        {
            return null;
        }

        public virtual object SetData(string type)
        {
            return null;
        }

        public abstract void AddSpecialFieldToCartAsync(string fieldId, decimal quantity);
    }
}