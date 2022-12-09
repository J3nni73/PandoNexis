import { get, post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';

import { COLLECTION_PAGE_LOAD, COLLECTION_PAGE_ERROR } from '../constants';

const rootRoute = '/api/collectionpage/';

export const getCollectionPageData = (pageSystemId) => (dispatch, getState) => {
    let pagenationActive = false;
    dispatch(toggleGenericLoader(true));
    return get(rootRoute + `getGetCollectionPageData?pageSystemId=` + pageSystemId)
        .then((response) => response.json())
        .then((response) => dispatch(fillCollectionPageStructure(response)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const loadError = (error) => (dispatch, getState) => {
  dispatch(toggleGenericLoader(false));
  return dispatch({
      type: COLLECTION_PAGE_ERROR,
    payload: {
      error,
    },
  });
};

export const fillCollectionPageStructure = (response) => (dispatch, getState) => {
   dispatch(toggleGenericLoader(false));
   
   return dispatch({
      type: COLLECTION_PAGE_LOAD,
    payload: {
        fileStructure: response,
    },
  });
};
