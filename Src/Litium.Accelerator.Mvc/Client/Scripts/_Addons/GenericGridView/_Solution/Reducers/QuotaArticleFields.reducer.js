import {
    GENERIC_GRID_FIELD_SET_QUOTA_SEARCH_LIST,
    GENERIC_GRID_FIELD_SET_QUOTA_EDIT,
    GENERIC_GRID_FIELD_SET_QUOTA_EXISTING,
    GENERIC_GRID_FIELD_SET_CURRENT_QUOTA,
   } from '../constants';


import {
    GENERIC_GRID_FIELD_SET_ARTICLES_LIST,
    GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    GENERIC_GRID_FIELD_RESET_STATE,
    SET_BUTTON_INPROGRESS
} from '../../constants';

const DEFAULT_STATE = {
  articlesList: [],
  existingArticlesList: [],
  quotaSearchList: [],
  isInEditMode: false,
  isExistingQuota: false,
  isLoading: false,
  currentQuery: '',
  inProgress: [],
};

export const quotaArticleFields = (
  state = DEFAULT_STATE,
  { type, payload }
) => {
  switch (type) {
    case SET_BUTTON_INPROGRESS:
    case GENERIC_GRID_FIELD_SET_ARTICLES_LIST:
    case GENERIC_GRID_FIELD_SET_CURRENT_QUOTA:
    case GENERIC_GRID_FIELD_SET_QUOTA_SEARCH_LIST:
    case GENERIC_GRID_FIELD_SET_QUOTA_EDIT:
    case GENERIC_GRID_FIELD_SET_QUOTA_EXISTING:
    case GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST:
    case GENERIC_GRID_FIELD_RESET_STATE:
      return {
        ...state,
        ...payload,
      };

    default:
      return state;
  }
};
