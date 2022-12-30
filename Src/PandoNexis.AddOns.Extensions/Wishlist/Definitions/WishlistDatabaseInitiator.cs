using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using PandoNexis.Accelerator.Extensions.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Wishlist.Definitions
{
    internal class WishlistDatabaseInitiator : DatabaseInitiator
    {
        public WishlistDatabaseInitiator(IConfiguration configuration) : base(configuration)
        {
        }

        public override void GetCheckDatabaseObjects()
        {
            SyncronizeDatabaseObjects(WishlistConstants.WishList, GetWishListColumns());
            SyncronizeDatabaseObjects(WishlistConstants.WishListItem, GetWishListItemColumns());
        }
        private List<DatabaseColumns> GetWishListColumns()
        {
            return new List<DatabaseColumns>
            {
                GetColumn(WishlistConstants.CookieId,DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(WishlistConstants.LastUpdated, DatabaseConstants.DateTime, DatabaseConstants.NotNull),
            };
        }
        private List<DatabaseColumns> GetWishListItemColumns()
        {
            return new List<DatabaseColumns>
            {
                GetColumn(WishlistConstants.BaseProductSystemId,DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(WishlistConstants.VariantSystemId, DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(WishlistConstants.AddedDate, DatabaseConstants.DateTime, DatabaseConstants.NotNull),
            };
        }
    }
}
