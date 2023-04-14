import React, { Fragment, useEffect, useState, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import SelectOrganizationAutocomplete from './SelectOrganizationAutocomplete';
import OrganizationSelectList from './OrganizationSelectList';
import { fillOrganizationList } from '../Actions/OrganizationSelector.action';
import { translate } from '../../../Services/translation';


const SelectOrganization = ({
    showingOrganisationSelector, setShowingOrganisationSelector, selectedItem, onSelectOrg, useSearch, query, result, showResult, showFullForm, onSearch, onBlur, onKeyDown, selectOnChange, onOrgSubmit
}) => {
    const dispatch = useDispatch();
    const [selectedOrg, setSelectedOrg] = useState([]);
    const [firstTimeShowing, setFirstTimeShowing] = useState(false);
    const searchInputRef = useRef(null);
    const { organizationList } = useSelector(
        (state) => state.organizationSelector
    );

    const focusOnInput = () => {

    };

    useEffect(() => {
        if (showingOrganisationSelector && !firstTimeShowing) {
            // Loading all organizations.
            if (fillOrganizationList) {
                dispatch(fillOrganizationList());
            }
            setFirstTimeShowing(true);
        }
    }, [showingOrganisationSelector]);

    useEffect(() => {
        if (organizationList && organizationList.length > 1) {
            const selOrganization = organizationList.filter(x => x.isSelected);
            if (selOrganization && selOrganization.length > 0) {
                setSelectedOrg(selOrganization[0]);
            }
        }
    }, [organizationList]);

    if (!organizationList || organizationList.length < 2 || !selectedOrg || selectedOrg.length < 1) {
        return null;
    }

    if (!showingOrganisationSelector)
        return null;
    return (
        <div role="search" className={`select-org__form ${showFullForm ? 'select-org__form--force-show' : ''}`} role="search">
            <div className="select-org__input-wrap">
                <h3 className="select-org__name">{translate('addons.changeorganization.title')}</h3>
                {useSearch &&
                    <Fragment>
                        <span className="select-org__title">{translate('common.organization.search')}</span>
                        <input className="select-org__input" type="search" placeholder={translate('common.organization.search')}
                            autoComplete="off" value={query} onChange={event => onSearch(event.target.value)}
                            onKeyDown={event => onKeyDown(event)}
                            ref={searchInputRef}
                            onBlur={() => onBlur()} />

                        {showResult && <SelectOrganizationAutocomplete result={result} selectedItem={selectedItem} onSelectOrg={onSelectOrg} />}
                    </Fragment>
                }
                {organizationList && organizationList.length > 0 &&
                    <OrganizationSelectList {...{ organizationList, selectedOrg }} />
                }
            </div>
            </div>
    );
};
export default SelectOrganization;
