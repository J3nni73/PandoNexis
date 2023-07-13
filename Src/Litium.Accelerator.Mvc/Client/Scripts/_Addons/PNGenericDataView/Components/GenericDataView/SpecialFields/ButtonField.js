import React, { Fragment } from 'react';
import { connect, useDispatch } from 'react-redux';
import { Tooltip as ReactTooltip } from 'react-tooltip';
import { translate } from '../../../../../Services/translation';
//import { useForm } from 'react-hook-form';

export const ButtonField = ({
    type, fieldId, isEditable = false, fieldSettings, entitySystemId, dataContainerIndex, suffix, defaultValue, title, dropDownOptions, useConfirmation, confirmationText, setErrorObject, onButtonClick, autoFocus,
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
        window.currGenDW_confirmationText = confirmationText;
        window.currGenDW_fieldId = fieldSettings.fieldId || fieldId;
        
        window.currGenDW_lastClickedFieldId = fieldSettings.fieldId || fieldId;
        window.currGenDW_lastClickedEntitySystemId = fieldSettings.entitySystemId;
        
        onButtonClick(e);
    };
    const rndNo = Math.floor(Math.random() * 89233);
    return (
        <div
            className="generic-data-view__button-field-container"

        >
            <div className="generic-data-view__button-field-button">
                {!fieldSettings.hideButton && (
                    <Fragment>
                        {fieldSettings.iconClass ? (
                            <i
                                id={`button-${fieldSettings.entitySystemId || entitySystemId}${fieldSettings.fieldId || fieldId}${rndNo}${dataContainerIndex || 0}`}
                                className={fieldSettings.iconClass}
                                onClick={(event) => buttonClick(event)}
                                title={defaultValue || fieldSettings.placeholderText || ''}
                            ></i>
                        ) : (
                            <button
                                id={`button-${fieldSettings.entitySystemId || entitySystemId}${fieldSettings.fieldId || fieldId}${rndNo}${dataContainerIndex || 0}`}
                                className={`generic-data-view__button-field-post-button ${fieldSettings.className} ${fieldSettings.hideButton ? 'hide' : ''}`}
                                onClick={(event) => buttonClick(event)}
                                {...autoFocus}
                            >{fieldSettings.buttonText}
                            </button>
                        )}
                        {fieldSettings.fieldTooltipMessage && fieldSettings.fieldTooltipMessage.length > 0 &&
                            <ReactTooltip className="generic-data-view__tooltip" float={true} delayShow="800" delayHide="300" anchorId={`button-${fieldSettings.entitySystemId || entitySystemId}${fieldSettings.fieldId || fieldId}${rndNo}${dataContainerIndex || 0}`} variant={fieldSettings.fieldTooltipType || "dark"} positionStrategy="fixed" offset="32" place="left">
                                {fieldSettings.fieldTooltipMessage}
                            </ReactTooltip>
                        }
                    </Fragment>
                )}
            </div>
        </div>
    )
};


const mapStateToProps = ({ genericDataView }) => genericDataView;
ButtonField.displayName = 'ButtonField';
export default connect(mapStateToProps)(ButtonField);
