import React, { useEffect, useState, Fragment } from 'react';
export const FieldErrorMsg = ({ errObjs, fieldId }) => {
    const errObj = errObjs.find(x => x.fieldId === fieldId);

    if (!errObj) {
        return null;
    }
    return (
        <div className="generic-data-view__error-field">{errObj.errorMessage}</div>
    );
};

export const getFieldData = (payload, form, key) => {
    return {
        ...payload,
        fieldId: key,
        fieldValue: form[key],
        [key]: form[key],
    };
};