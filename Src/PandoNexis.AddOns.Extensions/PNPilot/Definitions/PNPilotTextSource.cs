using Litium.Accelerator.Definitions.WebsiteTexts;

namespace PandoNexis.AddOns.Extensions.PNPilot.Definitions
{
    public class PNPilotTextSource : IWebsiteTextSource
    {
        public string Prefix => "addons";

        public bool UpdateExistingTexts => false;

        public List<WebsiteTextDefinition> GetTexts()
        {
            // Text that goes on all websites as a server text:
            // = MyCompanyWebsite.MyFirstText

            // Text that is generated both as server available AND also generates a client (js.-prefix) version of the text:
            // = MyCompanyWebsite.MyClientServerText AND js.mycompanywebsite.myclientservertext
            //"MyClientServerText".AsWebsiteTextDefinition("My client and server text", "Min client and server text", clientAvaliable: true),

            // Text that is generated as client available only:
            // js.mycompanywebsite.myclienttext
            //"MyClientText".AsWebsiteTextDefinition("My client text", "Min client text", clientAvaliable: true, serverAvaliable: false),

            // Text that is only created on specific websites
            //var b2BSiteIds = new List<Guid> { new Guid("dd6cca3f-da66-43c3-b062-663762f9e77f") };
            //"MySingleSiteText".AsWebsiteTextDefinition("My single site text", "Min client text", b2BSiteIds)
            return new List<WebsiteTextDefinition>
            {
                
                // NoErp-orders
                "pilot.headertexts.workitemid".AsWebsiteTextDefinition("Id", "Id", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.itemtitle".AsWebsiteTextDefinition("Title", "Titel", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.itemdescription".AsWebsiteTextDefinition("Description", "Beskrivning", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.duedate".AsWebsiteTextDefinition("Due date", "Förfallodatum", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.createddate".AsWebsiteTextDefinition("Created date", "Skapad datum", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.updateddate".AsWebsiteTextDefinition("Updated date", "Uppdaterad datum", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.createdby".AsWebsiteTextDefinition("Created by", "Skapad av", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.updatedby".AsWebsiteTextDefinition("Updated by", "Uppdaterad av", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.itemstatus".AsWebsiteTextDefinition("Status", "Status", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.itemtype".AsWebsiteTextDefinition("Type", "Typ", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.estimate".AsWebsiteTextDefinition("Estimate", "Estimat", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.estimaterisk".AsWebsiteTextDefinition("Risk", "Risk", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.estimatecomment".AsWebsiteTextDefinition("Comment", "Kommentar", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.sumtimespent".AsWebsiteTextDefinition("TimeSpent", "Nedlagd tid", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.addtimespent".AsWebsiteTextDefinition("Add timeSpent", "Lägg till nedlagd tid", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.addtimespentfrom".AsWebsiteTextDefinition("From", "Från", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.addtimespentto".AsWebsiteTextDefinition("To", "Till", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.addtimespentcomment".AsWebsiteTextDefinition("Comment", "Kommentar", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.customer".AsWebsiteTextDefinition("Customer", "Kund", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.assigned".AsWebsiteTextDefinition("Assigned", "Tilldelat till", clientAvailable: true, serverAvailable: true),
                "pilot.headertexts.reportedby".AsWebsiteTextDefinition("Reported by", "Rapporterad av", clientAvailable: true, serverAvailable: true),

            };
        }
    }
}
