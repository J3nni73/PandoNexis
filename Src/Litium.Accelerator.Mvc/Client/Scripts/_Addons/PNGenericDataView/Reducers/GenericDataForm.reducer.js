import {
    GENERIC_DATA_FORM_LOADING,
    GENERIC_DATA_FORM_RECEIVE,
    GENERIC_DATA_FORM_ERROR,
    GENERIC_DATA_FORM_SUBMITBUTTON,
} from '../constants';

import { error as errorReducer } from '../../../Reducers/Error.reducer';

const defaultState = {
  dataContainers: [],
  errors: {},
  isLoading: false,
  btnIsActive: false,
};

export const genericDataForm = (state = defaultState, action) => {
  const { type, payload } = action;
  switch (type) {
    case GENERIC_DATA_FORM_LOADING:
      return {
        ...state,
        errors: {},
        isLoading: true,
      };
    case GENERIC_DATA_FORM_SUBMITBUTTON:
    case GENERIC_DATA_FORM_RECEIVE:
      return {
        ...state,
        errors: {},
        isLoading: false,
        ...payload,
      };
    case GENERIC_DATA_FORM_ERROR:
      return {
        ...state,
        isLoading: false,
        errors: errorReducer(state.errors, action),
      };
    default:
      return state;
  }
};
