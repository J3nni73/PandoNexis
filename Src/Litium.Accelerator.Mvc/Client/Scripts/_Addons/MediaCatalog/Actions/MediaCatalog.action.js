import { get, post } from '../../../Services/http';
import { getURLSearchParams } from '../../../_PandoNexis/Services/url';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';

import { MEDIA_CATALOG_LOAD, MEDIA_CATALOG_ERROR } from '../constants';

const rootRoute = '/api/mediacatalog/';

export const getMediaCatalogData = (mediaFolderId) => (dispatch, getState) => {

    let pagenationActive = false;
    dispatch(toggleGenericLoader(true));

    return get(rootRoute + `getMediaData?folderId=` + mediaFolderId)
        .then((response) => response.json())
        .then((response) => dispatch(fillMediaStructure(response)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};


export const loadError = (error) => (dispatch, getState) => {
    dispatch(toggleGenericLoader(false));
    return dispatch({
        type: MEDIA_CATALOG_LOAD,
        payload: {
            error,
        },
    });
};

export const fillMediaStructure = (response) => (dispatch) =>{
    dispatch(toggleGenericLoader(false));
    return dispatch({
        type: MEDIA_CATALOG_LOAD,
        payload: {
            fileStructure: response,
        },
    });
};


export const clearRows = () => {
    return {
        type: MEDIA_CATALOG_LOAD,
        payload: {
            dataRows: [],
        },
    };
};

export const updateAllRows = (rows) => {
    return {
        type: MEDIA_CATALOG_LOAD,
        payload: {
            dataRows: rows,
        },
    };
};
