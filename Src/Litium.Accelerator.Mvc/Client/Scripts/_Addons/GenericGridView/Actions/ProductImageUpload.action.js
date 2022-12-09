import { post } from '../../../Services/http';
import { catchError } from '../../../Actions/Error.action';
import { toggleLoader } from './GenericGridView.action';
const rootRoute = '/api/genericgridview/';
let newImageIndexCount = 0;
import {
    GENERIC_GRID_VIEW_LOAD_ERROR, GENERIC_GRID_VIEW_RECEIVE, GENERIC_GRID_ROW_UPDATE, GENERIC_GRID_VIEW_SHOWEDITIMAGES, GENERIC_GRID_VIEW_IMAGES_WILLBEADDED
} from '../constants';

export const removeImageFromVariant = (entitySystemId, imageId, rowIndex, fieldId) => (dispatch, getState) => {
    const type = getState().genericGridView.currentDataType;
    // dispatch(toggleLoader(true));

    return post(rootRoute + `removeImageFromVariant/${entitySystemId}/${type}/${imageId.split('-id-')[1]}`)
        .then((response) => response.json())
        .then((response) => dispatch(handleRemoveImage(imageId, rowIndex, fieldId)))
        .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
    // .then((response) => dispatch(checkPaging(response, pagenationActive)))
};

export const addImagesToArchiveAndVariant = (entitySystemId, imageArr, rowIndex, fieldId) => (dispatch, getState) => {
    newImageIndexCount = 0;
    const type = getState().genericGridView.currentDataType;
    //dispatch(toggleLoader(true));
    if (imageArr) {
        const result = imageArr.reduce((objArray = [], currentValue) => {
            const data = {
                imageBase64: currentValue.data_url,
                imageName: currentValue.file?.name
            };
            objArray.push(data);
            return objArray;
        }, []);

        post(rootRoute + `addImagesToArchiveAndVariant/${entitySystemId}/${type}`, result)
            .then((response) => response.json())
            .then((response) => dispatch(handleImage(response, rowIndex, fieldId)))
            .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
    }

    // Iterate through images
    // Check so that image doues not exist in array

};

const handleRemoveImage = (imageId, rowIndex, fieldId) => (dispatch, getState) => {
    const fields = [...getState().genericGridView.dataRows[rowIndex].fields];
    if (imageId && fields) {
        fields.forEach((element, index) => {
            if (element.fieldID === fieldId) {
                fields[index].dropDownOptions = fields[index].dropDownOptions.filter(x => x.text != imageId);
            }
        });
    }

    const data = {
        entitySystemId: fields[0].entitySystemId,
    };

    dispatch({
        type: GENERIC_GRID_ROW_UPDATE,
        payload: { data, fields },
    });
};
const handleImage = (response, rowIndex, fieldId) => (dispatch, getState) => {
    const fields = [...getState().genericGridView.dataRows[rowIndex].fields];

    if (response && fields) {
        fields.forEach((element, index) => {
            if (element.fieldID === fieldId) {
                response.forEach((newImage) => {
                    fields[index].dropDownOptions = [...fields[index].dropDownOptions, { value: newImage?.value, text: newImage?.text }];
                })
            }
        });
    }

    const data = {
        entitySystemId: fields.entitySystemId,
    };

    dispatch({
        type: GENERIC_GRID_ROW_UPDATE,
        payload: { data, fields },
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


export const receive = (data) => {
    return {
        type: GENERIC_GRID_VIEW_RECEIVE,
        payload: data,
    };
};


export const showEditImages = (open, index) => {
    return {
        type: GENERIC_GRID_VIEW_SHOWEDITIMAGES,
        payload: { showModal: { open, index } },
    }
}

export const catshImagesWillBeAdded = (images) => (dispatch, getState) => {
    dispatch({
        type: GENERIC_GRID_VIEW_IMAGES_WILLBEADDED,
        payload: { images },
    });
}