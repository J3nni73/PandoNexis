import React, { useEffect, useState, Fragment, useRef } from 'react';
import { useSelector, useDispatch, connect } from 'react-redux';
import PropTypes, { object } from 'prop-types';
import classNames from 'classnames';
import { useForm } from 'react-hook-form';
import { GenericDataViewField } from '../../Field';
import { any } from 'array-flat-polyfill';
import { FieldErrorMsg, getFieldData } from '../viewFunctions';
import { checkFormField, sendContainerState } from '../../../../Actions/GenericDataContainer.action';
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
        const [containerState, setContainerState] = useState(settings?.containerState);
        const [nextAvailableStatus, setNewAvailableStatus] = useState(settings?.possibleContainerStateTransitions);
        const [cardIngressField, setCardIngressField] = useState(null);
        const [errorObject, setErrorObject] = useState(null);
        const dispatch = useDispatch();
        const dragItem = useRef();
        const rowItem = useRef();
        const dragOverItem = useRef();
        let tempState = null;
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
            if (!errObjs) {
                dispatch(isContainerValid());
            }
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

        //// If there is and error reset form fields
        //useEffect(() => {
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

        const dragStart = (e, status) => {
            e.dataTransfer.setData("text/plain", 'Dragme');
            e.dataTransfer.effectAllowed = "copyMove";
            e.target.style.cursor = "move";
            window.currentDragIndx = dragItem.current.dataset.dragIndex;
        };

        const dragEnter = (e, status) => {
            e.preventDefault();

            const isAvailable = nextAvailableStatus.indexOf(status.id) !== -1;

        };
        const dragLeave = (e, status) => {
            e.preventDefault();
            const element = e.target;
            element.classList.remove('drag-over');
            element.classList.remove('drag-forbidden');
            e.dataTransfer.effectAllowed = "move";
        };
        const dragOver = (e, status, leaving = false) => {
            e.preventDefault();
            e.UseDefaultCursors = true;
            const isAvailable = nextAvailableStatus.indexOf(status.id) !== -1;
            if (status.id !== containerState && isAvailable) {
                const element = e.target;
                const containerHolderEl = element.closest('.kb-container-holder');
                const containerRef = containerHolderEl?.dataset.containerRef;
                const isWithinContainer = containerRef ? containerRef === window.currentDragIndx : false;

                if (status.id !== containerState && isAvailable && isWithinContainer) {
                    e.target.classList.add('drag-over');
                    e.dataTransfer.effectAllowed = "copy";
                }
                else {
                    if (!isWithinContainer) {
                        // Try to mark 
                    }
                    e.target.classList.add('drag-forbidden');
                    e.dataTransfer.effectAllowed = "none";
                }
                e.dataTransfer.dropEffect = "copy";
                if (element) {
                    if (leaving) {
                        //element.classList.remove("drag-over");
                    }
                    else {
                        // element.classList.add("drag-over");
                        tempState = status.id;
                        e.UseDefaultCursors = false;
                    }
                }
            }
            else {
                e.dataTransfer.dropEffect = "none";
            }
        };

        const dropIt = (e, status) => {
            //console.log(tempState);
            //console.log(containerState);
            const element = e.target;
            const containerHolder = element.parentNode.parentNode;
            //alert(element.parentNode.outerHTML);
            const kanbanColumns = containerHolder.querySelectorAll('.kanban');
            if (kanbanColumns) {
                Array.from(kanbanColumns).forEach(
                    (kanban) => {
                        if (kanban) {
                            kanban.classList.remove('drag-over');
                            kanban.classList.remove('drag-forbidden');
                        }
                    }
                );
            }
            if (tempState && tempState !== containerState) {
                const entitySystemId = fields[0]?.entitySystemId;
                dispatch(sendContainerState(entitySystemId, tempState));
                setContainerState(tempState);
                tempState = null;
                element.style.cursor = 'default'; // Reset cursor
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
            <tr className="kb-container-holder" data-container-ref={`ref-${dataContainerIndex}`}>
                {error && (
                    <div colSpan={fields.length} className="generic-data-view__error">
                        {error.message || error.toString() || 'There was an error.'}
                    </div>
                )}

                {mainSettings?.genericDataContainerStateList?.length > 0 && mainSettings.genericDataContainerStateList.map((status, index) => (
                    <td
                        onDragEnter={(event) => dragEnter(event, status)}
                        onDragOver={(event) => dragOver(event, status)}
                        onDragLeave={(event) => dragLeave(event, status)}

                        key={`header-${index}`}
                        style={{ backgroundColor: status.backgroundColor || null }}
                        onClick={() => sortColumn(status.name, index)}
                        className={`${sortConfig && sortConfig?.name === status.name ? sortConfig.direction || '' : ''
                            } ${fieldsToShow.includes(index) ? '' : 'fieldToHide'} kanban`}
                    >

                        {containerState === status.id ? (
                            <div className="card"
                                ref={dragItem}
                                onDragEnd={(event) => dropIt(event, status)}
                                onDrop={(event) => dropIt(event, status)}
                                onDragStart={(event) => dragStart(event)}
                                key={`draggable-${index}`}
                                data-drag-index={`ref-${dataContainerIndex}`}

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
                                                <div {...register(cardIngressField.fieldId)}>
                                                    <GenericDataViewField
                                                        isEditable={cardIngressField.settings && cardIngressField.settings.editable}
                                                        type={cardIngressField.fieldType}
                                                        suffix={cardIngressField.fieldSuffix}
                                                        defaultValue={cleanData(cardIngressField.fieldValue, cardIngressField.fieldType) || ''}
                                                        title={cardIngressField.fieldName}
                                                        name={cardIngressField.fieldId}
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
                                                        fieldId={cardIngressField.fieldId}
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
                                                            fieldId,
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
                                                                    'columns': true,
                                                                    'generic-data-view__error': error,
                                                                    'fieldToHide': !fieldsToShow.includes(fieldIndex),
                                                                    'nowrap': !settings.wrapField
                                                                },
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
                                                                name={fieldId}
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
                                                                nextEntitySystemId={fieldIndex !== cardFields.length - 1 ? [fieldIndex + 1].entitySystemId : '-1'}
                                                                dataContainerIndex={dataContainerIndex}
                                                                fieldId={fieldId}
                                                                fieldSettings={settings}
                                                                ref={register}
                                                                dropDownOptions={
                                                                    fieldType === 'dropdown' || fieldType === 'productimageupload' ? dropDownOptions : null
                                                                }
                                                            />
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
                                        </Fragment>
                                    )
                                }
                            </div>
                        ) : (
                            null
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
