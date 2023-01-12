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
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QuantityListDeliveryDates, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QuotaId, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QuotaFormField, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.ProductImageUpload, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.Quota, SystemFieldTypeConstants.MultiField)
                {
                    Option = new MultiFieldOption { IsArray = true, Fields = new List<string>(){ PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QuotaId } }
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QuotaSearchItem, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField)
                {
                    Option = new MultiFieldOption { IsArray = true, Fields = new List<string>(){ PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QuotaId } }
                },


                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.SupplierItemNumber, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.Description, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.Packing, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.UnitPriceUSD, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QtyInn, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.QtyCtn, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.ItemNumber, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.CtnCbm, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.ItemSizeLength, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.ItemSizeWidth  , SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.ItemSizeHeight, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.CtnMeasureLength, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.CtnMeasureWidth, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.CtnMeasureHeight, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.GWKgs, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.NWKgs, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.CubicFoot, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField) // Kubikfot*35,325
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.Shipping, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField) //CFT(Kubikfot) formel*Frakt( 90kr/CFT)/(QTY/CTN(PCS))
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.ExchangeRate, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField) // Unit Price USD*Valuta kurs
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.ShippingAndExchangeRate, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField) // Summan av frakt + summa valuta
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.Cost, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField) // Summan valuta+frakt*1,05 Kostnad landat pris
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                  new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.FOB, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                  },
                    new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.DDP, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                    },
                      new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.DIST, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.FOBRev, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                    new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.DDPRev, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                    },
                      new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.DISTRev, SystemFieldTypeConstants.Decimal)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.SellingPriceBP, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField) // Summa Tull formel*1,25 Påslag 25% standard
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.StoreX3, PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_FieldTypesConstants.SpecialField) // Försäljningspris BP*3
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },

                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.RevSalesPrice, SystemFieldTypeConstants.Decimal) // Försäljningspris BP*3
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.BPArticleNumber, SystemFieldTypeConstants.Text)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                    MultiCulture = false,
                },
                 new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.CreatedDate, SystemFieldTypeConstants.DateTime)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = false,
                    MultiCulture = false,
                 },
                  new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.PNGenericGridView.Constants.GenericGridView_ProductFieldNameConstants.UpdatedDate, SystemFieldTypeConstants.DateTime)
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
