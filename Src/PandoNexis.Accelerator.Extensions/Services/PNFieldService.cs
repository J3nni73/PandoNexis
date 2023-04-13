using Litium.Blocks;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Websites;

namespace PandoNexis.Accelerator.Extensions.Services
{
    [Service(ServiceType = typeof(PNFieldService), Lifetime = DependencyLifetime.Scoped)]
    [RequireServiceImplementation]
    public class PNFieldService
    {
        private readonly FieldDefinitionService _fieldDefinitionService;

        public PNFieldService(FieldDefinitionService fieldDefinitionService)
        {
            _fieldDefinitionService = fieldDefinitionService;
        }
        public List<FieldDefinition> GetMultiFieldDefinitionList(FieldDefinition fieldDefinition) {
       
            var fielddefinitions = new List<FieldDefinition>();
            var fieldDefinitionOption = fieldDefinition.Option;
            
            if (fieldDefinitionOption is MultiFieldOption)
            {
                var fieldOption = (MultiFieldOption)fieldDefinitionOption;
                var fields = fieldOption.Fields;
                
                foreach (var fieldName in fields)
                {
                    var definition = GetFieldDefinitionByAreaType(fieldDefinition.AreaType.Name, fieldName);
                    if (definition != null)
                    {   
                        fielddefinitions.Add(definition);
                    }
                }
            }
            else
            {
                fielddefinitions.Add(fieldDefinition);
            }
            
            return fielddefinitions;
        }

        public FieldDefinition GetFieldDefinitionByAreaType(string areaType, string fieldName)
        {
            if (areaType == "ProductArea")
            {
                return _fieldDefinitionService.Get<ProductArea>(fieldName);
            }
            else if (areaType == "WebsiteArea")
            {
                return _fieldDefinitionService.Get<WebsiteArea>(fieldName);
            }
            else if (areaType == "BlockArea")
            {
                return _fieldDefinitionService.Get<BlockArea>(fieldName);
            }
            else if (areaType == "CustomerArea")
            {
                return _fieldDefinitionService.Get<CustomerArea>(fieldName);
            }
            else if (areaType == "GlobalizationArea")
            {
                return _fieldDefinitionService.Get<GlobalizationArea>(fieldName);
            }

            return null;
        }
    }
}
