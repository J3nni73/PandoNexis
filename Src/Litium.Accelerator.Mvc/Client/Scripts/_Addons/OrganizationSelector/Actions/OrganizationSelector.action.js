import { get, post } from '../../../Services/http';
import { getURLSearchParams } from '../../../_PandoNexis/Services/url';
import { catchError } from '../../../Actions/Error.action';
import { toggleGenericLoader } from '../../../_PandoNexis/Actions/GenericLoader.action';

import { translate } from '../../../Services/translation';
import { ORGANIZATION_SEARCH_QUERY, ORGANIZATION_SEARCH_RECEIVE, ORGANIZATION_SEARCH_ERROR, ORGANIZATION_SEARCH_SHOW_FULL_FORM, ORGANIZATION_SEARCH_SELECT_ITEM, ORGANIZATION_SEARCH_TOGGLE_AUTOCOMPLETE, ORGANIZATION_SET_ORG_LIST, ORGANIZATION_LOAD, ORGANIZATION_ERROR } from '../constants';

const rootRoute = '/api/organizationselector';
let selectedOrgId = '';

export const fillOrganizationList = () => (dispatch, getState) => {
    post(rootRoute + '/getall')
        .then(response => response.json())
        .then(organizations => dispatch(addClassToMainContent(organizations)))
        .then(organizations => dispatch(setOrgList(organizations)))
        .catch(ex => dispatch(catchError(ex, error => searchError(error))));
}

export const changeOrganization = (orgId, skipConfirm = false) => (dispatch, getState) => {
    const orgList = getState().organizationSelector?.organizationList;
    let selectedOrg = null;

    if (orgList) {
        const organizations = orgList.filter(x => x.id === orgId);
        if (organizations) {
            selectedOrg = organizations[0];
        }
    }
    if (orgId) {
        let changeOrg = false;
        if (!skipConfirm) {
            const regex = /\\n/g;
            const confirmText = translate('common.organization.change.question').replace("[orgName]", selectedOrg.name).replace(regex, "\n");
            changeOrg = confirm(confirmText);
        }

        if (changeOrg === true || skipConfirm) {
            // Set selected ID to prevent jumping back to last set VALUE just before reload
            dispatch(setSelectedOrganization(orgId));

            return post(rootRoute + '/setcurrentorganization', orgId)
                .then(response => window.location.reload())
                .catch(ex => dispatch(catchError(ex, error => searchError(error))));
        }
    }

    // Hide autocomplete
    //dispatch(hideAutocomplete(true));
}

export const searchError = error => ({
    type: ORGANIZATION_SEARCH_ERROR,
    payload: {
        error,
    }
})

export const receive = result => ({
    type: ORGANIZATION_SEARCH_RECEIVE,
    payload: {
        result,
        showResult: result && result.length > 0
    }
})

export const setOrgList = organizationList => ({
    type: ORGANIZATION_SET_ORG_LIST,
    payload: {
        organizationList,
        // result: organizationList, // Filter organizationList
        // showResult: payload.result && this.payload.result.length > 0
    }
});


export const addClassToMainContent = (organizations) => (dispatch, getState) => {
    if (organizations.length > 0) {
        const mainContentEl = document.querySelector('.main-content');
        if (mainContentEl) {
            mainContentEl.classList.add('organization-select-visible');
        }
    }
    return organizations;
}

export const filterList = () => (dispatch, getState) => {

    const orgList = getState().selectOrganization.organizationList;
    const q = getState().selectOrganization.query.toLowerCase();
    const filteredList = orgList.filter(item => item.name.toLowerCase().indexOf(q) > -1);

    dispatch(receive(filteredList));
}

export const hideAutocomplete = (hide) => (dispatch, getState) => {
    dispatch(autocompleteHidden(hide));
}

const autocompleteHidden = hideAutocomplete => ({
    type: ORGANIZATION_SEARCH_TOGGLE_AUTOCOMPLETE,
    payload: {
        showResult: !hideAutocomplete,
        query: '',
    }
})

const show = visible => ({
    type: ORGANIZATION_SEARCH_SHOW_FULL_FORM,
    payload: {
        showFullForm: visible,
    }
})

export const handleKeyDown = (event, opt) => (dispatch, getState) => {
    const { result, selectedItem } = getState().selectOrganization;

    if (!result || !result.length) {
        return;
    }

    const max = result.length - 1,
        clip = index => index < 0 ? max : index > max ? 0 : index;

    let selectedItemIndex = selectedItem;
    let useArrows = result.length > 0;
    if (result.length === 1) {
        selectedItemIndex = 0;
        useArrows = false;
    }
    switch (event.keyCode) {
        case 38:
            if (useArrows) {
                dispatch(selectItem(clip(selectedItemIndex - 1)));
            }
            break;
        case 40:
            if (useArrows) {
                dispatch(selectItem(clip(selectedItemIndex + 1)));
            }
            break;
        case 13:
            // On enter: Set item in drop-down
            dispatch(setListValueByIndex(selectedItemIndex));
            break;
        default:
            break;
    }
}

export const setSelectedOrganization = selectedOrgId => ({
    type: ORGANIZATION_SEARCH_SELECT_ITEM,
    payload: {
        selectedOrg: { id: selectedOrgId, name: '', isSelected: true },
    }
})

export const setDefaultOrganization = defaultOrg => ({
    type: ORGANIZATION_SEARCH_SELECT_ITEM,
    payload: {
        defaultOrg,
    }
})
const selectItem = selectedItem => ({
    type: ORGANIZATION_SEARCH_SELECT_ITEM,
    payload: {
        selectedItem,
    }
})
