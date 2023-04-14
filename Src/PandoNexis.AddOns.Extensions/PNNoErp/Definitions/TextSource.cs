using Litium.Accelerator.Definitions.WebsiteTexts;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Definitions
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
                "noerp.headertexts.ordernumber".AsWebsiteTextDefinition("Order number", "Ordernumber", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.orderdate".AsWebsiteTextDefinition("Order date", "Orderdatum", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.orderstate".AsWebsiteTextDefinition("Order state", "Orderstatus", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.ordertype".AsWebsiteTextDefinition("Order type", "Ordertyp", clientAvailable: true, serverAvailable: true),

                "noerp.headertexts.orderrowsamount".AsWebsiteTextDefinition("Amount of rows", "Antal rader", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.ordertotal".AsWebsiteTextDefinition("Order ordertotal", "Ordertotal", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.organzation".AsWebsiteTextDefinition("Customer", "Kund", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.customerinfo".AsWebsiteTextDefinition("Customer information", "Kundinformation", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.firstname".AsWebsiteTextDefinition("First name", "Förnamn", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.lastname".AsWebsiteTextDefinition("Last name", "Efternamn", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.acceptorder".AsWebsiteTextDefinition("Order received", "Order emottagen", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.createshipment".AsWebsiteTextDefinition("Create shipment", "Skapa leverans", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.notifyshippmentdelivered".AsWebsiteTextDefinition("Deliver", "Leverera", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.finalizeorder".AsWebsiteTextDefinition("Cancel", "Avbryt", clientAvailable: true, serverAvailable: true),

                "noerp.headertexts.vieworder".AsWebsiteTextDefinition("View order", "Visa order", clientAvailable: true, serverAvailable: true),


                "noerp.headertexts.shipmentstate".AsWebsiteTextDefinition("Shipment state", "Leveransstatus", clientAvailable: true, serverAvailable: true),
                "noerp.headertexts.paymentstate".AsWebsiteTextDefinition("Payment state", "Betalstatus", clientAvailable: true, serverAvailable: true),

            };
        }
    }
}
