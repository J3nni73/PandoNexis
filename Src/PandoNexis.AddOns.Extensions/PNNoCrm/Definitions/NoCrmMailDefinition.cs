using Litium.Accelerator.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Definitions
{
    public class NoCrmMailDefinition : HtmlMailDefinition
    {
        private string _body = string.Empty;
        private string _subject = string.Empty;
        private string _toEmail = string.Empty;
        private Guid _channelSystemId = Guid.Empty;

        public NoCrmMailDefinition(string toEmail, string subject, string body, Guid channelSystemId) 
        { 
            _toEmail = toEmail;
            _subject = subject;
            _body = body;
            _channelSystemId = channelSystemId;
        }
        public override string Body => _body;

        public override Guid ChannelSystemId => _channelSystemId;

        public override string Subject => _subject;

        public override string ToEmail => _toEmail;
    }
}
