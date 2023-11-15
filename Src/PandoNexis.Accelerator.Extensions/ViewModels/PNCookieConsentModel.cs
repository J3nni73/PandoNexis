using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.ViewModel
{
    public class PNCookieConsentModel
    {
        public bool EnableFeatures { get; set; } = true;
        public bool EnableAnalytics { get; set; } = true;
        public bool EnableMarketing { get; set; } = true;
    }
}
