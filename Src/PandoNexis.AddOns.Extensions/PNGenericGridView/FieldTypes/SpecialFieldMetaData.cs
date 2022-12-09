using Litium.FieldFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.FieldTypes
{
    public class SpecialFieldMetaData : FieldTypeMetadataBase
    {
        public override string Id => "SpecialField";

        public override Type JsonType => typeof(SpecialFieldContainer);

        public override IFieldType CreateInstance(IFieldDefinition fieldDefinition)
        {
            var item = new SpecialField();
            item.Init(fieldDefinition);
            return item;
        }
    }

    public class SpecialField : FieldTypeBase
    {
        private string _type;
        public override object GetValue(ICollection<FieldData> fieldDatas)
        {
            return new SpecialFieldContainer(_type);
        }

        public override void Init(IFieldDefinition fieldDefinition)
        {
            base.Init(fieldDefinition);
            _type = fieldDefinition.Id;
        }

        protected override ICollection<FieldData> PersistFieldDataInternal(object item)
        {
            var fieldDataArray = new FieldData[0];
            return fieldDataArray;
        }
    }

    public class SpecialFieldContainer
    {
        public readonly string Type;
        public readonly Guid? CampaignId;
        public readonly object Value;
        public SpecialFieldContainer(string type, Guid? campaignId = null, object value = null)
        {
            Type = type;
            CampaignId = campaignId;
            Value = value;
        }
        public string GetFieldType()
        {

            return "decimal";

        }
    }
}
