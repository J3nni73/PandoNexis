import { PNGENERICDATAVIEW_LOAD, PNGENERICDATAVIEW_DISPLAY_STATE, PNGENERICDATAVIEW_ERROR } from '../constants';

const DEFAULT_STATE = {
    query: '',
    data: [],
    show: false, 
    showFullForm: false,
    selectedItem: -1,
};

export const pnGenericDataViewReducer = (state = DEFAULT_STATE, action) => {
    switch (action.type) {
        case PNGENERICDATAVIEW_LOAD:
        case PNGENERICDATAVIEW_DISPLAY_STATE:
        case PNGENERICDATAVIEW_ERROR:

            return {
                ...state,
                ...action.payload,
            }
        default:
            return state;
    }
}