import { pnCollectionPage } from './PNCollectionPage/reducers';
import { pnLoggedOnInfoLabel } from './PNLoggedOnInfoLabel/reducers';
import { pnOrganizationSelector } from './PNOrganizationSelector/reducers';


export const addonReducers = {
    ...pnCollectionPage,
    ...pnLoggedOnInfoLabel,
    ...pnOrganizationSelector,
};
