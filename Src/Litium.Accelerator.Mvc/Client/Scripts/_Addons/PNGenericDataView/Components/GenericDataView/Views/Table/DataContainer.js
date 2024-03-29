import React, { useEffect, useState, useRef } from 'react';
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

import { translate } from '../../../../../../Services/translation';
import { Tooltip as ReactTooltip } from 'react-tooltip';
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
    settings,
    mainSettings,
  }) => {
    const {
      register,
      handleSubmit,
      setFocus,
      reset,
      formState: { dirtyFields, isSubmitted },
    } = useForm();
    const [containerSettings, setContainerSettings] = useState(settings);
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
        //console.log('HEPP');
        const FullData = Object.keys(theFormFields).reduce((payload, key) => {
          //console.log("key", form[key]);
          //console.log('key', key + ':  ' + form[key]);
          //if (key === 'customer') {
          //    alert();
          //}
          var errObj = checkFormField({
            fieldId: key,
            fieldValue: form[key],
            field: fields.find((x) => x.fieldId === key),
          });

          if (errObj) {
            errorObjects.push(errObj);
            return null;
          } else {
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

    const onCheckboxChange = (
      form,
      fieldId,
      entitySystemId,
      dataContainerIndex,
      settings
    ) => {
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
    };

    const validateForm = (form) => {
      const entitySystemId = fields[0].entitySystemId;
      const identifierField = { entitySystemId };
      const errObjs = isContainerValid(identifierField, form);
      setErrorObject(errObjs);
    };
    const setFields = (form) => {
      const entitySystemId = fields[0].entitySystemId;
      const identifierField = { entitySystemId };
      if (Object.keys(dirtyFields).length) {
        const data = Object.keys(dirtyFields).reduce((payload, key) => {
          reset({ [key]: form[key] });
          return {
            ...payload,
            [key]: form[key],
          };
        }, identifierField);
      }
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
      setFields(form);
      const selectedValueObject = {
        value: '',
        name: '',
        entitySystemId: entitySystemId,
        dataContainerIndex,
        postContainerPageSystemId:
          containerSettings?.postContainerPageSystemId || null,
        form: containerSettings?.postContainer ? form : null,
      };

      dispatch(
        buttonClick(
          fieldId,
          dataContainerIndex,
          selectedValueObject,
          isInModal,
          fieldSettings,
          fieldSettings.pageSystemId
        )
      );
    };

    const updateVal = (form, getWinValue = false) => {
      if (getWinValue) {
        form[window.currGenDW_lastClickedFieldId] =
          window.currGenDW_lastClickedFieldValue;
      }
      setFields(form);
    };
    const onBlur = (form, getWinValue = false) => {
      if (getWinValue) {
        form[window.currGenDW_lastClickedFieldId] =
          window.currGenDW_lastClickedFieldValue;
      }

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
              {error.status + ' ' || ''}
              {error.title || error.toString() || 'There was an error.'}
            </td>
          </tr>
        )}
        <tr>
          {fields.map(
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
              <td
                key={`field-${fieldIndex}-${fieldName}`}
                id={`${entitySystemId}${fieldId}${dataContainerIndex}`}
                colSpan={`${
                  fieldIndex === fields.length - 1 &&
                  !(
                    fields &&
                    containerSettings &&
                    containerSettings?.postContainer &&
                    containerSettings?.postContainerPageSystemId
                  )
                    ? 100
                    : null
                }`}
                {...register(fieldId)}
                style={
                  isLoading
                    ? {
                        opacity: 0.5,
                        backgroundColor: settings.backgroundColor || null,
                      }
                    : { backgroundColor: settings.backgroundColor || null }
                }
                className={classNames({
                  'generic-data-view__error': error,
                  fieldToHide: !fieldsToShow.includes(fieldIndex),
                  nowrap: !settings.wrapField,
                })}
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
                  name={fieldId}
                  setErrorObject={setErrorObject}
                  onButtonClick={
                    fieldType === 'button' ||
                    settings.genericButtons?.length > 0
                      ? handleSubmit(onButtonClick)
                      : null
                  }
                  onCheckboxChange={
                    fieldType === 'checkbox' || fieldType === 'radiobutton'
                      ? handleSubmit((e) =>
                          onCheckboxChange(
                            e,
                            fieldId,
                            entitySystemId,
                            dataContainerIndex,
                            settings
                          )
                        )
                      : null
                  }
                  onBlur={
                    containerSettings?.postContainer
                      ? handleSubmit(validateForm)
                      : !containerSettings?.postContainer &&
                        fieldType !== 'autocomplete' &&
                        fieldType !== 'dropdown' &&
                        fieldType !== 'productimageupload' &&
                        fieldType !== 'radiobutton' &&
                        fieldType !== 'checkbox'
                      ? handleSubmit(onBlur)
                      : null
                  }
                  onChange={
                    !containerSettings?.postContainer &&
                    (fieldType === 'dropdown' ||
                      fieldType === 'productimageupload' ||
                      fieldType === 'datetime')
                      ? handleSubmit((e) => onBlur(e, true))
                      : handleSubmit((e) => updateVal)
                  }
                  aria-labelledby={fieldName}
                  entitySystemId={entitySystemId}
                  nextEntitySystemId={
                    fieldIndex !== fields.length - 1
                      ? [fieldIndex + 1].entitySystemId
                      : '-1'
                  }
                  dataContainerIndex={dataContainerIndex}
                  fieldId={fieldId}
                  fieldSettings={settings}
                  genericButtons={settings.genericButtons}
                  ref={register}
                  dropDownOptions={
                    fieldType === 'dropdown' ||
                    fieldType === 'productimageupload'
                      ? options
                      : null
                  }
                />
                {errorObject && (
                  <FieldErrorMsg fieldId={fieldId} errObjs={errorObject} />
                )}
                {settings.errorFieldMessage && (
                  <span
                    className="generic-data-view__error-field-message"
                    dangerouslySetInnerHTML={{
                      __html: settings.errorFieldMessage,
                    }}
                  ></span>
                )}
                {settings.fieldTooltipMessage &&
                  settings.fieldTooltipMessage.length > 0 && (
                    <ReactTooltip
                      className="generic-data-view__tooltip"
                      float={true}
                      delayShow="800"
                      delayHide="300"
                      anchorId={`${entitySystemId}${fieldId}${dataContainerIndex}`}
                      variant={settings.fieldTooltipType || 'dark'}
                      positionStrategy="fixed"
                      offset="32"
                      place="left"
                    >
                      {settings.fieldTooltipMessage}
                    </ReactTooltip>
                  )}
              </td>
            )
          )}
          {fields &&
            containerSettings &&
            containerSettings?.postContainer &&
            containerSettings?.postContainerPageSystemId && (
              <td colSpan="100">
                <div className="small-12 columns text--right">
                  <GenericDataViewField
                    type={'button'}
                    title={
                      containerSettings?.postContainerButtonText ||
                      translate('addons.genericdataview.buttons.generalpost')
                    }
                    name={'Post'}
                    setErrorObject={setErrorObject}
                    onButtonClick={handleSubmit(onButtonClick)}
                    aria-labelledby={
                      settings?.postContainerButtonText ||
                      translate('addons.genericdataview.buttons.generalpost')
                    }
                    entitySystemId={fields[0].entitySystemId}
                    dataContainerIndex={dataContainerIndex}
                    fieldId={'Post'}
                    fieldSettings={{
                      buttonText:
                        containerSettings?.postContainerButtonText ||
                        translate('addons.genericdataview.buttons.generalpost'),
                    }}
                    ref={useRef()}
                  />
                </div>
              </td>
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
