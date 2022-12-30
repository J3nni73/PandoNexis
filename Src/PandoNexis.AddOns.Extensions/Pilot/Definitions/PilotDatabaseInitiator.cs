using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using PandoNexis.Accelerator.Extensions.Database.Services;
using PandoNexis.AddOns.Extensions.Pilot.Objects;
using PandoNexis.AddOns.Extensions.Pilot.Services;
using PandoNexis.AddOns.Extensions.Wishlist.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Pilot.Definitions
{
    internal class PilotDatabaseInitiator : DatabaseInitiator
    {
        private readonly PilotItemDALService _pilotItemDALService;
        public PilotDatabaseInitiator(IConfiguration configuration, PilotItemDALService pilotItemDALService) : base(configuration)
        {
            _pilotItemDALService = pilotItemDALService;
        }

        public override void GetCheckDatabaseObjects()
        {
            SyncronizeDatabaseObjects(PilotConstants.Item, GetItemColumns());
            SyncronizeDatabaseObjects(PilotConstants.Time, GetTimeColumns());

            //var test = new Item()
            //{
            //    SystemId = Guid.NewGuid(),
            //    OrganizationSystemId = Guid.NewGuid(),
            //    ParentSystemId = Guid.NewGuid(),
            //    ItemTitle = "Första aktiviteten",
            //    ItemDescription= "Description",
            //    ItemStatus = "Teststatus",
            //    ItemType = "Testtyp",
            //    CreatedDateTime= DateTime.Now,
            //    CreatedBy = Guid.NewGuid(),
            //    UpdatedDateTime= DateTime.Now,  
            //    UpdatedBy = Guid.NewGuid(),
            //};
            //_pilotItemDALService.AddOrUpdateItem(test);

            //test = new Item()
            //{
            //    SystemId = Guid.NewGuid(),
            //    OrganizationSystemId = Guid.NewGuid(),
            //    ParentSystemId = Guid.NewGuid(),
            //    ItemTitle = "Andra aktiviteten",
            //    ItemDescription = "Description",
            //    ItemStatus = "Teststatus",
            //    ItemType = "Testtyp",
            //    CreatedDateTime = DateTime.Now,
            //    CreatedBy = Guid.NewGuid(),
            //    UpdatedDateTime = DateTime.Now,
            //    UpdatedBy = Guid.NewGuid(),
            //};
            //_pilotItemDALService.AddOrUpdateItem(test);
            //test = new Item()
            //{
            //    SystemId = Guid.NewGuid(),
            //    OrganizationSystemId = Guid.NewGuid(),
            //    ParentSystemId = Guid.NewGuid(),
            //    ItemTitle = "Tredje aktiviteten",
            //    ItemDescription = "Description",
            //    ItemStatus = "Teststatus",
            //    ItemType = "Testtyp",
            //    CreatedDateTime = DateTime.Now,
            //    CreatedBy = Guid.NewGuid(),
            //    UpdatedDateTime = DateTime.Now,
            //    UpdatedBy = Guid.NewGuid(),
            //};
            //_pilotItemDALService.AddOrUpdateItem(test);

        }
        public List<DatabaseColumns> GetItemColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.OrganizationSystemId, DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.ParentSystemId, DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.ItemType, DatabaseConstants.Varchar50, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.ItemStatus, DatabaseConstants.Varchar50, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.ItemTitle, DatabaseConstants.Varchar100, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.ItemDescription, DatabaseConstants.VarcharMax, DatabaseConstants.Null),
                GetColumn(PilotConstants.DueDateTime, DatabaseConstants.DateTime, DatabaseConstants.Null)
            };
            result.AddRange(GetEditedColumns());
            return result;
        }
        public List<DatabaseColumns> GetTimeColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.ItemSystemId, DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.OrganizationSystemId, DatabaseConstants.UniqueIdentifier, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.TimeType, DatabaseConstants.Varchar50, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.TimeComment, DatabaseConstants.VarcharMax, DatabaseConstants.Null),
                GetColumn(PilotConstants.TimeFrom, DatabaseConstants.DateTime, DatabaseConstants.Null),
                GetColumn(PilotConstants.TimeTo, DatabaseConstants.DateTime, DatabaseConstants.Null),
                GetColumn(PilotConstants.TimeAmount, DatabaseConstants.Int, DatabaseConstants.NotNull),
                GetColumn(PilotConstants.TimeRisk, DatabaseConstants.Int, DatabaseConstants.Null),
            };
            result.AddRange(GetEditedColumns());
            return result;
        }
    }
}
