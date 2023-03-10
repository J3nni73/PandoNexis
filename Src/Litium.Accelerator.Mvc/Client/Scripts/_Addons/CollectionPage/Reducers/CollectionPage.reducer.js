import { COLLECTION_PAGE_LOAD, COLLECTION_PAGE_ERROR } from '../constants';

const DEFAULT_STATE = {
    pageStructure: {},
    currentStructureIndex: [],
    showInfo: false
};

export const collectionPage = (state = DEFAULT_STATE, action) => {
    
    switch (action.type) {
        case COLLECTION_PAGE_LOAD:
        case COLLECTION_PAGE_ERROR:
            return {
                ...state,
                ...action.payload,
            }
        default:
            return state;
    }
} 