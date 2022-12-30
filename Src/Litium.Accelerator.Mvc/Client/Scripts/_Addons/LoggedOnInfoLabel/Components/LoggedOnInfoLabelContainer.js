import React, { Fragment, useEffect, useState, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { getPersonInfo } from '../Actions/LoggedOnInfoLabel.action';
import { translate } from '../../../Services/translation';
//import FolderOpenIcon from '../Icons/folder_open_fill.svg?component';

const LoggedOnInfoLabelContainer = ({ heading, getData = false }) => {
    const dispatch = useDispatch();
    const { personInfo } = useSelector((state) => state.loggedOnInfoLabel);

    useEffect(() => {
        if (getData && getPersonInfo) {
            dispatch(getPersonInfo());
        }
    }, []);

    if (personInfo === null) {
        return null;
    }
    if (!personInfo) {
        return null;
    }
    return (
        <div className={`pn-info-label__person-info`}>
            {heading && 
                <h3>{heading}</h3>
            }
            {`${personInfo.firstName} ${personInfo.surname}`}
            {personInfo.additionalInfo && 
                <div dangerouslySetInnerHTML={{ __html: personInfo.additionalInfo }}></div>
            }
        </div>
    );
};
export default LoggedOnInfoLabelContainer;
