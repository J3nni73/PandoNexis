import { get, post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';

import {
    GENERIC_GRID_FORM_SUBMITBUTTON, GENERIC_GRID_FORM_LOADING, GENERIC_GRID_FORM_RECEIVE, GENERIC_GRID_FORM_ERROR
} from '../constants';

const rootRoute = '/api/genericgridview/';
//const rootRouteTodo = '/api/quotagridview/';

export const getGenericGridForm = (gridType) => (dispatch) => {
  const type = gridType;
  dispatch(loadingToggleActionForm(true));
  return get(rootRoute + 'getGenericGridForm/' + type)
    .then((response) => response.json())
    .then((result) => {
      dispatch(receive(result));
    })
    .catch((ex) => dispatch(catchError(ex, (error) => setError(error))));
};

// Todo Check this one if not use
export const addGGFData = (type, data, fields) => (dispatch, getState) => {
  // dispatch({
  //   type: GENERIC_GRID_ROW_UPDATE,
  //   payload: { data, fields },
  // });
  console.log('form ', type, data);
  return post(rootRoute + 'createFromForm/' + type, data)
    .then((response) => response.json())
    .then((response) => {
      console.log('Add GGF data ', response);
      // dispatch(checkRowResponse(response, fields));
    })
    .catch((ex) =>
      // add update Error if needed
      dispatch(catchError(ex, (error) => updateError(error, fields)))
    );
};

const receive = (dataRows) => ({
  type: GENERIC_GRID_FORM_RECEIVE,
  payload: {
    dataRows,
  },
});

export const setError = (error) => ({
  type: GENERIC_GRID_FORM_ERROR,
  payload: {
    error,
  },
});
export const changeSubmitBtnActivity = (bol) => ({
  type: GENERIC_GRID_FORM_SUBMITBUTTON,
  payload: {
    btnIsActive: bol,
  },
});

export const loadingToggleActionForm = (isLoading = false) => ({
  type: GENERIC_GRID_FORM_LOADING,
  payload: {
    isLoading
  }
})