import { genericGridView } from './Reducers/GenericGridView.reducer';
import { fieldConfiguration } from './Reducers/FieldConfiguration.reducer';
import { genericGridForm } from './Reducers/GenericGridForm.reducer';
import { quotaQuickSearch } from './_Solution/Reducers/QuotaQuickSearch.reducer';
import { quotaArticleFields } from './_Solution/Reducers/QuotaArticleFields.reducer';

export const pnGenericGridView = {
    genericGridView,
    fieldConfiguration,
    genericGridForm,
    quotaQuickSearch,
    quotaArticleFields
};