import {
    GENERIC_SEARCH_QUERY,
    GENERIC_SEARCH_RECEIVE,
    GENERIC_SEARCH_SHOW_FULL_FORM,
    GENERIC_SEARCH_SELECT_ITEM,
    GENERIC_SEARCH_TOGGLE_SHOW_RESULT,
    GENERIC_SEARCH_QUERY_GRIDTYPE,
    GENERIC_SEARCH_RESET_STATE,
} from '../../constants';


const DEFAULT_STATE = {
  gridType: '',
  query: '',
  result: [],
  showResult: false,
  showFullForm: false,
  selectedItem: -1,
};

export const quotaQuickSearch = (state = DEFAULT_STATE, action) => {
  switch (action.type) {
    case GENERIC_SEARCH_QUERY_GRIDTYPE:
    case GENERIC_SEARCH_QUERY:
    case GENERIC_SEARCH_RECEIVE:
    case GENERIC_SEARCH_SHOW_FULL_FORM:
    case GENERIC_SEARCH_SELECT_ITEM:
    case GENERIC_SEARCH_TOGGLE_SHOW_RESULT:
    case GENERIC_SEARCH_RESET_STATE:
      return {
        ...state,
        ...action.payload,
      };
    default:
      return state;
  }
};
