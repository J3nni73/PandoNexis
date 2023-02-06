using Microsoft.Extensions.Configuration;
using PandoNexis.Accelerator.Extensions.Database.Constants;
using PandoNexis.Accelerator.Extensions.Database.Objects;
using PandoNexis.Accelerator.Extensions.Database.Services;
using Solution.Extensions.PNPilot.Constants;
using Solution.Extensions.PNPilot.Services.DALServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Definitions
{
    public class PilotDatabaseInitiator : DatabaseInitiator
    {
        public PilotDatabaseInitiator(IConfiguration configuration) : base(configuration)
        {
        }

        public override void GetCheckDatabaseObjects()
        {
            SyncronizeDatabaseObjects(PilotConstants.Item, GetItemColumns());
            SyncronizeDatabaseObjects(PilotConstants.ItemType, GetItemTypeColumns());
            SyncronizeDatabaseObjects(PilotConstants.ItemStatus, GetItemStatusColumns());

            SyncronizeDatabaseObjects(PilotConstants.Time, GetTimeColumns());
            SyncronizeDatabaseObjects(PilotConstants.TimeType, GetTimeTypeColumns());
            SyncronizeDatabaseObjects(PilotConstants.TimeStatus, GetTimeStatusColumns());



        }
        public List<DatabaseColumns> GetTimeTypeColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(PilotConstants.Name, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.Description, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),

            };
            return result;
        }
        public List<DatabaseColumns> GetTimeStatusColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(PilotConstants.TimeTypeSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.Name, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.Description, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),

            };
            return result;
        }

        public List<DatabaseColumns> GetItemTypeColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(PilotConstants.Name, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.Description, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),

            };
            return result;
        }
        public List<DatabaseColumns> GetItemStatusColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(PilotConstants.ItemTypeSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.Name, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.Description, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),

            };
            return result;
        }

        public List<DatabaseColumns> GetItemColumns()
        {
            var result = new List<DatabaseColumns>
            {
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(PilotConstants.OrganizationSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ParentSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.Id, DatabaseTypeConstants.Varchar50, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ItemTypeSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.ItemStatusSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
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
                GetColumn(PilotConstants.SystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull, true),
                GetColumn(PilotConstants.ItemSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.OrganizationSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.TimeTypeSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.TimeStatusSystemId, DatabaseTypeConstants.UniqueIdentifier, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.TimeComment, DatabaseTypeConstants.VarcharMax, DatabaseTypeConstants.Null),
                GetColumn(PilotConstants.TimeFrom, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null),
                GetColumn(PilotConstants.TimeTo, DatabaseTypeConstants.DateTime, DatabaseTypeConstants.Null),
                GetColumn(PilotConstants.TimeAmount, DatabaseTypeConstants.Int, DatabaseTypeConstants.NotNull),
                GetColumn(PilotConstants.TimeRisk, DatabaseTypeConstants.Decimal, DatabaseTypeConstants.Null),
            };
            result.AddRange(GetEditedColumns());
            return result;
        }
    }
}
