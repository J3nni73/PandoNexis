import {
    FIELD_CONFIGURATOR_RECEIVE, FIELD_CONFIGURATOR_UPDATE_SELECTED_FIELDS
} from '../constants';

const DEFAULT_STATE = {
    fields: [],
    selectedFields: [],
    showInfo: false
};

export const fieldConfiguration = (state = DEFAULT_STATE, action) => {
    switch (action.type) {
        case FIELD_CONFIGURATOR_RECEIVE:
        case FIELD_CONFIGURATOR_UPDATE_SELECTED_FIELDS:
            return {
                ...state,
                ...action.payload,
            }
        default:
            return state;
    }
}