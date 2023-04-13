import React, { Fragment, useEffect, useState, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getCollectionPageData } from '../Actions/CollectionPage.action';
import { useClickOutside } from 'react-browser-hooks';
import ConditionalLinkWrapper from '../../../_PandoNexis/Components/ConditionalLinkWrapper';
//import Fuse from 'fuse.js'
//import { translate } from '../../../Services/translation';
//import Search from './Search';

const CollectionPageContainer = ({
    collectionPageSystemId, link, linkText
}) => {
    const dispatch = useDispatch();
    const [children, setChildren] = useState([]);
    const [filters, setFilters] = useState([]);
    const [selectedFilters, setSelectedFilters] = useState([]);
    const [selectedFilterValues1, setSelectedFilterValues1] = useState([]);
    const [selectedFilterValues2, setSelectedFilterValues2] = useState([]);
    const [selectedFilterValues3, setSelectedFilterValues3] = useState([]);
    const [componentInit, setComponentInit] = useState(false);
    const [activeFilterIndex, setActiveFilterIndex] = useState(-1);
    const filterContainerRef = useRef(false);
    useClickOutside(filterContainerRef, () => setActiveFilterIndex(-1));
    const { pageStructure } = useSelector(
        (state) => state.collectionPage
    );

    const clearStructure = () => {
        const obj = { folderName: firstFolderName || pageStructure.folderName };
        setCurrentStructure([obj]);
    };
    const toggleFilterValue = (event, filterValue, filterOrder) => {
        const target = event.target;
        switch (filterOrder) {
            case 1:
                if (!target.checked) {
                    setSelectedFilterValues1(selectedFilterValues1.filter(e => e.toLowerCase() !== filterValue.toLowerCase()))
                }
                else if (!(selectedFilterValues1.some(e => e.toLowerCase() === filterValue.toLowerCase()))) {
                    setSelectedFilterValues1([...selectedFilterValues1, filterValue.toLowerCase()])
                }
                break;
            case 2:
                if (!target.checked) {
                    setSelectedFilterValues2(selectedFilterValues2.filter(e => e.toLowerCase() !== filterValue.toLowerCase()))
                }
                else if (!(selectedFilterValues2.some(e => e.toLowerCase() === filterValue.toLowerCase()))) {
                    setSelectedFilterValues2([...selectedFilterValues2, filterValue.toLowerCase()])
                }
                break;
            case 3:
                if (!target.checked) {
                    setSelectedFilterValues3(selectedFilterValues3.filter(e => e.toLowerCase() !== filterValue.toLowerCase()))
                }
                else if (!(selectedFilterValues3.some(e => e.toLowerCase() === filterValue.toLowerCase()))) {
                    setSelectedFilterValues3([...selectedFilterValues3, filterValue.toLowerCase()])
                }
                break;
            default: break;
        }

    };
    const toggleFilter = (event, filterObj) => {
        const target = event.target;
        if (!target.checked) {
            setSelectedFilters(selectedFilters.filter(e => e.title !== filterObj.title))
        }
        else if (!(selectedFilters.some(e => e.title === filterObj.title))) {
            setSelectedFilters([...selectedFilters, filterObj])
        }
    };
    // When Component is initialized
    useEffect(() => {
        if (collectionPageSystemId) {
            dispatch(getCollectionPageData(collectionPageSystemId));
        }
    }, []);

    // When pageStructure is updated (Reducer value)
    useEffect(() => {
        if (pageStructure) {
            if (pageStructure.children && pageStructure.children.length > 0) {
                setChildren(pageStructure.children);
            }
            if (pageStructure.filters && pageStructure.filters.length > 0) {

                setFilters(pageStructure.filters);
            }
            setComponentInit(true);
        }
    }, [pageStructure]);

    // When Filter values is updated
    useEffect(() => {
        if (componentInit) {
            if (selectedFilterValues1.length < 1 && selectedFilterValues2.length < 1 && selectedFilterValues3.length < 1) {
                setChildren(pageStructure.children);
                return;
            }
            let filteredChildren = pageStructure.children;

            if (selectedFilterValues1 && selectedFilterValues1.length > 0) {
                filteredChildren = filteredChildren.filter(e => selectedFilterValues1.some(x => x.toLowerCase() === e.filterValue1?.toLowerCase()));
            }
            if (selectedFilterValues2 && selectedFilterValues2.length > 0) {
                filteredChildren = filteredChildren.filter(e => selectedFilterValues2.some(x => x.toLowerCase() === e.filterValue2?.toLowerCase()));
            }
            if (selectedFilterValues3 && selectedFilterValues3.length > 0) {
                filteredChildren = filteredChildren.filter(e => selectedFilterValues3.some(x => x.toLowerCase() === e.filterValue3?.toLowerCase()));
            }
            setChildren(filteredChildren);
            //setChildren(pageStructure.children.filter(e => selectedFilterValues1.some(x => x.toLowerCase() === e.filterValue1?.toLowerCase() || x.toLowerCase() === e.filterValue2?.toLowerCase() || x.toLowerCase() === e.filterValue3?.toLowerCase())));
        }
    }, [selectedFilterValues1, selectedFilterValues2, selectedFilterValues3]);

    // When Filter is updated
    useEffect(() => {
        if (componentInit) {
            if (selectedFilters.length < 1) {
                setChildren(pageStructure.children);
                return;
            }
            setChildren(pageStructure.children.filter(e => selectedFilters.some(x => x.values.some(y => y.toLowerCase() === e.filterValue1?.toLowerCase() || y.toLowerCase() === e.filterValue2?.toLowerCase() || y.toLowerCase() === e.filterValue3?.toLowerCase()))));
        }
    }, [selectedFilters]);


    if (!children || children.length < 0) {
        <div>No children</div>
    }
    return (
        <Fragment>
            {pageStructure.useFilters && filters && filters.length > 0 &&
                <div className="row collection-page__filters" ref={filterContainerRef}>
                    <div className="small-12 columns">
                        {filters.map((child, indexFilter) => {
                            return (<ul className="collection-page__filter-categories" id={`filter-${indexFilter}`} key={`filter-${indexFilter}`}><li>
                                <div className={`dropdown-selector ${activeFilterIndex === indexFilter ? 'dropdown-selector__current' : ''}`}>
                                    <div className=" button button__tertiary collection-page__filter-button" onClick={() => setActiveFilterIndex(activeFilterIndex === indexFilter ? -1 : indexFilter)}>
                                        {child.title}
                                        <i className="chevron__dropdown"></i>
                                    </div>
                                    <ul className="collection-page__filter-values dropdown-selector__items">
                                        {child.values.map((childValue, indexFilterValue) => {
                                            return (
                                                <li className="collection-page__filter-title" key={`filterValue-${indexFilterValue}`}>
                                                    <input id={`filter_${indexFilter}-${indexFilterValue}`} type="checkbox" onChange={(e) => toggleFilterValue(e, childValue, indexFilter + 1)} /><label htmlFor={`filter_${indexFilter}-${indexFilterValue}`}>{childValue}</label>
                                                </li>
                                            );
                                        })}

                                    </ul></div>
                                {/*<input id={`filter_${indexFilter}`} type="checkbox" onChange={(e) => toggleFilter(e, child)} /><label htmlFor={`filter_${indexFilter}`}>{child.title}</label>*/}
                            </li></ul>);
                        })}
                        {link && linkText &&
                            <a className="collection-page__filter-button-link button" href={link} title={linkText}>{linkText}</a>
                        }
                    </div>
                </div>
            }
            <div className="collection-page__data row small-up-2 medium-up-3 large-up-4">
                {children && children.map((child, index) => {
                    return (
                        <div className="column collection-page__child-container" key={`child-${index}`}>
                            <article className="collection-page__child">
                                <figure className="collection-page__child-image">
                                    {child.image &&
                                        <ConditionalLinkWrapper
                                            condition={child.button && child.button.href}
                                            wrapper={children => <a href={child.button.href} target={`${child.button.target ? child.button.target : ''}`}>{children}</a>}
                                        >
                                            <Fragment><img src={child.image.imageUrl} alt={child.image.title} width={child.image.width} height={child.image.height} /></Fragment>
                                        </ConditionalLinkWrapper>
                                    }
                                </figure>
                                <ConditionalLinkWrapper
                                    condition={child.button && child.button.href}
                                    wrapper={children => <a href={child.button.href} target={`${child.button.target ? child.button.target : ''}`}>{children}</a>}
                                >
                                    <section className="collection-page__child-text">
                                        <h3>{child.title}</h3>
                                        <div dangerouslySetInnerHTML={{ __html: child.introduction }}></div>
                                    </section>
                                </ConditionalLinkWrapper>
                                {child.button && child.button.href && child.button.linkText &&
                                    <a className={`button button__primary collection-page__child-cta ${child.button.class}`} href={child.button.href} target={`${child.button.target ? child.button.target : ''}`}>{child.button.linkText}</a>
                                }
                            </article>
                        </div>
                    );
                }
                )}
            </div >
        </Fragment>
    );
};
export default CollectionPageContainer;
