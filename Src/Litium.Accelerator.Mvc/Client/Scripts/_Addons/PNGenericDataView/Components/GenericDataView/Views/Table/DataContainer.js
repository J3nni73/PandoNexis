import React, { useEffect, useState } from 'react';
import PropTypes, { object } from 'prop-types';
import classNames from 'classnames';
import { useForm } from 'react-hook-form';
import { GenericDataViewField } from '../../Field';
import { any } from 'array-flat-polyfill';
import { FieldErrorMsg, getFieldData } from '../viewFunctions';
import { checkFormField } from '../../../../Actions/GenericDataContainer.action';
import { buttonClick } from '../../../../Actions/GenericDataContainerField.action';
import { loadModal } from '../../../../Actions/GenericDataView.action';
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
    }) => {
        const { register, handleSubmit, setFocus, reset, formState: { dirtyFields, isSubmitted }, } = useForm();

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

        const onButtonClick = (form, useConfirmation, fieldSettings, confirmationText, fieldId) => {
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

        //// If there is and error reset form fields
        //useEffect(() => {
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

        return (
            <>
                {error && (
                    <tr>
                        <td colSpan={fields.length} className="generic-data-view__error">
                            {error.message || error.toString() || 'There was an error.'}
                        </td>
                    </tr>
                )}
                <tr>
                    {fields.map(
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
                            <td
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
                                    `${fieldsToShow.includes(fieldIndex) ? '' : 'fieldToHide'}`
                                )}
                            >
                                {settings.fieldMessage && (
                                    <span
                                        className="generic-data-view__field-message"
                                        dangerouslySetInnerHTML={{ __html: settings.fieldMessage }}
                                    ></span>
                                )}
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
                            </td>
                        )
                    )}
                </tr>
            </>
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
