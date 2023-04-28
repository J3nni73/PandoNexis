import React, { useEffect, useState, Fragment } from 'react';
import { useSelector, useDispatch, connect } from 'react-redux';
import PropTypes, { object } from 'prop-types';
import classNames from 'classnames';
import { useForm } from 'react-hook-form';
import { GenericDataViewField } from '../../Field';
import { any } from 'array-flat-polyfill';
import { checkFormField } from '../../../../Actions/GenericDataContainer.action';
import { buttonClick } from '../../../../Actions/GenericDataContainerField.action';
import { loadModal } from '../../../../Actions/GenericDataView.action';

import { FieldErrorMsg, getFieldData } from '../viewFunctions';
/**
 *
 * @param {Array} fields - List of columns to display in the data container.
 * @param {Array} editableFieldIds - List of editable columns that can be updated in the data view.
 */


export const DataContainer = React.memo(
    ({
        fields = [],
        onDataContainerChange,
        error,
        isLoading = false,
        isInModal = false,
        identifierFieldId,
        dataContainerIndex,
        fieldsToShow,
        columnsInsideContainerSmall,
        columnsInsideContainerMedium,
        columnsInsideContainerLarge,
        showTitles,
        ingressField,

    }) => {
        const { register, handleSubmit, setFocus, watch, reset, getValues, formState: { dirtyFields, isSubmitted, errors }, } = useForm();
        const [cardFields, setCardFields] = useState([]);
        const [isFormValid, setIsFormValid] = useState(false);
        const [cardIngressField, setCardIngressField] = useState(null);
        const [errorObject, setErrorObject] = useState(null);
        const dispatch = useDispatch();

        const isContainerValid = (identifierField, form, theFormFields) => {
            let isFullFormCheck = false;
            if (theFormFields === undefined) {
                isFullFormCheck = true;
                theFormFields = form;
            }
            
            if (Object.keys(theFormFields).length) {
              
                let errorObjects = [];
                const FullData = Object.keys(theFormFields).reduce((payload, key) => {
                    //console.log("key", form[key]);
                    var errObj = checkFormField({
                        fieldID: key,
                        fieldValue: form[key],
                        field: fields.find(x => x.fieldID === key)
                    });
                   
                    if (errObj) {
                        errorObjects.push(errObj);
                        return null;
                    }
                    else {
                        if (!isFullFormCheck) {
                            reset({ [key]: form[key] });
                            const data = getFieldData(payload, form, key);
                            onDataContainerChange(data, fields, isInModal);
                            return data;
                        }
                        return null;
                    }
                }, identifierField);
                             
                return errorObjects;
            }
        }

        const onButtonClick = (form) => {
            //windows.currGenDW_useConfirmation = useConfirmation;
            //windows.currGenDW_fieldSettings = fieldSettings;
            //windows.currGenDW_fieldSettings = fieldSettings;
            //windows.currGenDW_confirmationText = confirmationText;
            //windows.currGenDW_fieldId = fieldId;
            const useConfirmation = currGenDW_useConfirmation;
            const fieldSettings = currGenDW_fieldSettings;
            const confirmationText = currGenDW_confirmationText;
            const fieldId = currGenDW_fieldId;

            const entitySystemId = fields[0].entitySystemId;
            const identifierField = { entitySystemId };
            const errObjs = isContainerValid(identifierField, form);
            setErrorObject(errObjs);
            if (!errObjs) {
                return true;
            }
            
            if (useConfirmation) {
                if (!confirm(confirmationText)) {
                    return false;
                }
            }
            if (fieldSettings?.buttonOpenInModal) {
                const modalSettings = {
                    modalPageSystemId: fieldSettings.modalPageSystemId,
                    entitySystemId,

                };
                dispatch(loadModal(modalSettings));
                return;
            }

            const selectedValueObject = {
                value: '',
                name: '',
                entitySystemId,
                dataContainerIndex,
            };
            dispatch(buttonClick(fieldId, dataContainerIndex, selectedValueObject));
        };

        const onBlur = (form) => {
            const identifierField = { EntitySystemId: fields[0].entitySystemId };
            const errObjs = isContainerValid(identifierField, form, dirtyFields);
            setErrorObject(errObjs);
        };

        useEffect(() => {
            if (ingressField) {
                const cardIngress = fields.filter(x => x.fieldID == ingressField);
                if (cardIngress.length > 0) {
                    setCardIngressField(cardIngress[0]);
                    setCardFields(fields.filter(x => x.fieldID != ingressField));
                    return;
                }
            }
            setCardFields(fields);
        }, []);

        // Reset container form state when fields are updated
        useEffect(() => {
            if (isSubmitted) {
                reset(
                    fields.reduce(
                        (state, field) => ({
                            ...state,
                            [field.fieldID]: field.fieldValue,
                        }),
                        {}
                    )
                );
            }
        }, [fields]);

        // If there is and error reset form fields
        //useEffect(() => {
        //    alert(2);
        //    if (error) {
        //        reset(
        //            fields.reduce(
        //                (state, field) => ({
        //                    ...state,
        //                    [field.fieldID]: field.fieldValue,
        //                }),
        //                {},
        //            ),
        //        );
        //    }

        //}, [error]);

        const cleanData = (fieldValue, fieldType) => {
            if (fieldValue && fieldType) {
                if (fieldType.toLowerCase() === 'decimal') {
                    fieldValue = parseFloat(fieldValue.replace(',', '.'));
                }
                return fieldValue;
            }
            return '';
        };
        if (!cardFields && !cardIngressField) {
            return null;
        }

        return (
            <div className="columns" >
                {error && (
                    <div colSpan={fields.length} className="generic-data-view__error">
                        {error.message || error.title || error.toString() || 'There was an error.'}
                    </div>
                )}
                <div className="card">
                    {cardIngressField &&
                        <div {...register(cardIngressField.fieldID)}>
                            <GenericDataViewField
                                isEditable={cardIngressField.settings && cardIngressField.settings.editable}
                                type={cardIngressField.fieldType}
                                suffix={cardIngressField.fieldSuffix}
                                defaultValue={cleanData(cardIngressField.fieldValue, cardIngressField.fieldType) || ''}
                                title={cardIngressField.fieldName}
                                name={cardIngressField.fieldID}
                                setErrorObject={setErrorObject}
                                onButtonClick={
                                    cardIngressField.fieldType === 'button'
                                        ? handleSubmit(onButtonClick)
                                        : null
                                }
                                onBlur={
                                    cardIngressField.fieldType !== 'autocomplete' && cardIngressField.fieldType !== 'dropdown' && cardIngressField.fieldType !== 'productimageupload'
                                        ? handleSubmit(onBlur)
                                        : null
                                }
                                onFieldChange={
                                    cardIngressField.fieldType === 'dropdown' || cardIngressField.fieldType === 'productimageupload' ? handleSubmit(onBlur) : null
                                }
                                aria-labelledby={cardIngressField.fieldName}
                                entitySystemId={cardIngressField.entitySystemId}
                                dataContainerIndex={dataContainerIndex}
                                fieldId={cardIngressField.fieldID}
                                fieldSettings={cardIngressField.settings}
                                ref={register}
                                dropDownOptions={
                                    cardIngressField.fieldType === 'dropdown' || cardIngressField.fieldType === 'productimageupload' ? cardIngressField.dropDownOptions : null
                                }
                            />
                        </div>
                    }
                    <div className={`row small-up-${columnsInsideContainerSmall} medium-up-${columnsInsideContainerMedium} large-up-${columnsInsideContainerLarge}`}>
                        {cardFields.map(
                            (
                                {
                                    fieldID,
                                    fieldType,
                                    fieldName,
                                    fieldValue,
                                    fieldSuffix,
                                    settings,
                                    entitySystemId,
                                    dropDownOptions,
                                },
                                fieldIndex
                            ) => (
                                <div
                                    key={`field-${fieldIndex}-${fieldName}`}
                                    {...register(fieldID)}
                                    style={
                                        isLoading
                                            ? {
                                                opacity: 0.5,
                                                backgroundColor: settings.backgroundColor || null,
                                            }
                                            : { backgroundColor: settings.backgroundColor || null }
                                    }
                                    className={classNames(
                                        {
                                            'generic-data-view__error': error,
                                        },
                                        `${fieldsToShow.includes(fieldIndex) ? '' : 'fieldToHide'} columns`
                                    )}
                                >
                                    {settings.fieldMessage && (
                                        <span
                                            className="generic-data-view__field-message"
                                            dangerouslySetInnerHTML={{ __html: settings.fieldMessage }}
                                        ></span>
                                    )}
                                    {showTitles && fieldType !== 'button' &&
                                        <b>{fieldName}</b>
                                    }
                                    <GenericDataViewField
                                        isEditable={settings && settings.editable}
                                        type={fieldType}
                                        suffix={fieldSuffix}
                                        defaultValue={cleanData(fieldValue, fieldType) || ''}
                                        title={fieldName}
                                        name={fieldID}
                                        setErrorObject={setErrorObject}
                                        onButtonClick={
                                            fieldType === 'button'
                                                ? handleSubmit(onButtonClick)
                                                : null
                                        }
                                        onBlur={
                                            fieldType !== 'autocomplete' && fieldType !== 'dropdown' && fieldType !== 'productimageupload'
                                                ? handleSubmit(onBlur)
                                                : null
                                        }
                                        onChange={
                                            fieldType === 'dropdown' || fieldType === 'productimageupload' ? handleSubmit(onBlur) : null
                                        }
                                        aria-labelledby={fieldName}
                                        entitySystemId={entitySystemId}
                                        dataContainerIndex={dataContainerIndex}
                                        fieldId={fieldID}
                                        fieldSettings={settings}
                                        ref={register}
                                        dropDownOptions={
                                            fieldType === 'dropdown' || fieldType === 'productimageupload' ? dropDownOptions : null
                                        }
                                    />
                                    {errorObject && <FieldErrorMsg fieldID={fieldID} errObjs={errorObject} />}
                                    {settings.errorFieldMessage && (
                                        <span
                                            className="generic-data-view__error-field-message"
                                            dangerouslySetInnerHTML={{
                                                __html: settings.errorFieldMessage,
                                            }}
                                        ></span>
                                    )}
                                </div>
                            )
                        )}
                    </div>
                </div>
            </div>
        );
    }
);

DataContainer.propTypes = {
    fields: PropTypes.array,
    onChange: PropTypes.func,
    error: PropTypes.any,
    isLoading: PropTypes.bool,
    editableFieldIds: PropTypes.arrayOf(PropTypes.string),
    identifierFieldId: PropTypes.string,
};

DataContainer.displayName = 'DataContainer';
export default DataContainer;
