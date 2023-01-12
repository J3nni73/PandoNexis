using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Database.Objects
{
    public class PNFieldData
    {
        public Guid OwnerSystemId { get; set; }
        public string FieldDefinitionId { get; set; }
        public string Culture { get; set; }
        public int Index { get; set; }
        public bool BooleanValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public decimal DecimalValue { get; set; }
        public Guid GuidValue { get; set; }
        public string IndexedTextValue { get; set; }
        public int IntValue { get; set; }
        public string JsonValue { get; set; }
        public int LongValue { get; set; }
        public string TextValue { get; set; }
        public string ChildOwnerId { get; set; }
        public int ChildIndex { get; set; }
    }
}
