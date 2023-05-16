using Newtonsoft.Json.Serialization;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericDataViewResponse
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string EntitySystemId { get; set; }
        public int ContainerIndex { get; set; }
        public Dictionary<string, object> Form { get; set; }
        public string FieldId { get; set; }
        public string DataSource { get; set; }
    }
}
