import React, { Fragment, useEffect, useState, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { getPersonInfo } from '../Actions/LoggedOnInfoLabel.action';
import { translate } from '../../../Services/translation';
//import FolderOpenIcon from '../Icons/folder_open_fill.svg?component';

const LoggedOnInfoLabelContainer = ({ heading, getData = false }) => {
    const dispatch = useDispatch();
    const { personInfo } = useSelector((state) => state.loggedOnInfoLabel);
    const [infoLabel, setInfoLabel] = useState('');
    const [initLabel, setInitLabel] = useState(false);
    useEffect(() => {
        if (getData && getPersonInfo) {
            dispatch(getPersonInfo());
        }
    }, []);

    
    //const updateLabel = (infoLabelFull, infoLabelPartial, index) => {
    //    if (index < (infoLabelFull.length)) {
    //        infoLabelPartial += infoLabelFull.charAt(index);
    //        setInfoLabel(infoLabelPartial);
    //        setTimeout(() => {
    //            updateLabel(infoLabelFull, infoLabelPartial, index+1)
    //        }, 30);
    //    }
    //}

    useEffect(() => {
        if (personInfo && !initLabel) {
            const infoLabel = `${personInfo?.organizationName ? personInfo?.organizationName + ' - ' : ''} ${personInfo?.firstName || ''} ${personInfo?.surname || ''}`;
            if (infoLabel.length > 2) {
                setInitLabel(true);
                setInfoLabel(infoLabel);
                //updateLabel(infoLabel, '', 0);
            }
        }
    }, [personInfo]);

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
            {infoLabel}
            {personInfo.additionalInfo &&
                <div dangerouslySetInnerHTML={{ __html: personInfo.additionalInfo }}></div>
            }
        </div>
    );
};
export default LoggedOnInfoLabelContainer;
