import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
export const IconField = ({
    common, fieldSettings, isEditable, defaultValue, onFocus, fieldId,
    ...props },
    ref
) => {
        return (
            <Fragment>
                {defaultValue && defaultValue.length > 0 && <i className={defaultValue} title={fieldSettings.buttonText || ''}></i>}
            </Fragment>
        );
}

const mapStateToProps = ({ genericDataView }) => genericDataView;
IconField.displayName = 'IconField';
export default connect(mapStateToProps)(IconField);