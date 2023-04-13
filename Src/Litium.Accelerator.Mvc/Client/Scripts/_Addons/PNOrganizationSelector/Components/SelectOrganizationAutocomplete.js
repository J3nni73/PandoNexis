import React, { Fragment } from 'react';

const SelectOrganizationAutocomplete = ({ result, selectedItem, onSelectOrg }) => (
    <ul className="select-org-autocomplete-result">
        {result && result.map((item, index, array) => (
            <Fragment key={`${item.name}-${index}`}>
                <li key={index} className={`select-org-autocomplete-result__item ${selectedItem === index ? 'select-org-autocomplete-result__item--selected' : ''}`} >
                    <a className={item.showAll ? 'select-org-autocomplete-result__show-all' : `select-org-autocomplete-result__link`} onClick={(orgId) => onSelectOrg({ id: item.id, name: item.name })}>
                        <div className="select-org-autocomplete-result__item--name" data-org-id={`${item.id}`} >{item.name}</div>
                    </a>
                </li>
            </Fragment>
        ))}
    </ul>
)

export default SelectOrganizationAutocomplete;