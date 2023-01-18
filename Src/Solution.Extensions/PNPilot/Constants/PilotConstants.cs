using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Constants
{
    public static class PilotConstants
    {
        //public const string PilotCustomer = "PilotCustomer";
        public const string Item = "Item";
        public const string ItemFieldData = "ItemFieldData";

        public const string SystemId = "SystemId";
        public const string OrganizationSystemId = "OrganizationSystemId";
        public const string ItemType = "ItemType";
        public const string ItemTitle = "ItemTitle";
        public const string ItemStatus = "ItemStatus";
        public const string ItemDescription = "ItemDescription";
        public const string ParentSystemId = "ParentSystemId";

        public const string DueDateTime = "DueDateTime";

        public const string Time = "Time";

        public const string ItemSystemId = "ItemSystemId";
        public const string TimeType = "TimeType";
        public const string TimeComment = "TimeComment";
        public const string TimeFrom = "TimeFrom";
        public const string TimeTo = "TimeTo";
        public const string TimeAmount = "TimeAmount";
        public const string TimeRisk = "TimeRisk";

        public const string ItemProcessor = "ItemProcessor";
    }
    public static class PilotFieldTemplateConstants
    {
        public const string PilotCustomer = "PilotCustomer";

        public const string PilotProject = "PilotProject";

    }
    public static class PilotFieldGroupConstants
    {
        public const string Addon = "PilotCustomer";

    }
    public static class PilotFieldNameConstants
    {
        public const string ProjectType = "ProjectType";
        public const string Customer = "Customer";
        public const string ErpId = "ErpId";
        public const string AddOns = "AddOns";
        public const string AddOn = "AddOn";
        public const string AddOnStatus = "AddOnStatus";
        public const string OrderedDate = "OrderedDate";
        public const string OrderedBy = "OrderedBy";
        public const string ImplementedDate = "ImplementedDate";

    }
    public static class ProjectTypeConstants
    {
        public const string PandoNexisAccelerator = "PandoNexisAccelerator";
        public const string LitiumAccelerator = "LitiumAccelerator";
        public const string SubContractor = "SubContractor";

    }
    public static class AddonStatusConstants
    {
        public const string Intended = "Intended";
        public const string Ordered = "Ordered";
        public const string Implemented = "Implemented";
        public const string Disconnected = "Disconnected";

    }

}
