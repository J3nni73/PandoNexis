using Litium.FieldFramework.FieldTypes;

namespace PandoNexis.Accelerator.Extensions.Definitions.FieldHelper
{
    public class FieldOptionChanges
    {
        public string FieldDefinitionId { get; set; }
        public string Area { get; set; }
        public List<TextOption.Item> Options { get; set;}
    }
}
