using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
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
                GetFieldOption(FieldHelperConstants.WebsiteArea, DataViewFieldNameConstants.DataArea, QuizWalkConstants.QuizWalk),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkAdmin),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkView),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkAddNew),
                GetFieldOption(FieldHelperConstants.WebsiteArea,DataViewFieldNameConstants.AreaSource, QuizWalkConstants.QuizWalkChat),

                GetFieldOption(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtonsNames, QuizWalkConstants.QuizWalkAddNew),
                GetFieldOption(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtonsNames, QuizWalkConstants.QuizWalkMoveNext),
            };

            UpdateFieldOptions(changes);
        }

        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtons, QuizWalkConstants.QuizWalkButtonsNames),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.ButtonPagePointer),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.UseConfirmation),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.ConfirmationText),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.ButtonOpenInModal),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.EndPointMethod),
                GetMultiFieldChange(FieldHelperConstants.WebsiteArea, QuizWalkConstants.QuizWalkButtons, DataViewFieldNameConstants.FieldTooltipMessage),
            };

            UpdateMultiFieldField(changes);
        }
    }
}
