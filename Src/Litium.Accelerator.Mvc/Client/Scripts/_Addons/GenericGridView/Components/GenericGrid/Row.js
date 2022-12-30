import React, { useEffect } from 'react';
import PropTypes, { object } from 'prop-types';
import classNames from 'classnames';
import { useForm } from 'react-hook-form';
import { GenericGridField } from './Field';
import { any } from 'array-flat-polyfill';

/**
 *
 * @param {Array} fields - List of columns to display in the grid row.
 * @param {Array} editableFieldIds - List of editable columns that can be updated in the grid.
 */

export const GenericGridRow = React.memo(
    ({
        fields = [],
        onChange,
        error,
        isLoading = false,
        identifierFieldId,
        rowIndex,
        fieldsToShow,
    }) => {
        const { register, handleSubmit, reset, formState: { dirtyFields, isSubmitted }, } = useForm();

        const onBlur = (form) => {
            const identifierField = { EntitySystemId: fields[0].entitySystemId };
            // Only submit changed field(s) along with identifier field
            if (Object.keys(dirtyFields).length) {
                const data = Object.keys(dirtyFields).reduce((payload, key) => {
                    reset({ [key]: form[key] });
                    return {
                        ...payload,
                        [key]: form[key],
                    };
                }, identifierField);

                console.log(' On-Blur ', data, fields);
                ////Actually, submit also other quantity field if both exists
                //if (Object.keys(dirtyFields).find((key) => key === 'Quantity')) {
                //    if (form.QuantityPallet) {
                //        data.QuantityPallet = form.QuantityPallet;
                //        data.QuantityForCustomer = form.QuantityForCustomer;
                //    }
                //}
                //if (Object.keys(dirtyFields).find((key) => key === 'QuantityPallet')) {
                //    if (form.Quantity) {
                //        data.Quantity = form.Quantity;
                //        data.QuantityForCustomer = form.QuantityForCustomer;
                //    }
                //}
                //if (Object.keys(dirtyFields).find((key) => key === 'CreditQuantity')) {
                //    console.log(data);
                //    data.CreditReason = form.CreditReason;
                //    data.CreditReasonDescription = form.CreditReasonDescription;
                //}
                //if (Object.keys(dirtyFields).find((key) => key === 'CreditReason')) {
                //    data.CreditQuantity = form.CreditQuantity;
                //    data.CreditReasonDescription = form.CreditReasonDescription;
                //}
                //if (
                //    Object.keys(dirtyFields).find(
                //        (key) => key === 'CreditReasonDescription'
                //    )
                //) {
                //    data.CreditReason = form.CreditReason;
                //    data.CreditQuantity = form.CreditQuantity;
                //}
                onChange(data, fields);
            }
        };

        // Reset row form state when fields are updated
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
                        <td colSpan={fields.length} className="generic-grid-view__error">
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
                                {...register(fieldName)}
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
                                        'generic-grid-view__error': error,
                                    },
                                    `${fieldsToShow.includes(fieldIndex) ? '' : 'fieldToHide'}`
                                )}
                            >
                                {settings.fieldMessage && (
                                    <span
                                        className="generic-grid-view__field-message"
                                        dangerouslySetInnerHTML={{ __html: settings.fieldMessage }}
                                    ></span>
                                )}
                                <GenericGridField
                                    isEditable={settings && !settings.readOnly}
                                    type={fieldType}
                                    suffix={fieldSuffix}
                                    defaultValue={cleanData(fieldValue, fieldType) || ''}
                                    title={cleanData(fieldValue, fieldType) || ''}
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
                                    rowIndex={rowIndex}
                                    fieldId={fieldID}
                                    fieldSettings={settings}
                                    ref={register}
                                    dropDownOptions={
                                        fieldType === 'dropdown' || fieldType === 'productimageupload' ? dropDownOptions : null
                                    }
                                />
                                {settings.errorFieldMessage && (
                                    <span
                                        className="generic-grid-view__error-field-message"
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

GenericGridRow.propTypes = {
    fields: PropTypes.array,
    onChange: PropTypes.func,
    error: PropTypes.any,
    isLoading: PropTypes.bool,
    editableFieldIds: PropTypes.arrayOf(PropTypes.string),
    identifierFieldId: PropTypes.string,
};

GenericGridRow.displayName = 'GenericGridRow';
