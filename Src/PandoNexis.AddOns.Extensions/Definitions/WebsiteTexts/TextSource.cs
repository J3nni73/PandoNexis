namespace PandoNexis.AddOns.Extensions.Definitions.WebsiteTexts
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
                // Media catalog
                "mediacatalog.fileinfo.name".AsWebsiteTextDefinition("Name", "Namn", clientAvailable: true, serverAvailable: false),
                "mediacatalog.fileinfo.width".AsWebsiteTextDefinition("Width", "Bredd", clientAvailable: true, serverAvailable: false),
                "mediacatalog.fileinfo.height".AsWebsiteTextDefinition("Height", "Höjd", clientAvailable: true, serverAvailable: false),
                "mediacatalog.fileinfo.description".AsWebsiteTextDefinition("Description", "Beskrivning", clientAvailable: true, serverAvailable: false),
                "mediacatalog.fileinfo.filesize".AsWebsiteTextDefinition("File size", "Filstorlek", clientAvailable: true, serverAvailable: false),
                "mediacatalog.fileinfo.fileextension".AsWebsiteTextDefinition("File extension", "Filändelse", clientAvailable: true, serverAvailable: false),
                "mediacatalog.nofilesfound".AsWebsiteTextDefinition("No files found ", "Inga filer i katalogen", clientAvailable: true, serverAvailable: false),
                "mediacatalog.info".AsWebsiteTextDefinition("Info", "Info", clientAvailable: true, serverAvailable: false),
                "mediacatalog.filesinfolder".AsWebsiteTextDefinition("Files in folder", "Filter i mapp", clientAvailable: true, serverAvailable: false),
                "mediacatalog.button.search".AsWebsiteTextDefinition("Search", "Sök", clientAvailable: true, serverAvailable: false),
                "mediacatalog.removeactivesearch".AsWebsiteTextDefinition("Remove active search filter", "Ta bort aktivt sökfilter", clientAvailable: true, serverAvailable: false),
                "mediacatalog.searchforfilestext".AsWebsiteTextDefinition("Search for files in all subfolders", "Sök efter filer i alla kataloger", clientAvailable: true, serverAvailable: false),
                "mediacatalog.button.download".AsWebsiteTextDefinition("Download", "Ladda ner", clientAvailable: true, serverAvailable: false),
                "mediacatalog.button.folderopen".AsWebsiteTextDefinition("Open Folder", "Öppen mapp", clientAvailable: true, serverAvailable: false),
                "mediacatalog.button.folder".AsWebsiteTextDefinition("Folder", "Mapp", clientAvailable: true, serverAvailable: false),
                "mediacatalog.button.stepoutoffolder".AsWebsiteTextDefinition("Step out of this folder", "Lämna denna katalog", clientAvailable: true, serverAvailable: false),
                
                // Generic grid view
                "genericgridview.loadingtext".AsWebsiteTextDefinition("Loading", "Laddar", clientAvailable: true, serverAvailable: false),
                "genericgridview.exportbutton.text".AsWebsiteTextDefinition("Export", "Exportera", clientAvailable: true, serverAvailable: false),
                "genericgridview.noresult".AsWebsiteTextDefinition("No result", "Inget resultat", clientAvailable: true, serverAvailable: false),
                "genericgridview.field.dropdown.chooseoptiontext".AsWebsiteTextDefinition("Choose", "Välj", clientAvailable: true, serverAvailable: false),
                "genericgridview.notloggedon.title".AsWebsiteTextDefinition("Not logged on", "Inte inloggad", clientAvailable: true, serverAvailable: false),
                "genericgridview.notloggedon.description".AsWebsiteTextDefinition("You must be logged", "Inloggning krävs", clientAvailable: true, serverAvailable: false),
                
                // Generic grid view buttons
                 "genericgridview.form.button.open.purchase".AsWebsiteTextDefinition("Open settings", "Öppna inställningar", clientAvailable: true, serverAvailable: false),
                 "genericgridview.form.button.open.quota".AsWebsiteTextDefinition("Open settings", "Öppna inställningar", clientAvailable: true, serverAvailable: false),
                 "genericgridview.form.button.open.orderhistory".AsWebsiteTextDefinition("Open settings", "Öppna inställningar", clientAvailable: true, serverAvailable: false),
                 "genericgridview.form.button.open.quickbuy".AsWebsiteTextDefinition("Open settings", "Öppna inställningar", clientAvailable: true, serverAvailable: false),

                // Generic grid view - FORM
                "genericgridview.form.button.open.quickbuy".AsWebsiteTextDefinition("Quick buy settings", "Inställningar för Quick buy", clientAvailable: true, serverAvailable: false),
            };
        }
    }
}
