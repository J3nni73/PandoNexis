import { pnCollectionPage } from './PNCollectionPage/reducers';
import { pnGenericDataView } from './PNGenericDataView/reducers';
import { pnLoggedOnInfoLabel } from './PNLoggedOnInfoLabel/reducers';
import { pnOrganizationSelector } from './PNOrganizationSelector/reducers';


export const addonReducers = {
    ...pnCollectionPage,
    ...pnGenericDataView,
    ...pnLoggedOnInfoLabel,
    ...pnOrganizationSelector,
};
