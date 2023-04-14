import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
//import AutocompleteField from './SpecialFields/Autocomplete';
import ButtonField from './SpecialFields/ButtonField';
import DropdownField from './SpecialFields/DropdownField';
//import IconField from './SpecialFields/IconField';
//import OrganizationSelection from './SpecialFields/OrganizationSelection';
//import ProductImageUpload from './SpecialFields/ProductImageUpload';

export const GenericDataViewField = React.forwardRef(
    (
        { type, fieldId, isEditable = false, fieldSettings, entitySystemId, dataContainerIndex, suffix, title, dropDownOptions,
            ...props
        },
        ref
    ) => {
        const onEnterKeyPress = function (event) {
            if (event.keyCode === 13 && event.target.nodeName === 'INPUT') {
                var parent = event.target.closest('tr');
                var currentElementName = event.target.name;
                var matchingInput = nextUntil(
                    parent,
                    'input[name=' + currentElementName + ']'
                );
                if (matchingInput) {
                    matchingInput.focus();
                }
            }
        };
        const nextUntil = function (elem, selector) {
            if (!Element.prototype.matches) {
                Element.prototype.matches =
                    Element.prototype.msMatchesSelector ||
                    Element.prototype.webkitMatchesSelector;
            }
            elem = elem.nextElementSibling;

            while (elem) {
                if (
                    elem.querySelector(selector) &&
                    elem.querySelector(selector).type != 'hidden'
                ) {
                    return elem.querySelector(selector);
                }

                elem = elem.nextElementSibling;
            }
        };

        const common = {
            ...ref,
            className: 'generic-data-view__input',
            ...props,
        };

        // First Special Fields (independent of isEditable)
        if (type === 'button') {
            
            return (
                <ButtonField
                    fieldId={fieldId}
                    entitySystemId={entitySystemId}
                    dataContainerIndex={dataContainerIndex}
                    fieldSettings={fieldSettings}
                    defaultValue={props.fieldName || ''}
                    title={name}
                    {...common}
                />
            );
        }
      
        //else if (type === 'TextOption' || type === 'dropdown') {
        //    return (
        //        <DropdownField
        //            fieldId={fieldId}
        //            entitySystemId={entitySystemId}
        //            common={common}
        //            ref={ref}
        //            onChange={props.onChange}
        //            onBlur={props.onBlur}
        //            options={dropDownOptions}
        //            isEditable={isEditable}
        //            dataContainerIndex={dataContainerIndex}
        //            fieldSettings={fieldSettings}
        //            defaultValue={props.defaultValue || ''}
        //            title={props.defaultValue}
        //        />
        //    );
        //}
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
        //else if (type === 'dropdown' || type==='TextOption') {
        //    return (
        //        <DropdownField
        //            fieldId={fieldId}
        //            entitySystemId={entitySystemId}
        //            common={common}
        //            ref={ref}
        //            onChange={props.onChange}
        //            onBlur={props.onBlur}
        //            dropDownOptions={dropDownOptions}
        //            isEditable={isEditable}
        //            dataContainerIndex={dataContainerIndex}
        //            fieldSettings={fieldSettings}
        //            defaultValue={props.defaultValue || ''}
        //            title={props.defaultValue}
        //        />
        //    );
        //} else if (type === 'icon') {
        //    return (
        //        <IconField
        //            defaultValue={props.defaultValue || ''}
        //            fieldId={fieldId}
        //            entitySystemId={entitySystemId}
        //            dataContainerIndex={dataContainerIndex}
        //            fieldSettings={fieldSettings}

        //        />
        //    );
        //} else if (type === 'organizationselection') {
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
        else if(type === 'html') {
            return (
                <div dangerouslySetInnerHTML={{ __html: props.defaultValue }}></div>
            );
        }
        else
        {
            // Fields depending on status of isEditable
            /*console.log("Type = ", type);*/
            switch (isEditable ? type : 'default') {
                case 'string':
                    return (
                        <Fragment>
                            <input
                                onKeyDown={(event) => onEnterKeyPress(event)}
                                type="text"
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
                                onKeyDown={(event) => onEnterKeyPress(event)}
                                type="number"
                                {...common}
                            />
                            {suffix}
                        </Fragment>
                    );
                default:
                    return (
                        <>
                            <input type="hidden" {...common} />
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
