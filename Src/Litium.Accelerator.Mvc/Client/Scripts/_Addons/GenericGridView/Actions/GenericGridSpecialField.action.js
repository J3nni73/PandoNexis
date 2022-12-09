import { get, patch, post, put, remove } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import {
  receiveCart,
  receive,
  checkRowResponse,
} from './GenericGridRow.action';
import {
  toggleLoader,
  checkResponse,
  getHeaderInformationData,
} from './GenericGridView.action';
import 'abortcontroller-polyfill/dist/abortcontroller-polyfill-only';

import {
    GENERIC_GRID_FIELD_LOAD, GENERIC_GRID_FIELD_LOAD_ERROR, GENERIC_GRID_FIELD_RECEIVE, GENERIC_GRID_FIELD_LIST_RECEIVE,
    GENERIC_GRID_FIELD_ROW_UPDATE, GENERIC_GRID_FIELD_GET_DATA_LIST, GENERIC_GRID_FIELD_GET_ORGANIZATION_LIST
} from '../constants';

const rootRoute = '/api/genericgridview/';

let abortController;

export const getOrganizationData = (entitySystemId) => (dispatch, getState) => {
  if (entitySystemId) {
    const currentDataType = getState().genericGridView.currentDataType;
    abortController && abortController.abort();
    abortController = new AbortController();
    dispatch(toggleLoader(true));
    return post(
      rootRoute +
        'getFieldData/' +
        currentDataType +
        '/OrganizationSelection/' +
        entitySystemId,
      '',
      abortController
    )
      .then((response) => response.json())
      .then((response) => dispatch(receiveOrganizationList(response)))
      .then((response) => dispatch(toggleLoader(false)))
      .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
  } else {
    dispatch(receiveOrganizationList(null));
  }
};

export const autocompleteGetList = (fieldId, query) => (dispatch, getState) => {
  const currentDataType = getState().genericGridView.currentDataType;
  //if (!query || query.length < 3) {
  //    return;
  //}

  abortController && abortController.abort();
  abortController = new AbortController();
  dispatch(toggleLoader(true));
  return post(
    rootRoute +
      'getFieldData/' +
      currentDataType +
      '/' +
      fieldId +
      '/' +
      encodeURI(query),
    '',
    abortController
  )
    .then((response) => response.json())
    .then((response) => dispatch(receiveList(response)))
    .then((response) => dispatch(toggleLoader(false)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const setFieldData = (fieldId, rowIndex, setValueObject) => (
  dispatch,
  getState
) => {
  const fields = getState().genericGridView.dataRows[rowIndex].fields;
  const data = {
    entitySystemId: fields.entitySystemId,
  };

  dispatch({
    type: GENERIC_GRID_ROW_UPDATE,
    payload: { data, fields },
  });

  const currentDataType = getState().genericGridView.currentDataType;
  setValueObject.fieldId = fieldId;

  setValueObject.dataSource = currentDataType;
  dispatch(toggleLoader(true));
  return put(rootRoute + 'setFieldData', setValueObject)
    .then((response) => response.json())
    .then((response) => dispatch(UpdateDataRow(response, fields)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const loadError = (error) => ({
  type: GENERIC_GRID_FIELD_LOAD_ERROR,
  payload: {
    error,
  },
});

export const receiveList = (data) => {
  return {
    type: GENERIC_GRID_FIELD_GET_DATA_LIST,
    payload: {
      currentAutocompleteList: data, //currentAutocompleteList: [{ value: 1, text: 2 }, { value: 11, name: 22 }, { value: 1, name: 2 }, { value: 11, name: 22 }]
    },
  };
};

export const receiveOrganizationList = (data) => {
  return {
    type: GENERIC_GRID_FIELD_GET_ORGANIZATION_LIST,
    payload: {
      organizations: data,
    },
  };
};

export const UpdateDataRow = (response, fields) => (dispatch, getState) => {
  dispatch(toggleLoader(false));

  // If ony one row is to be updated
  if (response.row) {
    return dispatch(checkRowResponse(response, fields));
  }

  // Update header data
  dispatch(getHeaderInformationData());

  // If whole grid is updated
  return dispatch(checkResponse(response));

  //if (response.cart){
  //    dispatch(receiveCart(response.cart));
  //}
  //dispatch(receive(response.row, fields));
};
