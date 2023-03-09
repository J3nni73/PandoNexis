import { PORTAL_PAGE_LOAD, PORTAL_PAGE_ERROR } from '../constants';

const DEFAULT_STATE = {
    fileStructure: {},
    currentStructureIndex: [],
    showInfo: false
};

export const portalPage = (state = DEFAULT_STATE, action) => {
    
    switch (action.type) {
        case PORTAL_PAGE_LOAD:
        case PORTAL_PAGE_ERROR:
            return {
                ...state,
                ...action.payload,
            }
        default:
            return state;
    }
} 