import { post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';

import {
    DROPZONE_SUBMIT_ERROR, GENERIC_DATA_VIEW_RECEIVE, GENERIC_DATA_VIEW_LOAD_ERROR, ORDER_SCANNING_RECEIVE,
    GENERIC_MODAL_DATA_VIEW_RECEIVE
} from '../constants';

export const uploadDropZoneFile = (file, pageSystemId, isInModal=false) => (dispatch, getState) => {
  const rootRoute = '/api/genericdataview/';
  const readerMTL = new FileReader();

  dispatch(toggleGenericLoader(true));

  // Closure to capture the file information.
  readerMTL.readAsDataURL(file);
  readerMTL.onload = function () {
      return post(rootRoute + pageSystemId, readerMTL.result)
      .then((response) => response.json())
      .then((response) => {
        dispatch(receive(response.dataView, isInModal));
        dispatch(receiveOrderScanning(response.orderScanning));
        dispatch(toggleGenericLoader(false));
      })
      .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
  };
};

export const loadError = (error) => ({
  type: GENERIC_DATA_VIEW_LOAD_ERROR,
  payload: {
    error,
  },
});

export const receive = (data, isInModal=false) => {
  return {
      type: isInModal ? GENERIC_MODAL_DATA_VIEW_RECEIVE: GENERIC_DATA_VIEW_RECEIVE,
    payload: data,
  };
};

export const receiveOrderScanning = (data) => {
  return {
    type: ORDER_SCANNING_RECEIVE,
    payload: data,
  };
};
