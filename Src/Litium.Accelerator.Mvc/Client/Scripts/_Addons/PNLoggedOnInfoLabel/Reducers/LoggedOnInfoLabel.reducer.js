import { LOGGED_ON_INFO_LABEL_LOAD, LOGGED_ON_INFO_LABEL_ERROR } from '../constants';

const DEFAULT_STATE = {
    personInfo: {},
};

export const loggedOnInfoLabel = (state = DEFAULT_STATE, action) => {
    switch (action.type) {
        case LOGGED_ON_INFO_LABEL_LOAD:
        case LOGGED_ON_INFO_LABEL_ERROR:

            return {
                ...state,
                ...action.payload,
            }
        default:
            return state;
    }
}