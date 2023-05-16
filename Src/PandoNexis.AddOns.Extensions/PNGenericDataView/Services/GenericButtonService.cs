using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Websites;
using Microsoft.AspNetCore.Identity;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Services
{
    [Service(ServiceType = typeof(GenericButtonService))]
    public class GenericButtonService
    {
        public GenericButtonService()
        {
        }

        public GenericButton GetButton(Website website, string multiItemName, string buttonName, string buttonNameFieldDefinition, Guid entitySystemId)
        {

            var button = new GenericButton();
            var buttonLinksContainer = website.Fields.GetValue<IList<MultiFieldItem>>(multiItemName);
            if (buttonLinksContainer != null)
            {
                foreach (var field in buttonLinksContainer)
                {
                    if (field.Fields.GetValue<string>(buttonNameFieldDefinition) == buttonName)
                    {
                        button.EntitySystemId = entitySystemId;
                        button.ButtonText = field.Fields.GetValue<string>(buttonNameFieldDefinition);
                        button.UseConfirmation = field.Fields.GetValue<bool>(DataViewFieldNameConstants.UseConfirmation);
                        button.ConfirmationText = field.Fields.GetValue<string>(DataViewFieldNameConstants.ConfirmationText);
                        button.ButtonOpenInModal = field.Fields.GetValue<bool>(DataViewFieldNameConstants.ButtonOpenInModal);
                        button.PageSystemId = field.Fields.GetValue<Guid>(DataViewFieldNameConstants.ButtonPagePointer);
                        button.EndPointMethod = field.Fields.GetValue<string>(DataViewFieldNameConstants.EndPointMethod);
                        button.FieldTooltipMessage = field.Fields.GetValue<string>(DataViewFieldNameConstants.FieldTooltipMessage);
                    }
                }
            }
            return button;
        }
    }
}
