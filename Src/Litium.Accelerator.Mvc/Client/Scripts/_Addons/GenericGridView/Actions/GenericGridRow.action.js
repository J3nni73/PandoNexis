import { patch } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import {
  toggleLoader,
  receiveGenericGridViewTabs,
  getHeaderInformationData,
} from './GenericGridView.action';

import {
    GENERIC_GRID_ROW_UPDATE, GENERIC_GRID_ROW_ERROR, GENERIC_GRID_ROW_RECEIVE, CART_RECEIVE
} from '../constants';

const rootRoute = '/api/genericgridview/';

export const update = (type, data, fields) => (dispatch, getState) => {
  dispatch({
    type: GENERIC_GRID_ROW_UPDATE,
    payload: { data, fields },
  });
  // console.log('Update Row --', type, data, fields);
  return patch(rootRoute + type, data)
    .then((response) => response.json())
    .then((response) => dispatch(checkRowResponse(response, fields)))
    .catch((ex) =>
      dispatch(catchError(ex, (error) => updateError(error, fields)))
    );
};
export const checkRowResponse = (response, fields) => (dispatch, getState) => {
  // Update header data
  dispatch(getHeaderInformationData());

  // If processor return tabs
  if (response.gridViewTabs) {
    dispatch(receiveGenericGridViewTabs(response.gridViewTabs));
  }

  if (!response.cart && !response.row) {
    return dispatch(receive(response, fields));
  } else {
    if (response.cart) {
      dispatch(receiveCart(response.cart));
    }
    if (response.row) {
      dispatch(receive(response.row, fields));
    }
    return true;
  }
};

export const loadError = (error) => ({
  type: GENERIC_GRID_ROW_ERROR,
  payload: {
    error,
  },
});

export const receiveCart = (cart) => ({
  type: CART_RECEIVE,
  payload: cart,
});

export const updateError = (error, fields) => ({
  type: GENERIC_GRID_ROW_ERROR,
  payload: { error, fields },
});

export const receive = (response, fields) => {
  return {
    type: GENERIC_GRID_ROW_RECEIVE,
    payload: { response, fields },
  };
};
