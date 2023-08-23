using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using Solution.Extensions.PNQuizWalk.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNQuizWalk.Definitions
{
    public class QuizWalkFieldHeliper : FieldHelper
    {
        public QuizWalkFieldHeliper(FieldDefinitionService fieldDefinitionService, 
                                    SecurityContextService securityContextService, 
                                    LanguageService languageService) : base(fieldDefinitionService, securityContextService, languageService)
        {
        }

        public override void HandleFieldOptions()
        {
            var changes = new List<FieldOptionChanges>()
            {
                GetFieldOption(nameof(WebsiteArea), DataViewFieldNameConstants.DataArea, QuizWalkConstants.QuizWalk),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkAdmin),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkView),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkAddNew),
                GetFieldOption(nameof(WebsiteArea),DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkChat),

                GetFieldOption(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtonsNames, QuizWalkConstants.QuizWalkAddNew),
                GetFieldOption(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtonsNames, QuizWalkConstants.QuizWalkMoveNext),
            };

            UpdateFieldOptions(changes);
        }

        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtons, QuizWalkConstants.QuizWalkButtonsNames),
                GetMultiFieldChange(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.ButtonPagePointer),
                GetMultiFieldChange(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.UseConfirmation),
                GetMultiFieldChange(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.ConfirmationText),
                GetMultiFieldChange(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.ButtonOpenInModal),
                GetMultiFieldChange(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.EndPointMethod),
                GetMultiFieldChange(nameof(WebsiteArea), QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.FieldTooltipMessage),
            };

            UpdateMultiFieldField(changes);
        }
    }
}
