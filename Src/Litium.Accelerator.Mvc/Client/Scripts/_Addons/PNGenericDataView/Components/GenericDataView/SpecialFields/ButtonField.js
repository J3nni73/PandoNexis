import React, { Component, Fragment } from 'react';
import { connect, useDispatch } from 'react-redux';
import { translate } from '../../../../../Services/translation';
//import { useForm } from 'react-hook-form';

export const ButtonField = ({
    type, fieldId, isEditable = false, fieldSettings, entitySystemId, dataContainerIndex, suffix, defaultValue, title, dropDownOptions, useConfirmation, confirmationText, setErrorObject, onButtonClick,
    ...props },
    ref
) => {
    const dispatch = useDispatch();
    //const { handleSubmit, getValues, dirtyFields } = useForm();
    //console.log("theFormValues", JSON.stringify(theFormValues));
    const buttonClick = (e) => {
        // Check validation
        window.currGenDW_useConfirmation = useConfirmation;
        window.currGenDW_fieldSettings = fieldSettings;
        window.currGenDW_fieldSettings = fieldSettings;
        window.currGenDW_confirmationText = confirmationText;
        window.currGenDW_fieldId = fieldId;

        onButtonClick(e);
    };

    return (
        <div
            className="generic-data-view__button-field-container"
           
        >
            <div className="generic-data-view__button-field-button">
                {!fieldSettings.hideButton && (
                    <Fragment>
                        {fieldSettings.iconClass ? (
                            <i
                                className={fieldSettings.iconClass}
                                onClick={(event) => buttonClick(event)}
                                title={defaultValue || fieldSettings.placeholderText || ''}
                            ></i>
                        ) : (
                            <button
                                className="generic-data-view__button-field-post-button"
                                    onClick={(event) => buttonClick(event)}
                            >{fieldSettings.buttonText}
                            </button>
                        )}
                    </Fragment>
                )}
            </div>
        </div>
    )};


    const mapStateToProps = ({ genericDataView }) => genericDataView;
    ButtonField.displayName = 'ButtonField';
    export default connect(mapStateToProps)(ButtonField);
