using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Constants
{
    public static class DataFieldTypes
    {
    
        public const string StringDGType = "string";
        public const string BooleanDGType = "checkbox";
        public const string MultirowDGType = "textarea";
        public const string DateTimeDGType = "datetime";
        public const string DecimalDGType = "decimal";
        public const string TextAreaDGType = "textarea";
    }
    public static class DataFieldFormats
    {
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm";
        public const string TimeFormat = "0.##";
    }
    
}
