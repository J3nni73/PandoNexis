import { post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleLoader } from './GenericGridView.action';

import {
    DROPZONE_SUBMIT_ERROR, GENERIC_GRID_VIEW_RECEIVE, GENERIC_GRID_VIEW_LOAD_ERROR, ORDER_SCANNING_RECEIVE
} from '../constants';

export const uploadDropZoneFile = (file, type) => (dispatch, getState) => {
  const rootRoute = '/api/genericgridview/';
  const readerMTL = new FileReader();

  dispatch(toggleLoader(true));

  // Closure to capture the file information.
  readerMTL.readAsDataURL(file);
  readerMTL.onload = function () {
    return post(rootRoute + type, readerMTL.result)
      .then((response) => response.json())
      .then((response) => {
        dispatch(receive(response.gridView));
        dispatch(receiveOrderScanning(response.orderScanning));
        dispatch(toggleLoader(false));
      })
      .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
  };
};

export const loadError = (error) => ({
  type: GENERIC_GRID_VIEW_LOAD_ERROR,
  payload: {
    error,
  },
});

export const receive = (data) => {
  return {
    type: GENERIC_GRID_VIEW_RECEIVE,
    payload: data,
  };
};

export const receiveOrderScanning = (data) => {
  return {
    type: ORDER_SCANNING_RECEIVE,
    payload: data,
  };
};
