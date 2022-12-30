// Link in all MAIN addon reducers here

import { pnMediaCatalog } from './MediaCatalog/reducers';
import { pnCollectionPage } from './CollectionPage/reducers';
import { pnGenericGridView } from './GenericGridView/reducers';
import { pnOrganizationSelector } from './OrganizationSelector/reducers';
import { pnLoggedOnInfoLabel } from './LoggedOnInfoLabel/reducers';

export const addonReducers = {
    ...pnMediaCatalog,  
    ...pnCollectionPage,
    ...pnGenericGridView,
    ...pnOrganizationSelector,
    ...pnLoggedOnInfoLabel
};


