using System.Collections.Generic;
using System.Drawing;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Products;

namespace Solution.Extensions.Definitions.Products
{
    internal class ProductsFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QuantityListDeliveryDates, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QuotaId, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QuotaFormField, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.ProductImageUpload, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.Quota, SystemFieldTypeConstants.MultiField)
                {
                    Option = new MultiFieldOption { IsArray = true, Fields = new List<string>(){ PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QuotaId } }
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QuotaSearchItem, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField)
                {
                    Option = new MultiFieldOption { IsArray = true, Fields = new List<string>(){ PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QuotaId } }
                },


                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.SupplierItemNumber, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.Description, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.Packing, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.UnitPriceUSD, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QtyInn, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.QtyCtn, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.ItemNumber, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.CtnCbm, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.ItemSizeLength, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.ItemSizeWidth  , SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.ItemSizeHeight, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.CtnMeasureLength, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.CtnMeasureWidth, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.CtnMeasureHeight, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.GWKgs, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.NWKgs, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.CubicFoot, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField) // Kubikfot*35,325
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.Shipping, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField) //CFT(Kubikfot) formel*Frakt( 90kr/CFT)/(QTY/CTN(PCS))
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.ExchangeRate, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField) // Unit Price USD*Valuta kurs
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.ShippingAndExchangeRate, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField) // Summan av frakt + summa valuta
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.Cost, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField) // Summan valuta+frakt*1,05 Kostnad landat pris
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                  new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.FOB, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                  },
                    new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.DDP, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                    },
                      new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.DIST, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.FOBRev, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                    new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.DDPRev, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                    },
                      new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.DISTRev, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.SellingPriceBP, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField) // Summa Tull formel*1,25 Påslag 25% standard
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.StoreX3, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.FieldTypesConstants.SpecialField) // Försäljningspris BP*3
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.RevSalesPrice, SystemFieldTypeConstants.Decimal) // Försäljningspris BP*3
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.BPArticleNumber, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                 new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.CreatedDate, SystemFieldTypeConstants.DateTime)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = false,
                    MultiCulture = false,
                 },
                  new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.ProductFieldNameConstants.UpdatedDate, SystemFieldTypeConstants.DateTime)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = false,
                    MultiCulture = false,
                },
            };



            return fields;
        }
    }

}
