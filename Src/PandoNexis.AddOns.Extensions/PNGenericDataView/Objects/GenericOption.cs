using Litium.Web.Administration.WebApi.Common.EditorHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericOption
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; } = false;
    }
}
