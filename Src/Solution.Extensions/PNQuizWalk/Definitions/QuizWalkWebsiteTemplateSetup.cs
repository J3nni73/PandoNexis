using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using Solution.Extensions.PNQuizWalk.Constants;

namespace PandoNexis.AddOns.Extensions.PNQuizWalk.Definitions
{
    internal class AcceleratorWebsiteTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetWebsiteField("AcceleratorWebsite", QuizWalkConstants.QuizWalkButtons, QuizWalkConstants.QuizWalkButtons),

            };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
           return null;
        }
    }
}
