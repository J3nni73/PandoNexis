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
            SyncronizeDatabaseObjects(PilotConstants.ItemFieldData, GetFieldDataColumns());

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
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.OrganizationSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ParentSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ItemType, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ItemStatus, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ItemTitle, DatabaseTypeConstants.Varchar100, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ItemDescription, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),
                GetColumn(PilotConstants.DueDateTime, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null)
            };
            result.AddRange(GetEditedColumns());
            return result;
        }
        public List<DatabaseColumns> GetTimeColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ItemSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.OrganizationSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.TimeType, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.TimeComment, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),
                GetColumn(PilotConstants.TimeFrom, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null),
                GetColumn(PilotConstants.TimeTo, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null),
                GetColumn(PilotConstants.TimeAmount, DatabaseTypeConstants.Int, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.TimeRisk, DatabaseTypeConstants.Int, DatabaseTypeConstants.Null),
            };
            result.AddRange(GetEditedColumns());
            return result;
        }       
    }
}
