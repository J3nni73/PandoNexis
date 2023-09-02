import { get, post } from '../../../Services/http';
import { getURLSearchParams } from '../../../_PandoNexis/Services/url';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';
import {
    GENERIC_DATA_VIEW_LOAD, GENERIC_DATA_VIEW_LOAD_ERROR, GENERIC_DATA_VIEW_RECEIVE, GENERIC_DATA_VIEW_SET_DATA_TYPE, GENERIC_DATA_VIEW_SET_NOT_LOGGED_ON,
    GENERIC_DATA_VIEW_ALL_ROWS, GENERIC_DATA_VIEW_SET_SEARCH_PENDING_STATUS, GENERIC_DATA_VIEW_UPDATE_CART_TABS,
    GENERIC_DATA_VIEW_GET_HEADER_INFORMATION_DATA, GENERIC_DATA_UPDATE_FIELDS,
    GENERIC_DATA_VIEW_SHOW_MODAL, GENERIC_MODAL_DATA_UPDATE_FIELDS, GENERIC_MODAL_DATA_VIEW_ALL_ROWS, GENERIC_MODAL_DATA_VIEW_RECEIVE, GENERIC_MODAL_DATA_VIEW_INIT,
    GENERIC_DATA_VIEW_SET_CURRENT_PAGE_ID
} from '../constants';
//import mockdata from '../mockdataKanban.json';// _jennifer.json';
//import mockdata from '../mockdata4.json';// '../mockdataForm.json';
const rootRoute = '/api/genericdataview/';
const genericLoaderType = "spinner"; // spinner or ripple

export const toggleModal = (forceClose=false) => (dispatch, getState) => {
    const pnModal = getState().genericDataView.pnModal;
    const bodyEl = document.querySelector('body');
    if (bodyEl) {
        if (forceClose || pnModal.open) {
            bodyEl.classList.remove('modal--open');
        }
        else {
            bodyEl.classList.add('modal--open');            
        }
    }
    dispatch({
        type: GENERIC_DATA_VIEW_SHOW_MODAL,
        payload: { pnModal: { open: forceClose? false : !pnModal.open, index: pnModal.index } },
    });
};


export const getMediaFolderData = (mediaFolderId) => (dispatch, getState) => {
    //  alert("Action-id = " + mediaFolderId);

    let pagenationActive = false;
    //dispatch(toggleLoader(true));
    
    return get(rootRoute + `getGenericDataViewMedia?folderId=` + mediaFolderId)
        .then((response) => response.json())
        .then((response) => dispatch(checkResponse(response)))
        .then((response) => dispatch(checkPaging(response, pagenationActive)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const load = (pageId, settings, isInModal=false, entitySystemId='') => (dispatch, getState) => {
    ////alert(entitySystemId);
    let pagenationActive = false;
    dispatch(toggleLoader(true));    
    if (!isInModal) {      
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
            type: GENERIC_DATA_VIEW_SET_CURRENT_PAGE_ID,
            payload: {
                currentPageId: pageId,
            },
        });
        dispatch({
            type: GENERIC_DATA_VIEW_LOAD,
            payload: { currentPageId: pageId, settings },
        });
    }
   
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

    if (entitySystemId) {
        params.append("entitySystemId", entitySystemId);
    }
    
    ////ASPEN MOCKUP
    //if (mockdata) {
    //    const response = mockdata; // isInModal ? mockdata2 : mockdata;

    //    dispatch(checkResponse(response, isInModal));
    //    dispatch(checkPaging(response, pagenationActive));
    //    return;
    //}
    
    return get(rootRoute + `getGenericDataView/${pageId}?${params}`)
        .then((response) => response.json())
        .then((response) => dispatch(checkResponse(response, isInModal)))
        .then((response) => dispatch(checkPaging(response, pagenationActive)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const loadModal = (modalSettings) => (dispatch, getState) => {

    if (modalSettings?.fromTopLevelDataContainerIndex!=null) {
        window.currGenDW_dataContainerIndex = modalSettings?.fromTopLevelDataContainerIndex;
    }

    // Set modal settings
    dispatch({
        type: GENERIC_MODAL_DATA_VIEW_INIT,
        payload: { modalSettings },
    });
    
    // Open modal
    dispatch(toggleModal());

    // Trigger BE-call

    if (modalSettings?.modalPageSystemId) {
        dispatch(load(modalSettings.modalPageSystemId, false, true, modalSettings.entitySystemId));
    }
};

//Maybe not used any more.. We get tabs from DataSource processor
export const getGenericDataTabs = () => (dispatch, getState) => {
    return get('/api/cart/getDeliveryCartTabs')
        .then((response) => response.json())
        .then((cartTabs) => dispatch(receiveGenericDataViewTabs(cartTabs)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const getGenericDataViewForExport = () => (dispatch, getState) => {
    dispatch(toggleLoader(true));
    const pageId = getState().genericDataView.currentPageId;
    return get(rootRoute + 'getGenericDataViewForExport/' + pageId)
        .then((response) => response.json())
        .then((data) => {
            const fileName = pageId + '.xlsx';

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

export const getGenericDataViewByTab = (id) => (dispatch, getState) => {
    dispatch(toggleLoader(true));
    const pageId = getState().genericDataView.currentPageId;
    return get(rootRoute + `getGenericDataViewByTab/${pageId}?id=${id}`)
        .then((response) => response.json())
        .then((response) => dispatch(clearAndUpdate(response)))
        .then((response) => dispatch(toggleLoader(false)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

///headerInformation
export const getHeaderInformationData = () => (dispatch, getState) => {
    return;

    let pageId = getState().genericDataView.currentPageId;
    if (!pageId || pageId.length < 1) {
        pageId = 'void';
    }
    return get(rootRoute + `getHeaderInformation/${pageId}`)
        .then((response) => response.json())
        .then((response) => dispatch(setHeaderInformation(response)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const toggleLoader = (visible) => (dispatch, getState) => {
    dispatch({
        type: GENERIC_DATA_VIEW_SET_SEARCH_PENDING_STATUS,
        payload: {
            isSearching: visible,
        },
    });

    dispatch(toggleGenericLoader(visible, genericLoaderType));
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
export const checkResponse = (response, isInModal = false) => (dispatch, getState) => {
    
    if (response.result) {
        response = response.result;
    }
    
    //console.log('Check Response ', response);
    if (!response) {
        dispatch(toggleLoader(false));
        return null;
    }
    
    //Clear Data view before adding data-due to strange bug //
    dispatch(clearRows(isInModal));
    
    if (!isInModal) {
        dispatch(getHeaderInformationData());
        //Set dataViewHasTabs to true if response contains dataViewTabs
        if (response.dataViewTabs) {
            response.dataViewHasTabs = true;
        }
        dispatch(clearRows());
    }

    dispatch(toggleLoader(false));
    
    if (response.isNotLoggedOn) {
        //We use reversed condition due to object maybe not found
        return dispatch({
            type: GENERIC_DATA_VIEW_SET_NOT_LOGGED_ON,
            payload: {
                isNotLoggedOn: response.isNotLoggedOn,
            },
        });
    } else {
        return dispatch(receive(response, isInModal));
    }
};

//Called from GenericDataRow.action
export const receiveGenericDataViewTabs = (cartTabs) => (
    dispatch,
    getState
) => {
    return dispatch({
        type: GENERIC_DATA_VIEW_UPDATE_CART_TABS,
        payload: {
            dataViewTabs: cartTabs,
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
        type: GENERIC_DATA_VIEW_GET_HEADER_INFORMATION_DATA,
        payload: {
            headerInformation: headerInfo,
        },
    });
};

export const loadError = (error) => (dispatch, getState) => {
    dispatch(toggleLoader(false));
    return dispatch({
        type: GENERIC_DATA_VIEW_LOAD_ERROR,
        payload: {
            error,
        },
    });
};

export const clearAndUpdate = (response, isInModal=false) => (dispatch, getState) => {
    dispatch(clearRows(isInModal));
    dispatch(receive(response, isInModal));
    //if (response.dataContainers) {
    //   dispatch({
    //       type: GENERIC_DATA_VIEW_ALL_ROWS,
    //       payload: {
    //           dataContainers: response.dataContainers
    //       }
    //   });
    //}
    return true;
};

export const clearRows = (isInModal = false) => {
    if (isInModal) {
        return {
            type:  GENERIC_MODAL_DATA_VIEW_ALL_ROWS,
            payload: {
                modalDataContainers: [],
            },
        };
    }
    return {
        type: GENERIC_DATA_VIEW_ALL_ROWS,
        payload: {
            dataContainers: [],
        },
    };
};

export const updateAllRows = (dataContainers, isInModal=false) => {
    return {
        type: isInModal ? GENERIC_MODAL_DATA_VIEW_ALL_ROWS : GENERIC_DATA_VIEW_ALL_ROWS,
        payload: {
            dataContainers: dataContainers,
        },
    };
};

export const receive = (data, isInModal = false) => {
    
    if (isInModal) {
        return {
            type: GENERIC_MODAL_DATA_VIEW_RECEIVE,
            payload: data,
        };
    }
    return {
        type: GENERIC_DATA_VIEW_RECEIVE,
        payload: data,
    };
};

// Används denna??
export const updateFieldsInData = (dataContainers, isInModal = false) => {
    return {
        type: isInModal ? GENERIC_MODAL_DATA_UPDATE_FIELDS : GENERIC_DATA_UPDATE_FIELDS,
        payload: {
            dataContainers,
        },
    };
};