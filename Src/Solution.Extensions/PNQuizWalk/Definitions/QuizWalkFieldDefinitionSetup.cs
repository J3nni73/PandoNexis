using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using Solution.Extensions.PNQuizWalk.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Definitions
{
    internal class QuizWalkFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<WebsiteArea>(QuizWalkConstants.QuizWalkButtonsNames, SystemFieldTypeConstants.TextOption)
                {
                      Option = new TextOption
                      {
                        MultiSelect = false,
                        Items = new List<TextOption.Item>()
                      }
                },

                new FieldDefinition<WebsiteArea>(QuizWalkConstants.QuizWalkButtons, SystemFieldTypeConstants.MultiField)
                {
                    MultiCulture = false,
                     Option = new MultiFieldOption
                     {
                         IsArray = true,
                         Fields  = new List<string>()
                     }
                },
            };
            return fields;
        }
    }
}
