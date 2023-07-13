import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
//import AutocompleteField from './SpecialFields/Autocomplete';
import ButtonField from './SpecialFields/ButtonField';
import DropdownField from './SpecialFields/DropdownField';
import DatePickerField from './SpecialFields/DatePickerField';
import IconField from './SpecialFields/IconField';
//import OrganizationSelection from './SpecialFields/OrganizationSelection';
//import ProductImageUpload from './SpecialFields/ProductImageUpload';

export const GenericDataViewField = React.forwardRef(
    (
        { type, fieldId, isEditable = false, fieldSettings, entitySystemId, nextEntitySystemId = '-1', dataContainerIndex, suffix, title, dropDownOptions, setErrorObject, onButtonClick, genericButtons, onCheckboxChange,
            ...props
        },
        ref
    ) => {
        let isRequired = false;
        const [isChecked, setIsChecked] = useState(props.defaultValue !== "false" ? true : false);

        if (fieldSettings && fieldSettings.validationRules) {
            isRequired = fieldSettings.validationRules.find(x => x.rule === 'IsRequired') ? true : false;
        }

        const handleChecked = (e) => {
            window.currGenDW_isTrue = !isChecked;
            onCheckboxChange(e);
            setIsChecked(!isChecked);
        };

        const onEnterKeyPress = function (event, isClick) {
            const currentElementName = fieldId;
            //if (isClick) {
            window.currGenDW_lastClickedFieldId = currentElementName;
            window.currGenDW_lastClickedEntitySystemId = entitySystemId;
            //}
            if ((event.keyCode === 13 || event.keyCode === 9)) { //&& event.target.nodeName === 'INPUT') {

                let parent = event.target.closest('tr');
                if (!parent) {
                    parent = event.target.closest('.row');
                }

                focusNextElement(parent, entitySystemId);
            }
        };

        const checkElements = (siblingFields, entSysId) => {
            let foundEl = false;
            for (let i = 0; i < siblingFields.length; i++) {
                //  siblingFields.Each(theEl => {
                const theEl = siblingFields[i];
                if (foundEl) {
                    window.currGenDW_lastClickedFieldId = theEl.name;
                    window.currGenDW_lastClickedEntitySystemId = entSysId;
                    return;
                }

                if (theEl.name === fieldId) {
                    foundEl = true;
                }
            }

            return foundEl;
        };

        const focusNextElement = function (parentEl) {
            // capture all elements containing data-gdv-field
            const siblingFields = parentEl.querySelectorAll('[data-gdv-field]');

            // If not found then take next row
            if (!checkElements(siblingFields, entitySystemId)) {
                const nextSibling = parentEl.nextSibling;
                if (nextSibling) {
                    checkElements(nextSibling, nextEntitySystemId);
                }
            }
        };
        let common = {
            ...ref,
            className: 'generic-data-view__input',
            ...props,
        };



        //useEffect(() => {
        //    if (common.ref?.current) {
        //        onCheckboxChange(common.ref.current);
        //        aler(3);
        //    }

        //}, [isChecked]);
        // First Special Fields (independent of isEditable)
        if (genericButtons?.length > 0) {
            return (
                <div className="generic-data-view__btn-group"
                    {...ref}>
                    {genericButtons.map((button, index) => (
                        <ButtonField
                            key={`button-group${index}-${dataContainerIndex}`}
                            fieldId={fieldId}
                            entitySystemId={button.entitySystemId || entitySystemId}
                            dataContainerIndex={dataContainerIndex}
                            fieldSettings={button}
                            defaultValue={props.fieldName || ''}
                            setErrorObject={setErrorObject}
                            title={button.buttonText}
                            onButtonClick={(event) => onButtonClick(event)}
                            onKeyDown={(event) => onEnterKeyPress(event)}
                            onClick={(event) => onEnterKeyPress(event)}
                            autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}

                        />
                    ))}

                </div>
            );
        }

        // First Special Fields (independent of isEditable)
        if (type === 'button') {

            return (
                <ButtonField
                    fieldId={fieldId}
                    entitySystemId={entitySystemId}
                    dataContainerIndex={dataContainerIndex}
                    fieldSettings={fieldSettings}
                    defaultValue={props.fieldName || ''}
                    setErrorObject={setErrorObject}
                    title={name}
                    onButtonClick={onButtonClick}
                    onKeyDown={(event) => onEnterKeyPress(event)}
                    onClick={(event) => onEnterKeyPress(event)}
                    autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}

                    {...ref}
                />
            );
        }
        else if (type === 'TextOption' || type === 'dropdown') {
            if (!common.defaultValue) {
                common.defaultValue = '-1';
            }
            return (
                <DropdownField
                    fieldId={fieldId}
                    entitySystemId={entitySystemId}
                    common={common}
                    {...ref}
                    onChange={props.onChange}
                    onBlur={props.onBlur}
                    dropDownOptions={dropDownOptions}
                    isEditable={isEditable}
                    dataContainerIndex={dataContainerIndex}
                    fieldSettings={fieldSettings}
                    defaultValue={props.defaultValue || ''}
                    onFocus={(event) => onEnterKeyPress(event)}
                    title={props.defaultValue}
                    autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}
                />
            );
        }
        //else if (type === 'autocomplete') {
        //    return (
        //        <AutocompleteField
        //            fieldId={fieldId}
        //            entitySystemId={entitySystemId}
        //            dataContainerIndex={dataContainerIndex}
        //            fieldSettings={fieldSettings}
        //            defaultValue={props.defaultValue || ''}
        //            title={props.defaultValue}
        //        />
        //    );
        else if (type === 'icon') {
            return (
                <IconField
                    defaultValue={props.defaultValue || ''}
                    fieldId={fieldId}
                    entitySystemId={entitySystemId}
                    dataContainerIndex={dataContainerIndex}
                    fieldSettings={fieldSettings}
                />
            );
        }
        //else if (type === 'organizationselection') {
        //    return (
        //        <OrganizationSelection
        //            fieldId={fieldId}
        //            entitySystemId={entitySystemId}
        //            dataContainerIndex={dataContainerIndex}
        //            fieldSettings={fieldSettings}
        //            defaultValue={props.defaultValue || ''}
        //            title={props.defaultValue}
        //        />
        //    );
        //} else if (type === 'productimageupload') {
        //    return (
        //        <ProductImageUpload
        //            fieldId={fieldId}
        //            entitySystemId={entitySystemId}
        //            onChange={props.onChange}
        //            dropDownOptions={dropDownOptions}
        //            isEditable={isEditable}
        //            dataContainerIndex={dataContainerIndex}
        //            fieldSettings={fieldSettings}
        //            defaultValue={props.defaultValue || ''}
        //        />
        //    );
        else if (type === 'datetime') {
            return (
                <DatePickerField
                    data-gdv-field
                    fieldId={fieldId}
                    readOnly={!isEditable}
                    autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}
                    {...common} />
            );
        }

        else if (type === 'radiobutton') {
            return (
                <label className="generic-data-view__radiobutton-wrapper">
                    <input
                        data-gdv-field
                        name={fieldId}
                        readOnly={!isEditable}
                        required={isRequired}
                        className="generic-data-view__radiobutton"
                        autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}
                        onMouseDown={(event) => onEnterKeyPress(event, true)}
                        type="radiobutton" {...ref} /><span>{fieldSettings?.placeholderText || title}</span></label>
            );
        }
        else if (type === 'checkbox') {
            return (
                <label className="generic-data-view__checkbox-wrapper">
                    <input
                        data-gdv-field
                        name={fieldId}
                        required={isRequired}
                        readOnly={!isEditable}
                        className="generic-data-view__checkbox"
                        onChange={(e) => handleChecked(e)}
                        onMouseDown={(event) => onEnterKeyPress(event, true)}
                        checked={isChecked}
                        type="checkbox"
                        autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}
                        key={`test${fieldId}${entitySystemId}${isChecked}`} {...ref}
                    /><span>{fieldSettings?.placeholderText || title}</span></label>
            );
        }
        else if (type === 'html') {
            return (
                <div dangerouslySetInnerHTML={{ __html: props.defaultValue }}></div>
            );
        }
        else {
            // Fields depending on status of isEditable
            /*console.log("Type = ", type);*/
            switch (isEditable ? type : 'default') {
                case 'string':
                    return (
                        <Fragment>
                            <input
                                data-gdv-field
                                onKeyDown={(event) => onEnterKeyPress(event)}
                                type="text"
                                required={isRequired}
                                readOnly={!isEditable}
                                autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}
                                onClick={(event) => onEnterKeyPress(event)}
                                onMouseDown={(event) => onEnterKeyPress(event, true)}
                                {...common}
                            />
                            {suffix}
                        </Fragment>
                    );
                case 'decimal':
                case 'int':
                    return (
                        <Fragment>
                            <input
                                data-gdv-field
                                onKeyDown={(event) => onEnterKeyPress(event)}
                                type="number"
                                required={isRequired}
                                readOnly={!isEditable}
                                onClick={(event) => onEnterKeyPress(event)}
                                onMouseDown={(event) => onEnterKeyPress(event, true)}
                                autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}
                                {...common}
                            />
                            {suffix}
                        </Fragment>
                    );
                case 'textarea':
                    return (
                        <textarea
                            defaultValue={props.defaultValue}
                            data-gdv-field
                            name={fieldId}
                            {...common}
                            autoFocus={window.currGenDW_lastClickedFieldId && window.currGenDW_lastClickedEntitySystemId && window.currGenDW_lastClickedEntitySystemId === entitySystemId && window.currGenDW_lastClickedFieldId === fieldId}
                        />
                    );

                default:
                    return (
                        <>
                            <input type="hidden" {...common} required={isRequired} />
                            <span className="generic-data-view__simple-text" title={props.defaultValue}>{props.defaultValue}
                                {suffix}</span>
                        </>
                    );
            }
        }


    }
);

GenericDataViewField.propTypes = {
    isEditable: PropTypes.bool,
    type: PropTypes.string,
};

GenericDataViewField.displayName = 'GenericDataViewField';
