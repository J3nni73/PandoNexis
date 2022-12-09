import { post, get } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';

import {
    FIELD_CONFIGURATOR_RECEIVE, FIELD_CONFIGURATOR_ERROR, FIELD_CONFIGURATOR_UPDATE_SELECTED_FIELDS
} from '../constants';

const rootRoute = '/api/fieldconfiguration/';

export const load = (type) => (dispatch, getState) => {
  get(rootRoute + type)
    .then((response) => response.json())
    .then((fields) => dispatch(receive(fields)))
    .catch((ex) => dispatch(catchError(ex, (error) => searchError(error))));
};

export const receive = (result) => ({
  type: FIELD_CONFIGURATOR_RECEIVE,
  payload: {
    fields: result.fields,
    selectedFields: result.selectedFields,
  },
});

export const addField = (item, type) => (dispatch, getState) => {
  let selectedFields = [...getState().fieldConfiguration.selectedFields];
  selectedFields.push(item);

  return post(rootRoute + type, selectedFields)
    .then((response) => dispatch(storeUpdateSelectedFields(selectedFields)))
    .catch((ex) => dispatch(catchError(ex, (error) => searchError(error))));
};

export const removeField = (fieldIndex, type) => (dispatch, getState) => {
  let selectedFields = [...getState().fieldConfiguration.selectedFields];
  selectedFields.splice(fieldIndex, 1);

  return post(rootRoute + type, selectedFields)
    .then((response) => dispatch(storeUpdateSelectedFields(selectedFields)))
    .catch((ex) => dispatch(catchError(ex, (error) => searchError(error))));
};

export const storeUpdateSelectedFields = (selectedFields) => {
  return {
    type: FIELD_CONFIGURATOR_UPDATE_SELECTED_FIELDS,
    payload: {
      selectedFields,
    },
  };
};

export const searchError = (error) => ({
  type: FIELD_CONFIGURATOR_ERROR,
  payload: {
    error,
  },
});
