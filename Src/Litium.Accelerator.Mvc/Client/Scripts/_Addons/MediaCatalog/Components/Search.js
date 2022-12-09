import React, { Fragment, useEffect, useState, useRef } from 'react';
import { translate } from '../../../Services/translation';

const Search = ({ filterItems }) => {

    const searchRef = useRef();
    const [searchQuery, setSearchQuery] = useState('');
    const [componentLoaded, setComponentLoaded] = useState(false);
    const [activeSearchQuery, setActiveSearchQuery] = useState('');
    const search = () => {
        setActiveSearchQuery(searchQuery);
        filterItems(searchQuery);
    };
    const onKeyUp = (event) => {
        if (event.key === "Enter" || event.charCode === 13) {
            search();
        }
    };

    const checkSearchQuery = (event) => {
        var newValue = event.target.value;
        setSearchQuery(newValue);
        if (!newValue || newValue.length < 1 && searchQuery) {
            setSearchQuery('');
            setActiveSearchQuery('');
            return;
        }
    };

    useEffect(() => {
        if (!activeSearchQuery || activeSearchQuery.length === '') {
            if (componentLoaded) {
                searchRef.current.value = '';
                search();
            }
            setComponentLoaded(true);
        }
    }, [activeSearchQuery]);
    return (
        <Fragment>
            <div className="media-catalog__search-filter-remove">
                {activeSearchQuery &&
                    <div onClick={() => { setSearchQuery(''), setActiveSearchQuery('') }}>{translate('addons.mediacatalog.removeactivesearch')}: <i>{activeSearchQuery}</i></div>
                }
            </div>
            <div className="media-catalog__search">
                <input type="search" onChange={(e) => checkSearchQuery(e)} onKeyPress={(event) => onKeyUp(event)} ref={searchRef} placeholder={ translate('addons.mediacatalog.searchforfilestext') } />
                <div className={`media-catalog__search-button ${!searchQuery ? 'media-catalog__search-button--disabled' : ''}`} onClick={(e) => search()}>{translate('addons.mediacatalog.button.search')}</div>

                {/*false && currentStructure && currentStructure.length > 0 && currentStructure.map((folder, breadcrumbIndex) => {
                return (
                    <span key={`media-folder__breadcrumb${breadcrumbIndex}`} onClick={() => breadcrumbClick(folder)}>{folder.folderName}</span>
                )
            })*/}
            </div>
        </Fragment>
    )
};

export default Search;
