using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using PandoNexis.Accelerator.Extensions.Database.Services;

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
                GetColumn(WishlistConstants.CookieId,DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(WishlistConstants.LastUpdated, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.NotNull),
            };
        }
        private List<DatabaseColumns> GetWishListItemColumns()
        {
            return new List<DatabaseColumns>
            {
                GetColumn(WishlistConstants.BaseProductSystemId,DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(WishlistConstants.VariantSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(WishlistConstants.AddedDate, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.NotNull),
            };
        }
    }
}
