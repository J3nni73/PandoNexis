import React, { Component, Fragment } from 'react';
import { connect, useDispatch } from 'react-redux';
import { translate } from '../../../../../Services/translation';
import { buttonClick } from '../../../Actions/GenericDataContainerField.action';
import { loadModal } from '../../../Actions/GenericDataView.action';

export const ButtonField = ({
    type, fieldId, isEditable = false, fieldSettings, entitySystemId, dataContainerIndex, suffix, defaultValue, title, dropDownOptions, useConfirmation, confirmationText, 
    ...props }
) => {
    const dispatch = useDispatch();
    const postValue = () => {
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
                                onClick={(event) => this.postValue()}
                                title={ defaultValue || fieldSettings.placeholderText || ''}
                            ></i>
                        ) : (
                            <button
                                className="generic-data-view__button-field-post-button"
                                onClick={(event) => postValue()}
                            >
                                {fieldSettings.buttonText}
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
