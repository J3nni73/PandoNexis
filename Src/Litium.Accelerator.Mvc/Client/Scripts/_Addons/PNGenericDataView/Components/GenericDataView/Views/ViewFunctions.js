import React, { useEffect, useState, Fragment } from 'react';
export const FieldErrorMsg = ({ errObjs, fieldID }) => {
    const errObj = errObjs.find(x => x.fieldID === fieldID);
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
        fieldID: key,
        fieldValue: form[key],
        [key]: form[key],
    };
};