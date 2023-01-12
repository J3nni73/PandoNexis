using Litium;
using Litium.AspNetCore.RequestTimeFeature;
using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using PandoNexis.AddOns.Extensions.Pilot.Definitions;
using PandoNexis.AddOns.Extensions.Pilot.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.Services.QuotaService;

namespace PandoNexis.AddOns.Extensions.Pilot.Services
{
    [Service(ServiceType = typeof(ItemProcessorService))]
    public class ItemProcessorService
    {
        public ItemProcessorService() { }

        public GenericGridViewRow BuildItemRow(Item item)
        {
            var row = new GenericGridViewRow()
            {
                Fields = new List<GenericGridViewField>()
            };
           

                GenericGridViewFieldSettings settings = new GenericGridViewFieldSettings { ReadOnly = true };

            row.Fields.Add(GetField(Guid.Empty, PilotConstants.SystemId, PilotConstants.SystemId, "string", false, item.SystemId.ToString()));
            row.Fields.Add(GetField(Guid.Empty, PilotConstants.OrganizationSystemId, PilotConstants.OrganizationSystemId, "string", false, item.OrganizationSystemId.ToString()));
            row.Fields.Add(GetField(Guid.Empty, PilotConstants.ParentSystemId, PilotConstants.ParentSystemId, "string", false, item.ParentSystemId.ToString()));
            row.Fields.Add(GetField(Guid.Empty, PilotConstants.ItemType, PilotConstants.ItemStatus, "string", false, item.ItemType));
            row.Fields.Add(GetField(Guid.Empty, PilotConstants.ItemTitle, PilotConstants.ItemTitle, "string", false, item.ItemTitle));
            row.Fields.Add(GetField(Guid.Empty, PilotConstants.ItemDescription, PilotConstants.ItemDescription, "string", false, item.ItemDescription));
            row.Fields.Add(GetField(Guid.Empty, PilotConstants.DueDateTime, PilotConstants.DueDateTime, "string", false, item.DueDateTime.ToString()));
           

            return row;
        }
        public List<GenericGridViewRow> BuildItemRows(List<Item> items)
        {
            var rows = new List<GenericGridViewRow>();

            foreach (var item in items)
            {

                rows.Add(BuildItemRow(item));
            }
            return rows;
        }

        public List<GenericGridViewField> GetFieldsToForm()
        {
            var result = new List<GenericGridViewField>();

            result.Add(GetField(Guid.Empty, PilotConstants.SystemId, PilotConstants.SystemId, "string", false, string.Empty));
            result.Add(GetField(Guid.Empty, PilotConstants.OrganizationSystemId, PilotConstants.OrganizationSystemId, "string", false, string.Empty));
            result.Add(GetField(Guid.Empty, PilotConstants.ParentSystemId, PilotConstants.ParentSystemId, "string", false, string.Empty));
            result.Add(GetField(Guid.Empty, PilotConstants.ItemType, PilotConstants.ItemStatus, "string", false, string.Empty));
            result.Add(GetField(Guid.Empty, PilotConstants.ItemTitle, PilotConstants.ItemTitle, "string", false, string.Empty));
            result.Add(GetField(Guid.Empty, PilotConstants.ItemDescription, PilotConstants.ItemDescription, "string", false, string.Empty));
            result.Add(GetField(Guid.Empty, PilotConstants.DueDateTime, PilotConstants.DueDateTime, "string", false, string.Empty));

            return result;
        }
        public GenericGridViewField GetField(Guid EntitySystemId, string fieldId, string fieldName, string fieldType, bool editable, string value)
        {
            var field = new GenericGridViewField();

            field.EntitySystemId = EntitySystemId.ToString();
            field.FieldID = fieldId;
            field.FieldName = fieldName;
            field.FieldValue = value;
            field.FieldType = fieldType;
            field.Settings = new GenericGridViewFieldSettings()
            {
                Editable = editable,
                ReadOnly = editable == false,
            }; ;
            //if (fieldType == "dropdown" || fieldType == "checkbox")
            //{
            //    newFields.DropDownOptions = GetDropDownOptions(field.FieldID);

            //}
            //if (fieldType == "productimageupload")
            //{
            //    newFields.DropDownOptions = GetArticleImages(variant);//image?.GetUrlToImage(Size.Empty, new Size(200, 120)).Url;
            //                                                          //newFields.FieldValue = image.Alt;
            //}


            return field;
        }
    }
}
