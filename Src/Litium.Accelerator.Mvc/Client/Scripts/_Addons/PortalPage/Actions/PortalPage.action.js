import { get, post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';

import { PORTAL_PAGE_LOAD, PORTAL_PAGE_ERROR } from '../constants';

const rootRoute = '/api/portalpage/';

export const getPortalPageData = (pageSystemId) => (dispatch, getState) => {
    let pagenationActive = false;
   
    dispatch(toggleGenericLoader(true));
    return get(rootRoute + `getGetPortalPageData?pageSystemId=` + pageSystemId)
        .then((response) => response.json())
        .then((response) => dispatch(fillPortalPageStructure(response)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const loadError = (error) => (dispatch, getState) => {
  dispatch(toggleGenericLoader(false));
  return dispatch({
      type: PORTAL_PAGE_ERROR,
    payload: {
      error,
    },
  });
};

export const fillPortalPageStructure = (response) => (dispatch, getState) => {
   dispatch(toggleGenericLoader(false));
   
   return dispatch({
       type: PORTAL_PAGE_LOAD,
    payload: {
        pageStructure: response,
    },
  });
};
