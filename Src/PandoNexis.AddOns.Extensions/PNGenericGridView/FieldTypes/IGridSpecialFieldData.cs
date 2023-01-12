using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;
using Litium.Owin.InversionOfControl;
using System.Reflection;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.FieldTypes
{
    [Litium.Runtime.DependencyInjection.Service(
   ServiceType = typeof(IGridSpecialFieldData),
   Lifetime = Litium.Runtime.DependencyInjection.DependencyLifetime.Transient,
   NamedService = true)]
    public interface IGridSpecialFieldData
    {
        string GetGridViewFieldType();

        GenericGridViewFieldSettings GetSettings(GenericGridViewField field);
        string GetValue(GenericGridViewField field, Variant variant);

        object GetData(string type, string query);

        object SetData(string type, FieldUpdateData fieldData);

        void AddSpecialFieldToCartAsync(string fieldId, decimal quantity);
    }
    //internal class Components : IComponentInstaller
    //{
    //    public void Install(IIoCContainer container, Assembly[] assemblies)
    //    {
    //        container.For<IGridSpecialFieldData>().AsPlugin().RegisterAsSingleton();
    //    }
    //}

}