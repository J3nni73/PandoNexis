using Litium.FieldFramework.FieldTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Definitions.FieldHelper
{
    public class FieldOptionChanges
    {
        public string FieldDefinitionId { get; set; }
        public string Area { get; set; }
        public List<TextOption.Item> Options { get; set;}
    }
}
