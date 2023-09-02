/****   Generic Data Container ( Former Row )   ****/

import { patch, post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';
import {
    checkResponse,
    receiveGenericDataViewTabs,
    getHeaderInformationData,
    clearRows,
} from './GenericDataView.action';

import {
    GENERIC_DATA_CONTAINER_UPDATE, GENERIC_DATA_CONTAINER_ERROR, GENERIC_DATA_CONTAINER_RECEIVE, CART_RECEIVE, GENERIC_MODAL_DATA_CONTAINER_RECEIVE, GENERIC_MODAL_DATA_CONTAINER_UPDATE
} from '../constants';

const rootRoute = '/api/genericdataview/';

export const checkFormField = (obj) => {

    //obj.fieldValue
    //obj.field
    //alert(JSON.stringify(fields));
    const validationRules = obj?.field?.settings?.validationRules;
    const fieldId = obj?.fieldId;
    const fieldValue = obj?.fieldValue;
    let errorObject = null;
    if (obj && obj.fieldId && validationRules) {
        for (let i = 0; i < validationRules.length; i++) {
            const obj = validationRules[i];
            if (obj.rule === 'IsRequired') {
                if ((typeof fieldValue == 'string' && fieldValue.trim().length < 1) || (typeof fieldValue == 'boolean' && fieldValue == false)) {
                    errorObject = {
                        ...obj, fieldId
                    };
                    break;
                }
            }
            else {
                const regex = new RegExp(obj.rule);
                const result = regex.test(fieldValue);
                if (!result) {
                    errorObject = {
                        ...obj, fieldId
                    };
                    break;
                }
            }
        }
    }
    return errorObject;
};

const getFocused = () => {
    //const activeElement = document.activeElement;
    return document.activeElement;
}

export const sendContainerState = (entitySystemId, containerState) => (dispatch, getState) => {

    //var field = fields.find(x => x.fieldId === data.fieldId);
    //if (field && field.settings && field.settings.validationRules) {

    //    isFieldValid(data.fieldValue, field.settings.validationRules);
    //    //alert(JSON.stringify(field.settings.validationRules));
    //    return;
    //}   
    
    //dispatch({
    //    type: isInModal ? GENERIC_MODAL_DATA_CONTAINER_UPDATE : GENERIC_DATA_CONTAINER_UPDATE,
    //    payload: { data, fields },
    //});
    // console.log('Update Row --', type, data, fields);
    const data = {
        entitySystemId,
        containerState
    };
    return post(rootRoute + "changeContainerState", data)
        .then((response) => response.json())
        //.then((response) => dispatch(checkDataContainerResponse(response, fields, isInModal, entitySystemId)))
        .catch((ex) =>
            dispatch(catchError(ex, (error) => updateError(error, entitySystemId)))
        );
};

export const update = (pageSystemId, data, fields, isInModal = false, entitySystemId = '') => (dispatch, getState) => {
    //var field = fields.find(x => x.fieldId === data.fieldId);
    //if (field && field.settings && field.settings.validationRules) {

    //    isFieldValid(data.fieldValue, field.settings.validationRules);
    //    //alert(JSON.stringify(field.settings.validationRules));
    //    return;
    //}   

    dispatch({
        type: isInModal ? GENERIC_MODAL_DATA_CONTAINER_UPDATE : GENERIC_DATA_CONTAINER_UPDATE,
        payload: { data, fields },
    });
    // console.log('Update Row --', type, data, fields);

    return patch(rootRoute + pageSystemId, data)
        .then((response) => response.json())
        .then((response) => dispatch(checkDataContainerResponse(response, fields, isInModal, entitySystemId)))
        .catch((ex) =>
            dispatch(catchError(ex, (error) => updateError(error, fields)))
        );
};
export const checkDataContainerResponse = (response, fields, isInModal = false, entitySystemId = '', containerIndex = -1, responseActions) => (dispatch, getState) => {

    if (!isInModal || responseActions?.updateTopLevel) {
        // Update header data
        dispatch(getHeaderInformationData());
        // If processor return tabs
        if (response.dataViewTabs) {
            dispatch(receiveGenericDataViewTabs(response.dataViewTabs));
        }
    }

    if (!response.cart && !response.dataContainers) {

        const genericDataView = { ...getState().genericDataView };
        
        let updatedTopLevel = false;
        if (isInModal && responseActions?.updateTopLevel) {
            const topLevelContainerIndex = window.currGenDW_dataContainerIndex;
            if (topLevelContainerIndex !== null && topLevelContainerIndex !== undefined && topLevelContainerIndex > -1) {
                updatedTopLevel = true;
                const dataContainers = genericDataView.dataContainers[topLevelContainerIndex] = { ...response };
                return dispatch({
                    type: GENERIC_DATA_CONTAINER_RECEIVE,
                    payload: { dataContainers, fields },
                });
            }
        }
        if (!updatedTopLevel) {
            if (isInModal) {
                genericDataView.modalDataContainers[containerIndex] = { ...response };
            }
            else {
                genericDataView.dataContainers[containerIndex] = { ...response };
            }
        }
        // Kolla så denna fortfatande funkar
        dispatch(receive(genericDataView, fields, isInModal, entitySystemId)); //dispatch(checkResponse(genericDataView, isInModal));
   } else {
        if (response.cart) {
            dispatch(receiveCart(response.cart));
        }
        if (response.dataContainers) {
            dispatch(receive(response.dataContainers, fields, isInModal, entitySystemId));
        }
        else {
            dispatch(receive(response, fields, isInModal, entitySystemId));
        }
        return true;
    }
};

export const loadError = (error) => ({
    type: GENERIC_DATA_CONTAINER_ERROR,
    payload: {
        error,
    },
});

export const receiveCart = (cart) => ({
    type: CART_RECEIVE,
    payload: cart,
});

export const updateError = (error, fields) => ({
    type: GENERIC_DATA_CONTAINER_ERROR,
    payload: { error, fields },
});

///Kolla upp denna metod så att den fortfarande fungerar
export const receive = (response, fields, isInModal = false, entitySystemId = '') => {
    return {
        type: isInModal ? GENERIC_MODAL_DATA_CONTAINER_RECEIVE : GENERIC_DATA_CONTAINER_RECEIVE,
        payload: { response, fields },
    };
};

//export const receive = (dataContainers, fields, isInModal = false, entitySystemId = '') => (dispatch, getState) =>{
//    ///Kolla upp denna metod
//    //console.log("data", response);
//    //dispatch(clearRows(isInModal));
//    return dispatch({
//        type: isInModal ? GENERIC_MODAL_DATA_CONTAINER_RECEIVE : GENERIC_DATA_CONTAINER_RECEIVE,
//        payload: { dataContainers, fields },
//    });
//};
