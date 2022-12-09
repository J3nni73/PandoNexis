// Link in all MAIN addon reducers here

import { pnMediaCatalog } from './MediaCatalog/reducers';
import { pnCollectionPage } from './CollectionPage/reducers';
import { pnGenericGridView } from './GenericGridView/reducers';

export const addonReducers = {
    ...pnMediaCatalog,  
    ...pnCollectionPage,
    ...pnGenericGridView,
};


