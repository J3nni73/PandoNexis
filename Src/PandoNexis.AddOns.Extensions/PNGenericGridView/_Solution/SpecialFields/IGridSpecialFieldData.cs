
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium.Owin.InversionOfControl;
using Litium.Products;
using System.Reflection;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Litium.Runtime.DependencyInjection.Service(
  ServiceType = typeof(IGridSpecialFieldData),
  Lifetime = Litium.Runtime.DependencyInjection.DependencyLifetime.Transient,
  NamedService = true)]
    public interface IGridSpecialFieldData
    {
        string GetGridViewFieldType();

        GenericGridViewFieldSettings GetSettings(FieldConfigurationField field);
        string GetValue(FieldConfigurationField field, Variant variant);

        object GetData(string type, string query);

        object SetData(string type, FieldUpdateData fieldData);
    }

    //internal class Components : IComponentInstaller
    //{
    //    public void Install(IIoCContainer container, Assembly[] assemblies)
    //    {
    //        container.For<IGridSpecialFieldData>().AsPlugin().RegisterAsSingleton();
    //    }
    //}
}