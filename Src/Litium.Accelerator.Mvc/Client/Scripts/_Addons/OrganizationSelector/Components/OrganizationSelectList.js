import React, { Fragment, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { changeOrganization } from '../Actions/OrganizationSelector.action';

import { translate } from '../../../Services/translation';

const OrganizationSelectList = ({ organizationList, onOrgSubmit, translate, selectedOrg, selectOnChange }) => {
    const dispatch = useDispatch();
    const changeOrg = (orgId) => {
        if (orgId !== selectedOrg.id) {
            if (changeOrganization) {
                dispatch(changeOrganization(orgId));
            }
        }
    };

    return (
        <div className="select-org__interaction">
            <select className="select-org-list" onChange={event => changeOrg(event.target.value)} id="selectOrganizationList" value={selectedOrg.id} >
                {organizationList && organizationList.map((item, index, array) => (
                    <Fragment key={`${item.name}-${index}`}>
                        <option key={index} value={item.id}>{item.name}</option>
                    </Fragment>
                ))}
            </select>
        </div>
    );
};

export default OrganizationSelectList;
