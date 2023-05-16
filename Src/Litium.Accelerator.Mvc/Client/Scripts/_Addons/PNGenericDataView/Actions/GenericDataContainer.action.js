/****   Generic Data Container ( Former Row )   ****/

import { patch, post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';
import {
    receiveGenericDataViewTabs,
    getHeaderInformationData,
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
                if (fieldValue.trim().length < 1) {
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
export const checkDataContainerResponse = (response, fields, isInModal = false, entitySystemId = '') => (dispatch, getState) => {

    if (!isInModal) {
        // Update header data
        dispatch(getHeaderInformationData());

        // If processor return tabs
        if (response.dataViewTabs) {
            dispatch(receiveGenericDataViewTabs(response.dataViewTabs));
        }
    }
    if (!response.cart && !response.dataContainer) {
        return dispatch(receive(response, fields, isInModal, entitySystemId));
    } else {
        if (response.cart) {
            dispatch(receiveCart(response.cart));
        }
        if (response.dataContainer) {
            dispatch(receive(response.dataContainer, fields, isInModal, entitySystemId));
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

export const receive = (response, fields, isInModal = false, entitySystemId = '') => {
    return {
        type: isInModal ? GENERIC_MODAL_DATA_CONTAINER_RECEIVE : GENERIC_DATA_CONTAINER_RECEIVE,
        payload: { response, fields },
    };
};
