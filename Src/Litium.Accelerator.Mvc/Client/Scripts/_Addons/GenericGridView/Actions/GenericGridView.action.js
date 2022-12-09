import { get, post } from '../../../Services/http';
import { getURLSearchParams } from '../../../_PandoNexis/Services/url';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';

import {
    GENERIC_GRID_VIEW_LOAD, GENERIC_GRID_VIEW_LOAD_ERROR, GENERIC_GRID_VIEW_RECEIVE, GENERIC_GRID_VIEW_SET_DATA_TYPE, GENERIC_GRID_VIEW_SET_NOT_LOGGED_ON,
    GENERIC_GRID_VIEW_ALL_ROWS, GENERIC_GRID_VIEW_SET_SEARCH_PENDING_STATUS, GENERIC_GRID_VIEW_UPDATE_CART_TABS,
    GENERIC_GRID_VIEW_GET_HEADER_INFORMATION_DATA, GENERIC_GRID_UPDATE_FIELDS
} from '../constants';

const rootRoute = '/api/genericgridview/';

export const categoryById = (type, data, settings) => (dispatch, getState) => {
  let pagenationActive = false;
  dispatch(toggleLoader(true));

  const searchParams = getURLSearchParams();
  if (!settings) {
    settings = getURLSearchParams();
  } else {
    pagenationActive = !!settings.page;

    let removePaging = false;
    if (!settings.page) {
      removePaging = true;
    }
    settings = { ...searchParams, ...settings };
    if (removePaging) {
      settings.page = 1;
    }
  }

  dispatch({
    type: GENERIC_GRID_VIEW_SET_DATA_TYPE,
    payload: {
      currentDataType: type,
    },
  });

  dispatch({
    type: GENERIC_GRID_VIEW_LOAD,
    payload: { type, settings },
  });

  const params = new URLSearchParams();
  Object.entries(settings).forEach(([key, values]) => {
    if (Array.isArray(values)) {
      values.forEach((value) => {
        if (value) {
          params.append(key, value);
        }
      });
    } else if (values) {
      params.append(key, values);
    }
  });

  return post(rootRoute + `handleFormData/${type}`, data)
    .then((response) => response.json())
    .then((response) => dispatch(checkResponse(response)))
    .then((response) => dispatch(checkPaging(response, pagenationActive)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};


export const getMediaFolderData = (mediaFolderId) => (dispatch, getState) => {
 //   alert("Action-id = " + mediaFolderId);

    let pagenationActive = false;
    //dispatch(toggleLoader(true));


    return get(rootRoute + `getGenericGridViewMedia?folderId=` + mediaFolderId)
        .then((response) => response.json())
        .then((response) => dispatch(checkResponse(response)))
        .then((response) => dispatch(checkPaging(response, pagenationActive)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const load = (type, settings) => (dispatch, getState) => {
  let pagenationActive = false;
  dispatch(toggleLoader(true));

  const searchParams = getURLSearchParams();
  if (!settings) {
    settings = getURLSearchParams();
  } else {
    pagenationActive = !!settings.page;

    let removePaging = false;
    if (!settings.page) {
      removePaging = true;
    }
    settings = { ...searchParams, ...settings };
    if (removePaging) {
      settings.page = 1;
    }
  }

  dispatch({
    type: GENERIC_GRID_VIEW_SET_DATA_TYPE,
    payload: {
      currentDataType: type,
    },
  });

  dispatch({
    type: GENERIC_GRID_VIEW_LOAD,
    payload: { type, settings },
  });

  const params = new URLSearchParams();
  Object.entries(settings).forEach(([key, values]) => {
    if (Array.isArray(values)) {
      values.forEach((value) => {
        if (value) {
          params.append(key, value);
        }
      });
    } else if (values) {
      params.append(key, values);
    }
  });

  return get(rootRoute + `getGenericGridView/${type}?${params}`)
    .then((response) => response.json())
    .then((response) => dispatch(checkResponse(response)))
    .then((response) => dispatch(checkPaging(response, pagenationActive)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

// Maybe not used any more.. We get tabs from DataSource processor
export const getGenericGridTabs = () => (dispatch, getState) => {
  return get('/api/cart/getDeliveryCartTabs')
    .then((response) => response.json())
    .then((cartTabs) => dispatch(receiveGenericGridViewTabs(cartTabs)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const getGenericGridViewForExport = () => (dispatch, getState) => {
  dispatch(toggleLoader(true));
  const type = getState().genericGridView.currentDataType;
  return get(rootRoute + 'getGenericGridViewForExport/' + type)
    .then((response) => response.json())
    .then((data) => {
      const fileName = type + '.xlsx';

      const linkSource = `data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,${data.base64}`;
      const downloadLink = document.createElement('a');

      downloadLink.href = linkSource;
      downloadLink.download = fileName;

      document.body.appendChild(downloadLink);

      downloadLink.click();
      downloadLink.remove();
    })
    .then((response) => dispatch(toggleLoader(false)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const getGenericGridByTab = (id) => (dispatch, getState) => {
  dispatch(toggleLoader(true));
  const type = getState().genericGridView.currentDataType;
  return get(rootRoute + `getGenericGridViewByTab/${type}?id=${id}`)
    .then((response) => response.json())
    .then((response) => dispatch(clearAndUpdate(response)))
    .then((response) => dispatch(toggleLoader(false)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

/// headerInformation
export const getHeaderInformationData = () => (dispatch, getState) => {
  let type = getState().genericGridView.currentDataType;
  if (!type || type.length < 1) {
    type = 'void';
  }
  return get(rootRoute + `getHeaderInformation/${type}`)
    .then((response) => response.json())
    .then((response) => dispatch(setHeaderInformation(response)))
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const toggleLoader = (visible) => (dispatch, getState) => {
  dispatch({
    type: GENERIC_GRID_VIEW_SET_SEARCH_PENDING_STATUS,
    payload: {
      isSearching: visible,
    },
  });

  dispatch(toggleGenericLoader(visible));
  return;
};

export const checkPaging = (response, isPaging) => (dispatch, getState) => {
  if (!response) return null;
  if (isPaging) {
    window.scrollTo(0, 0);
    document.scrollingElement.scrollTop = 0;
  }
  dispatch(toggleLoader(false));
  return response;
};
export const checkResponse = (response) => (dispatch, getState) => {
  if (response.result) {
    response = response.result;
  }
  console.log('Check Response ', response);
  if (!response) {
    dispatch(toggleLoader(false));
    return null;
  }
  // Clear Grid view before adding data - due to strange bug //
  dispatch(clearRows());
  dispatch(getHeaderInformationData());
  // Set gridViewHasTabs to true if response contains gridViewTabs
  if (response.gridViewTabs) {
    response.gridViewHasTabs = true;
  }

  dispatch(clearRows());

  dispatch(toggleLoader(false));
  if (response.isNotLoggedOn) {
    // We use reversed condition due to object maybe not found
    return dispatch({
      type: GENERIC_GRID_VIEW_SET_NOT_LOGGED_ON,
      payload: {
        isNotLoggedOn: response.isNotLoggedOn,
      },
    });
  } else {
    return dispatch(receive(response));
  }
};

// Called from GenericGridRow.action
export const receiveGenericGridViewTabs = (cartTabs) => (
  dispatch,
  getState
) => {
  return dispatch({
    type: GENERIC_GRID_VIEW_UPDATE_CART_TABS,
    payload: {
      gridViewTabs: cartTabs,
    },
  });
};

export const setHeaderInformation = (headerInfo) => (dispatch, getState) => {
    if (headerInfo && headerInfo.result) {
        const headerComponents = document.querySelector('.header__components');
        if (headerComponents) {
            headerComponents.classList.add('header__components--with-info');
        }
    }
  return dispatch({
    type: GENERIC_GRID_VIEW_GET_HEADER_INFORMATION_DATA,
    payload: {
      headerInformation: headerInfo,
    },
  });
};

export const loadError = (error) => (dispatch, getState) => {
  dispatch(toggleLoader(false));
  return dispatch({
    type: GENERIC_GRID_VIEW_LOAD_ERROR,
    payload: {
      error,
    },
  });
};

export const clearAndUpdate = (response) => (dispatch, getState) => {
  dispatch(clearRows());
  dispatch(receive(response));
  //if (response.dataRows) {
  //    dispatch({
  //        type: GENERIC_GRID_VIEW_ALL_ROWS,
  //        payload: {
  //            dataRows: response.dataRows
  //        }
  //    });
  //}
  return true;
};

export const clearRows = () => {
  return {
    type: GENERIC_GRID_VIEW_ALL_ROWS,
    payload: {
      dataRows: [],
    },
  };
};

export const updateAllRows = (rows) => {
  return {
    type: GENERIC_GRID_VIEW_ALL_ROWS,
    payload: {
      dataRows: rows,
    },
  };
};

export const receive = (data) => {
  return {
    type: GENERIC_GRID_VIEW_RECEIVE,
    payload: data,
  };
};


export const updateFielsInGrid = (rows) => {
  return {
    type: GENERIC_GRID_UPDATE_FIELDS,
    payload: {
      dataRows: rows,
    },
  };
};