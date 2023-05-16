import { get, patch, post, put, remove } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';

import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';
import {
    receiveCart,
    receive,
    checkDataContainerResponse,
} from './GenericDataContainer.action';
import {
    checkResponse,
    getHeaderInformationData,
} from './GenericDataView.action';
import 'abortcontroller-polyfill/dist/abortcontroller-polyfill-only';

import {
    GENERIC_DATA_FIELD_LOAD, GENERIC_DATA_FIELD_LOAD_ERROR, GENERIC_DATA_FIELD_RECEIVE, GENERIC_DATA_FIELD_LIST_RECEIVE,
    GENERIC_DATA_CONTAINER_UPDATE, GENERIC_DATA_FIELD_GET_DATA_LIST, GENERIC_DATA_FIELD_GET_ORGANIZATION_LIST, GENERIC_MODAL_DATA_CONTAINER_UPDATE
} from '../constants';

const rootRoute = '/api/genericdataview/';

let abortController;

export const getOrganizationData = (entitySystemId) => (dispatch, getState) => {
    if (entitySystemId) {
        const currentPageId = getState().genericDataView.currentPageId;
        abortController && abortController.abort();
        abortController = new AbortController();
        dispatch(toggleGenericLoader(true));
        return post(
            rootRoute +
            'getFieldData/' +
            currentPageId +
            '/OrganizationSelection/' +
            entitySystemId,
            '',
            abortController
        )
            .then((response) => response.json())
            .then((response) => dispatch(receiveOrganizationList(response)))
            .then((response) => dispatch(toggleGenericLoader(false)))
            .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
    } else {
        dispatch(receiveOrganizationList(null));
    }
};

export const autocompleteGetList = (fieldId, query) => (dispatch, getState) => {
    const currentPageId = getState().genericDataView.currentPageId;
    //if (!query || query.length < 3) {
    //    return;
    //}

    abortController && abortController.abort();
    abortController = new AbortController();
    dispatch(toggleGenericLoader(true));
    return post(
        rootRoute +
        'getFieldData/' +
        currentPageId +
        '/' +
        fieldId +
        '/' +
        encodeURI(query),
        '',
        abortController
    )
        .then((response) => response.json())
        .then((response) => dispatch(receiveList(response)))
        .then((response) => dispatch(toggleGenericLoader(false)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const buttonClick = (fieldId, containerIndex, setValueObject, isInModal = false, buttonSettings = {}) => (
    dispatch,
    getState
) => {

    let fields = [];

    if (containerIndex !== null && containerIndex !== undefined) {
        fields = getState().genericDataView.dataContainers[containerIndex].fields;
    }
    const data = {
        entitySystemId: fields.entitySystemId,
    };

    dispatch({
        type: isInModal ? GENERIC_MODAL_DATA_CONTAINER_UPDATE : GENERIC_DATA_CONTAINER_UPDATE,
        payload: { data, fields },
    });

    const currentPageId = getState().genericDataView.currentPageId;
    setValueObject.fieldId = fieldId;

    setValueObject.dataSource = currentPageId;
    dispatch(toggleGenericLoader(true));
    return put(rootRoute + 'buttonClick', setValueObject)
        .then((response) => response.json())
        .then((response) => dispatch(updateDataContainer(response, fields, buttonSettings)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
};

export const loadError = (error) => ({
    type: GENERIC_DATA_FIELD_LOAD_ERROR,
    payload: {
        error,
    },
});

export const receiveList = (data) => {
    return {
        type: GENERIC_DATA_FIELD_GET_DATA_LIST,
        payload: {
            currentAutocompleteList: data, //currentAutocompleteList: [{ value: 1, text: 2 }, { value: 11, name: 22 }, { value: 1, name: 2 }, { value: 11, name: 22 }]
        },
    };
};

export const receiveOrganizationList = (data) => {
    return {
        type: GENERIC_DATA_FIELD_GET_ORGANIZATION_LIST,
        payload: {
            organizations: data,
        },
    };
};

export const updateDataContainer = (response, fields, buttonSettings = {}) => (dispatch, getState) => {
    dispatch(toggleGenericLoader(false));
    
    // If ony one data container is to be updated
    if (response) {
        // First check if we want to download a file
        if (buttonSettings && buttonSettings.downloadMimeTypeString?.length > 0 && buttonSettings.downloadFileType?.length > 0) {
            const fileName = `${buttonSettings.downloadFileName || buttonSettings.fieldId}.${buttonSettings.downloadFileType}`;

            const linkSource = `${buttonSettings.downloadMimeTypeString},${response.base64}`;
            const downloadLink = document.createElement('a');

            downloadLink.href = linkSource;
            downloadLink.download = fileName;

            document.body.appendChild(downloadLink);

            downloadLink.click();
            downloadLink.remove();
            return;
        }
        return dispatch(checkDataContainerResponse(response, fields));
    }

    // Update header data
    dispatch(getHeaderInformationData());

    // If whole data view is updated
    return dispatch(checkResponse(response));

    //if (response.cart){
    //    dispatch(receiveCart(response.cart));
    //}
    //dispatch(receive(response.dataContainer, fields));
};
