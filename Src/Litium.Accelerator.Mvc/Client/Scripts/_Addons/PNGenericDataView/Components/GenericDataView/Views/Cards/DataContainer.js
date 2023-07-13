import React, { useEffect, useState, Fragment, useRef } from 'react';
import { useSelector, useDispatch, connect } from 'react-redux';
import PropTypes, { object } from 'prop-types';
import classNames from 'classnames';
import { useForm } from 'react-hook-form';
import { GenericDataViewField } from '../../Field';
import { any } from 'array-flat-polyfill';
import { checkFormField } from '../../../../Actions/GenericDataContainer.action';
import { buttonClick } from '../../../../Actions/GenericDataContainerField.action';
import { loadModal } from '../../../../Actions/GenericDataView.action';

import { translate } from '../../../../../../Services/translation';
import { Tooltip as ReactTooltip } from 'react-tooltip';

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
        settings,
        dataContainer,
        mainSettings
    }) => {
        const { register, handleSubmit, setFocus, watch, reset, getValues, formState: { dirtyFields, isSubmitted, errors }, } = useForm();
        const [cardFields, setCardFields] = useState([]);
       
        const [isFormValid, setIsFormValid] = useState(false);
        const [containerSettings, setContainerSettings] = useState(settings);
        const [cardIngressField, setCardIngressField] = useState(null);
        const [errorObject, setErrorObject] = useState(null);
        const dispatch = useDispatch();

        const isContainerValid = (identifierField, form, theFormFields) => {
            
            let isFullFormCheck = false;
            
            if (theFormFields === undefined) {
                isFullFormCheck = true;
                theFormFields = form;
            }
            
            if (Object.keys(theFormFields).length < 1) {
                const lastClickedFieldId = window.currGenDW_lastClickedFieldId;
                const lastClickedFieldValue = window.currGenDW_lastClickedFieldId;
                
                theFormFields[lastClickedFieldId] = true;
            }
            
            if (Object.keys(theFormFields).length) {
                let errorObjects = [];
                const FullData = Object.keys(theFormFields).reduce((payload, key) => {
                    //console.log("key", form[key]);
                    var errObj = checkFormField({
                        fieldId: key,
                        fieldValue: form[key],
                        field: fields.find(x => x.fieldId === key)
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
        }; 
        const onCheckboxChange = (form, fieldId, entitySystemId, dataContainerIndex, settings) => {
            const isTrue = currGenDW_isTrue;
            form[fieldId] = isTrue;
            const identifierField = { entitySystemId };
            if (!dirtyFields[fieldId]) {
                dirtyFields[fieldId] = true;
            }
            const errObjs = isContainerValid(identifierField, form);
            setErrorObject(errObjs);
            if (errObjs?.length) {
                return true;
            }
            isContainerValid(identifierField, form, dirtyFields);
        }
        const validateForm = (form) => {
            const entitySystemId = fields[0].entitySystemId;
            const identifierField = { entitySystemId };
            const errObjs = isContainerValid(identifierField, form);
            setErrorObject(errObjs);
        };

        const onButtonClick = (form, buttonData = null) => {
            const useConfirmation = currGenDW_useConfirmation;
            const fieldSettings = currGenDW_fieldSettings;
            const confirmationText = currGenDW_confirmationText;
            const fieldId = currGenDW_fieldId;

            const entitySystemId = fields[0].entitySystemId;
            const identifierField = { entitySystemId };
            const errObjs = isContainerValid(identifierField, form);
            setErrorObject(errObjs);
            if (errObjs?.length) {
                return true;
            }

            if (useConfirmation) {
                if (!confirm(confirmationText)) {
                    return false;
                }
            }

            if (fieldSettings?.buttonOpenInModal) {

                const modalSettings = {
                    modalPageSystemId: fieldSettings.pageSystemId,
                    entitySystemId,
                };
                dispatch(loadModal(modalSettings));
                return;
            }

            const selectedValueObject = {
                value: '',
                name: '',
                entitySystemId: entitySystemId,
                dataContainerIndex,
                postContainerPageSystemId = containerSettings?.postContainerPageSystemId || null,
                form: containerSettings?.postContainer ? form : null
            };
            dispatch(buttonClick(fieldId, dataContainerIndex, selectedValueObject, false, fieldSettings, fieldSettings.pageSystemId));
        };

        const onBlur = (form, getWinValue=false) => {
            if (getWinValue) {
                form[window.currGenDW_lastClickedFieldId] = window.currGenDW_lastClickedFieldValue;
            }
            const identifierField = { EntitySystemId: fields[0].entitySystemId };
            const errObjs = isContainerValid(identifierField, form, dirtyFields);
            setErrorObject(errObjs);
        };

        useEffect(() => {
            if (ingressField) {
                const cardIngress = fields.filter(x => x.fieldId == ingressField);
                if (cardIngress.length > 0) {
                    setCardIngressField(cardIngress[0]);
                    setCardFields(fields.filter(x => x.fieldId != ingressField));
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
                            [field.fieldId]: field.fieldValue,
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
        //                    [field.fieldId]: field.fieldValue,
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
                        {error.status + ' ' || ''}{error.title || error.toString() || 'There was an error.'}
                    </div>
                )}
                <div className="card">
                    {cardIngressField &&
                        <div {...register(cardIngressField.fieldId)}>
                            <GenericDataViewField
                                isEditable={cardIngressField.settings && cardIngressField.settings.editable}
                                type={cardIngressField.fieldType}
                                suffix={cardIngressField.fieldSuffix}
                                defaultValue={cleanData(cardIngressField.fieldValue, cardIngressField.fieldType) || ''}
                                title={cardIngressField.fieldName}
                                name={cardIngressField.fieldId}
                                setErrorObject={setErrorObject}
                                onCheckboxChange={fieldType === 'checkbox' || fieldType === 'radiobutton' ? handleSubmit(onCheckboxChange)
                                    : null}
                                onButtonClick={
                                    cardIngressField.fieldType === 'button'
                                        ? handleSubmit(onButtonClick)
                                        : null
                                }
                                onBlur={
                                    cardIngressField.fieldType !== 'autocomplete' && cardIngressField.fieldType !== 'dropdown' && cardIngressField.fieldType !== 'productimageupload' && cardIngressField.fieldType !== 'dropdown'
                                        ? handleSubmit(onBlur)
                                        : null
                                }
                              
                                aria-labelledby={cardIngressField.fieldName}
                                entitySystemId={cardIngressField.entitySystemId}
                                dataContainerIndex={dataContainerIndex}
                                fieldId={cardIngressField.fieldId}
                                fieldSettings={cardIngressField.settings}
                                ref={register}
                                dropDownOptions={
                                    cardIngressField.fieldType === 'dropdown' || cardIngressField.fieldType === 'productimageupload' ? cardIngressField.options : null
                                }
                            />
                        </div>
                    }
                    <div className={`row small-up-${columnsInsideContainerSmall} medium-up-${columnsInsideContainerMedium} large-up-${columnsInsideContainerLarge}`}>
                        {cardFields.map(
                            (
                                {
                                    fieldId,
                                    fieldType,
                                    fieldName,
                                    fieldValue,
                                    fieldSuffix,
                                    settings,
                                    entitySystemId,
                                    options,
                                },
                                fieldIndex
                            ) => (
                                <div
                                    key={`field-${fieldIndex}-${fieldName}`}
                                    id={`${entitySystemId}${fieldId}${dataContainerIndex}`}
                                    {...register(fieldId)}
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
                                        `${fieldsToShow.includes(fieldIndex) ? '' : 'fieldToHide'} columns `
                                    )}
                                >
                                    {settings.fieldMessage && (
                                        <span
                                            className="generic-data-view__field-message"
                                            dangerouslySetInnerHTML={{ __html: settings.fieldMessage }}
                                        ></span>
                                    )}
                                    {showTitles && (fieldType !== 'button' && settings.genericButtons?.length < 1) &&
                                        <b>{fieldName}</b>
                                    }
                                   
                                    <GenericDataViewField
                                        isEditable={settings && settings.editable}
                                        type={fieldType}
                                        suffix={fieldSuffix}
                                        defaultValue={  cleanData(fieldValue, fieldType) || ''}
                                        title={fieldName}
                                        name={fieldId}
                                        setErrorObject={setErrorObject}
                                        onCheckboxChange={fieldType === 'checkbox' || fieldType === 'radiobutton' ? handleSubmit((e) => onCheckboxChange(e, fieldId, entitySystemId, dataContainerIndex, settings))
                                            : null}
                                        onButtonClick={
                                            fieldType === 'button' || settings.genericButtons?.length > 0
                                                ? handleSubmit(onButtonClick)
                                                : null
                                        }
                                        onBlur={
                                            containerSettings?.postContainer ? handleSubmit(validateForm) :
                                                !containerSettings?.postContainer && (fieldType !== 'autocomplete' && fieldType !== 'dropdown' && fieldType !== 'productimageupload' && fieldType !== 'checkbox' && fieldType !== 'radiobutton')
                                                ? handleSubmit(onBlur)
                                                : null
                                        }
                                        
                                        onChange={
                                            !containerSettings?.postContainer && (fieldType === 'dropdown' || fieldType === 'productimageupload' || fieldType === 'datetime') ? handleSubmit((e) => onBlur(e, true)) : null
                                        }
                                        aria-labelledby={fieldName}
                                        entitySystemId={entitySystemId}
                                        nextEntitySystemId={fieldIndex !== cardFields.length - 1 ? [fieldIndex + 1].entitySystemId : '-1'}
                                        dataContainerIndex={dataContainerIndex}
                                        fieldId={fieldId}
                                        fieldSettings={settings}
                                        ref={register}
                                        genericButtons={settings.genericButtons}
                                        dropDownOptions={
                                            fieldType === 'dropdown' || fieldType === 'productimageupload' ? options : null
                                        }
                                    />
                                    {settings.fieldTooltipMessage && settings.fieldTooltipMessage.length>0 &&
                                        <ReactTooltip className="generic-data-view__tooltip" float={true} delayShow="800" delayHide="300" anchorId={`${entitySystemId}${fieldId}${dataContainerIndex}`} variant={settings.fieldTooltipType || "dark"} positionStrategy="fixed" offset="32" place="left" >
                                            {settings.fieldTooltipMessage}
                                        </ReactTooltip>
                                    }
                                    {errorObject && <FieldErrorMsg fieldId={fieldId} errObjs={errorObject} />}
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
                    {fields && containerSettings && containerSettings?.postContainer && containerSettings?.postContainerPageSystemId &&
                        <div className="row">
                            <div className="small-12 columns text--right">
                                <GenericDataViewField
                                    type={"button"}
                                    title={containerSettings?.postContainerButtonText || translate('addons.genericdataview.buttons.generalpost')}
                                    name={"Post"}
                                    setErrorObject={setErrorObject}
                                    onButtonClick={handleSubmit(onButtonClick)}
                                    aria-labelledby={settings?.postContainerButtonText || translate('addons.genericdataview.buttons.generalpost')}
                                    entitySystemId={fields[0].entitySystemId}
                                    dataContainerIndex={dataContainerIndex}
                                    fieldId={"Post"}
                                    fieldSettings={{ buttonText: containerSettings?.postContainerButtonText || translate('addons.genericdataview.buttons.generalpost'), }}
                                    ref={useRef()}
                                />
                            </div>
                        </div>
                    }
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
