import React, { useEffect, useState } from 'react';
import PropTypes, { object } from 'prop-types';
import classNames from 'classnames';
import { useForm } from 'react-hook-form';
import { GenericDataViewField } from '../../Field';
import { any } from 'array-flat-polyfill';

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
        const { register, handleSubmit, reset, formState: { dirtyFields, isSubmitted }, } = useForm();
        const [cardFields, setCardFields] = useState([]);
        const [cardIngressField, setCardIngressField] = useState(null);

        const onBlur = (form) => {
            const identifierField = { EntitySystemId: fields[0].entitySystemId };

            // Only submit changed field(s) along with identifier field
            if (Object.keys(dirtyFields).length) {
                const data = Object.keys(dirtyFields).reduce((payload, key) => {
                    reset({ [key]: form[key] });
                    return {
                        ...payload,
                        fieldID: key,
                        fieldValue: form[key],
                        [key]: form[key],
                    };
                }, identifierField);
                onDataContainerChange(data, fields, isInModal);
            }
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
                        {error.message || error.toString() || 'There was an error.'}
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
