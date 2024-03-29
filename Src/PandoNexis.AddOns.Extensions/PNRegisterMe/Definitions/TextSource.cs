﻿using Litium.Accelerator.Definitions.WebsiteTexts;

namespace PandoNexis.AddOns.Extensions.PNRegisterMe.Definitions
{
    public class NoErpTextSource : IWebsiteTextSource
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
                "registerme.headertexts.saveperson".AsWebsiteTextDefinition("Save person", "Spara person", clientAvailable: true, serverAvailable: true),
                "registerme.headertexts.checkcode".AsWebsiteTextDefinition("Validate code", "Kontrollera kod", clientAvailable: true, serverAvailable: true),
                
            };
        }
    }
}
