using Litium.Customers;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Constants;

namespace PandoNexis.AddOns.Extensions.PNPilot.Definitions
{
    public class FieldHelperSource : FieldHelper
    {
        public FieldHelperSource(FieldDefinitionService fieldDefinitionService, SecurityContextService securityContextService, LanguageService languageService) : base(fieldDefinitionService, securityContextService, languageService)
        {
        }

        public override void HandleFieldOptions()
        {
            var changes = new List<FieldOptionChanges>()
            {
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DataArea, PilotProcessorConstants.Pilot),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, PilotProcessorConstants.WorkItems),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, PilotProcessorConstants.NewOrViewWorkItem),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, PilotProcessorConstants.NewOrViewTimeSpent),
                GetFieldOption(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonNames, PilotProcessorConstants.NewWorkItem),
                GetFieldOption(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonNames, PilotProcessorConstants.ViewWorkItem),
                GetFieldOption(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonNames, PilotProcessorConstants.ViewWorkItem),
                GetFieldOption(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonNames, PilotProcessorConstants.ViewTimeSpentOnWorkItem),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.ProjectType,ProjectTypeConstants.PandoNexisAccelerator ),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.ProjectType,ProjectTypeConstants.LitiumAccelerator ),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.ProjectType,ProjectTypeConstants.SubContractor ),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.ProjectType,ProjectTypeConstants.NextFramework ),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.AddOnStatus,AddonStatusConstants.Intended ),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.AddOnStatus,AddonStatusConstants.Ordered ),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.AddOnStatus,AddonStatusConstants.Implemented ),
                GetFieldOption(nameof(CustomerArea), PilotFieldNameConstants.AddOnStatus,AddonStatusConstants.Disconnected ),
                GetFieldOption(nameof(CustomerArea), ContactLoggConstants.ContactType,ContactTypeConstants.Email),
                GetFieldOption(nameof(CustomerArea), ContactLoggConstants.ContactType,ContactTypeConstants.Meeting),
                GetFieldOption(nameof(CustomerArea), ContactLoggConstants.ContactType,ContactTypeConstants.TeamsChat),
                GetFieldOption(nameof(CustomerArea), ContactLoggConstants.ContactType,ContactTypeConstants.Phone),

                GetFieldOption(nameof(CustomerArea), ContactLoggConstants.ContactStatus,ContactStatusConstants.Initiated),
                GetFieldOption(nameof(CustomerArea), ContactLoggConstants.ContactStatus,ContactStatusConstants.OnGoing),
                GetFieldOption(nameof(CustomerArea), ContactLoggConstants.ContactStatus,ContactStatusConstants.Completed),


                GetFieldOption(nameof(WebsiteArea),PilotProcessorConstants.PilotButtonNames, PilotProcessorConstants.NewWorkItem),

            };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonLinks, PilotProcessorConstants.PilotButtonNames),
                GetMultiFieldChange(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonLinks, DataViewFieldNameConstants.ButtonPagePointer),
                GetMultiFieldChange(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonLinks, DataViewFieldNameConstants.UseConfirmation),
                GetMultiFieldChange(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonLinks, DataViewFieldNameConstants.ConfirmationText),
                GetMultiFieldChange(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonLinks, DataViewFieldNameConstants.ButtonOpenInModal),
                GetMultiFieldChange(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonLinks, DataViewFieldNameConstants.EndPointMethod),
                GetMultiFieldChange(nameof(WebsiteArea), PilotProcessorConstants.PilotButtonLinks, DataViewFieldNameConstants.FieldTooltipMessage),
                GetMultiFieldChange(nameof(WebsiteArea), PilotFieldNameConstants.AddOns,  PilotFieldNameConstants.AddOn),
                GetMultiFieldChange(nameof(WebsiteArea), PilotFieldNameConstants.AddOns,  PilotFieldNameConstants.AddOnStatus),
                GetMultiFieldChange(nameof(WebsiteArea), PilotFieldNameConstants.AddOns,  PilotFieldNameConstants.OrderedDate),
                GetMultiFieldChange(nameof(WebsiteArea), PilotFieldNameConstants.AddOns,  PilotFieldNameConstants.OrderedBy),
                GetMultiFieldChange(nameof(WebsiteArea), PilotFieldNameConstants.AddOns,  PilotFieldNameConstants.ImplementedDate),
                GetMultiFieldChange(nameof(WebsiteArea), ContactLoggConstants.ContactLogg,  ContactLoggConstants.ContactDateTime),
                GetMultiFieldChange(nameof(WebsiteArea), ContactLoggConstants.ContactLogg,  ContactLoggConstants.ContactType),
                GetMultiFieldChange(nameof(WebsiteArea), ContactLoggConstants.ContactLogg,  ContactLoggConstants.ContactStatus),
                GetMultiFieldChange(nameof(WebsiteArea), ContactLoggConstants.ContactLogg,  ContactLoggConstants.InvolvedPersons),
                GetMultiFieldChange(nameof(WebsiteArea), ContactLoggConstants.ContactLogg,  ContactLoggConstants.Title),
                GetMultiFieldChange(nameof(WebsiteArea), ContactLoggConstants.ContactLogg,  ContactLoggConstants.Description),


            };

            UpdateMultiFieldField(changes);
        }
    }
}
