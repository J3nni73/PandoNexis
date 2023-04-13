import React, { Fragment, useEffect, useState, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import SelectOrganization from './SelectOrganization';
import LoggedOnInfoLabelContainer from '../../PNLoggedOnInfoLabel/Components/LoggedOnInfoLabelContainer';

import GlobeIcon from '../Icons/globe.svg?component';
//import Fuse from 'fuse.js'
//import { translate } from '../../../Services/translation';

//import Breadcrumbs from './Breadcrumbs';

const OrganizationSelectorContainer = () => {
    const [showingOrganisationSelector, setShowingOrganisationSelector] = useState(false);

    // Two options...
    // * useSearch: Is a search-filter function to filter out large amount of organizations
    const [useSearch, setUseSearch] = useState(false);

    const searchContainerRef = useRef(null);
    //const [fuse, setFuse] = useState(null);
    
    return (
        <Fragment>
            <LoggedOnInfoLabelContainer getData={true} /><i className="pn-organization-selector__icon" onClick={() => setShowingOrganisationSelector(!showingOrganisationSelector)}><GlobeIcon width="24" width="24" /></i>
            <div className="pn-organization-selector__form" ref={searchContainerRef}>
                {<SelectOrganization {...{ useSearch, showingOrganisationSelector, setShowingOrganisationSelector }} />}
            </div>
        </Fragment>
    );
};
export default OrganizationSelectorContainer;
