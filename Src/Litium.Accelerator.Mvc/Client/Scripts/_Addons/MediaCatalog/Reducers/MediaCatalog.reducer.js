import { MEDIA_CATALOG_LOAD, MEDIA_CATALOG_ERROR } from '../constants';

const DEFAULT_STATE = {
    fileStructure: {},
    currentStructureIndex: [],
    showInfo: false
};

export const mediaCatalog = (state = DEFAULT_STATE, action) => {
    switch (action.type) {
        case MEDIA_CATALOG_LOAD:
        case MEDIA_CATALOG_ERROR:
            return {
                ...state,
                ...action.payload,
            }
        default:
            return state;
    }
}