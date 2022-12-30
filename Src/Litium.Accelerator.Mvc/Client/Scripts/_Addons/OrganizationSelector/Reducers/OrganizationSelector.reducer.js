import { ORGANIZATION_SEARCH_QUERY, ORGANIZATION_SEARCH_RECEIVE, ORGANIZATION_SEARCH_SHOW_FULL_FORM, ORGANIZATION_SEARCH_SELECT_ITEM, ORGANIZATION_SEARCH_TOGGLE_AUTOCOMPLETE, ORGANIZATION_SET_ORG_LIST, ORGANIZATION_LOAD, ORGANIZATION_PERSON_INFO } from '../constants';

const DEFAULT_STATE = {
    query: '',
    result: [],
    showResult: false,
    showFullForm: false,
    selectedItem: -1,
    organizationList: [],
    personInfo: {},
    selectedOrg: { id: -1, name: '', isSelected: false, },
};

export const organizationSelector = (state = DEFAULT_STATE, action) => {
    switch (action.type) {
        case ORGANIZATION_SEARCH_QUERY:
        case ORGANIZATION_SEARCH_RECEIVE:
        case ORGANIZATION_SEARCH_SHOW_FULL_FORM:
        case ORGANIZATION_SEARCH_SELECT_ITEM:
        case ORGANIZATION_SEARCH_TOGGLE_AUTOCOMPLETE:
        case ORGANIZATION_SET_ORG_LIST:
        case ORGANIZATION_LOAD:
        case ORGANIZATION_PERSON_INFO:

            return {
                ...state,
                ...action.payload,
            }
        default:
            return state;
    }
}