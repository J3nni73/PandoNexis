import React, { useEffect, useState, Fragment, useRef } from 'react';
import { useSelector, useDispatch, connect } from 'react-redux';
import PropTypes, { object } from 'prop-types';
import classNames from 'classnames';
import { useForm } from 'react-hook-form';
import { GenericDataViewField } from '../../Field';
import { any } from 'array-flat-polyfill';
import { FieldErrorMsg, getFieldData } from '../viewFunctions';
import { checkFormField } from '../../../../Actions/GenericDataContainer.action';
import { buttonClick } from '../../../../Actions/GenericDataContainerField.action';
import { loadModal } from '../../../../Actions/GenericDataView.action';
//import { DndContext } from '@dnd-kit/core';
//import { Draggable } from '../../../../../../_PandoNexis/Components/Draggable';
//import { Droppable } from '../../../../../../_PandoNexis/Components/Droppable';

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
        dataContainer,
        dataContainerIndex,
        fieldsToShow,
        columnsInsideContainerSmall,
        columnsInsideContainerMedium,
        columnsInsideContainerLarge,
        showTitles,
        ingressField,
        settings,
        mainSettings,
        sortConfig,
        sortColumn,
        onlyShowTaskName = true

    }) => {
        const { register, handleSubmit, setFocus, reset, formState: { dirtyFields, isSubmitted }, } = useForm();
        const [cardFields, setCardFields] = useState([]);
        const [currentStatus, setCurrentStatus] = useState(settings?.currentStatus);
        const [nextAvailableStatus, setNewAvailableStatus] = useState(settings?.possibleNextStatusList);
        const [cardIngressField, setCardIngressField] = useState(null);
        const [errorObject, setErrorObject] = useState(null);
        const dispatch = useDispatch();
        const dragItem = useRef();
        const dragOverItem = useRef();
        let tempStatus = null;
        let initialOffset = null;
        let currentOffset = null;

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

        const dragStart = (e, status) => {
            e.dataTransfer.setData("text", 'Dragme');
            e.dataTransfer.effectAllowed = "copyMove";
            e.target.style.cursor = "move";
        };

        const dragEnter = (e, status) => {
            e.dataTransfer.effectAllowed = "copy";
        };
        const dragOver = (e, status, leaving = false) => {
            e.preventDefault();
            e.UseDefaultCursors = true;
            const isAvailable = nextAvailableStatus.indexOf(status.id) !== -1;
            if (status.id !== currentStatus && isAvailable) {
                const element = e.target;

                if (element) {                    
                    if (leaving) {
                        element.classList.remove("drag-over");
                    }
                    else {
                        element.classList.add("drag-over");
                        tempStatus = status.id;
                        e.UseDefaultCursors = false;
                    }
                }
            }
            else {
                e.dataTransfer.dropEffect = "copy";
            }
        };
        
        const dropIt = (e, status) => {
            console.log(tempStatus);
            console.log(currentStatus);

            if (tempStatus && tempStatus !== currentStatus) {
                setCurrentStatus(tempStatus);
                tempStatus = null;
                e.target.style.cursor = 'default'; // Reset cursor
                return true;
            }

        };
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
            <tr>
                {error && (
                    <div colSpan={fields.length} className="generic-data-view__error">
                        {error.message || error.toString() || 'There was an error.'}
                    </div>
                )}

                {mainSettings?.availableStatusList?.length > 0 && mainSettings.availableStatusList.map((status, index) => (
                    <td
                        
                        onDragEnter={(event) => dragOver(event, status)}
                        onDragLeave={(event) => dragOver(event, status, true)}
                        
                        key={`header-${index}`}
                        style={{ backgroundColor: status.backgroundColor || null }}
                        onClick={() => sortColumn(status.name, index)}
                        className={`${sortConfig && sortConfig?.name === status.name ? sortConfig.direction || '' : ''
                            } ${fieldsToShow.includes(index) ? '' : 'fieldToHide'} kanban`} 
                    >
                        
                        {currentStatus === status.id ? (
                            <div className="card"
                                ref={dragItem}
                                onDragEnd={(event) => dropIt(event, status)}
                                onDrop={(event) => dropIt(event, status)}
                                onDragStart={ (event) => dragStart(event)}
                                onDragEnter={(event) => dragEnter(event)}
                                key={`draggable-${index}`}
                                draggable
                            >
                                {onlyShowTaskName ? (
                                    <div className="clean">
                                        {settings?.taskName || ''}
                                    </div>
                                ) :
                                    (
                                        <Fragment>
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
                                                        onBlur={
                                                            cardIngressField.fieldType !== 'autocomplete' && cardIngressField.fieldType !== 'dropdown' && cardIngressField.fieldType !== 'productimageupload'
                                                                ? handleSubmit(onBlur)
                                                                : null
                                                        }
                                                        onChange={
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
                                        </Fragment>
                                    )
                                }

                                </div>
                        ) : (
                                <div className="fill">&nbsp;</div>
                        )}

                    </td>
                ))}
            </tr>
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
