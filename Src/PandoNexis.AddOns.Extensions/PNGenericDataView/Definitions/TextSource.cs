using Litium.Accelerator.Definitions.WebsiteTexts;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Definitions
{
    public class TextSource : IWebsiteTextSource
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
                
                // Generic grid view
                "genericdataview.loadingtext".AsWebsiteTextDefinition("Loading", "Laddar", clientAvailable: true, serverAvailable: false),
                "genericdataview.exportbutton.text".AsWebsiteTextDefinition("Export", "Exportera", clientAvailable: true, serverAvailable: false),
                "genericdataview.noresult".AsWebsiteTextDefinition("No result", "Inget resultat", clientAvailable: true, serverAvailable: false),
                "genericdataview.field.dropdown.chooseoptiontext".AsWebsiteTextDefinition("Choose", "Välj", clientAvailable: true, serverAvailable: false),
                "genericdataview.notloggedon.title".AsWebsiteTextDefinition("Not logged on", "Inte inloggad", clientAvailable: true, serverAvailable: false),
                "genericdataview.notloggedon.description".AsWebsiteTextDefinition("You must be logged", "Inloggning krävs", clientAvailable: true, serverAvailable: false),
                
                // Generic grid view - FORM
                "genericdataview.form.button.open.quickbuy".AsWebsiteTextDefinition("Quick buy settings", "Inställningar för Quick buy", clientAvailable: true, serverAvailable: false),
            };
        }
    }
}
