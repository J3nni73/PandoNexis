import React, { Fragment, useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getPortalPageData } from '../Actions/PortalPage.action';
import ConditionalLinkWrapper from '../../../_PandoNexis/Components/ConditionalLinkWrapper';
//import Fuse from 'fuse.js'
//import { translate } from '../../../Services/translation';
//import Search from './Search';

const PortalPageContainer = ({
    portalPageSystemId,
}) => {
    const dispatch = useDispatch();
    const [children, setChildren] = useState([]);
    const [filters, setFilters] = useState([]);
    const [selectedFilters, setSelectedFilters] = useState([]);
    const [selectedFilterValues, setSelectedFilterValues] = useState([]);
    const [componentInit, setComponentInit] = useState(false);
    const [activeFilterIndex, setActiveFilterIndex] = useState(-1);

    const { pageStructure } = useSelector(
        (state) => state.portalPage
    );

    const clearStructure = () => {
        const obj = { folderName: firstFolderName || pageStructure.folderName };
        setCurrentStructure([obj]);
    };
    const toggleFilterValue = (event, filterValue) => {
        const target = event.target;
        if (!target.checked) {
            setSelectedFilterValues(selectedFilterValues.filter(e => e.toLowerCase() !== filterValue.toLowerCase()))
        }
        else if (!(selectedFilterValues.some(e => e.toLowerCase() === filterValue.toLowerCase()))) {
            setSelectedFilterValues([...selectedFilterValues, filterValue.toLowerCase()])
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
        if (portalPageSystemId) {
            dispatch(getPortalPageData(portalPageSystemId));
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
            if (selectedFilterValues.length < 1) {
                setChildren(pageStructure.children);
                return;
            }
            setChildren(pageStructure.children.filter(e => selectedFilterValues.some(x => x.toLowerCase() === e.filterValue1?.toLowerCase() || x.toLowerCase() === e.filterValue2?.toLowerCase() || x.toLowerCase() === e.filterValue3?.toLowerCase())));
        }
    }, [selectedFilterValues]);

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
                <div className="row portal-page__filters">
                    {filters.map((child, indexFilter) => {
                        return (<ul className="small-12 medium-6 large-4 columns  portal-page__filter-categories" key={`filter-${indexFilter}`}><li>
                            <div class={`dropdown-selector ${activeFilterIndex === indexFilter ? 'dropdown-selector__current' : ''}`}>
                                <div class=" button button__tertiary portal-page__filter-button" onClick={() => setActiveFilterIndex(activeFilterIndex === indexFilter ? -1 : indexFilter)}>
                                    {child.title}
                                    <i class="chevron__dropdown"></i>
                                </div>
                                <ul className="portal-page__filter-values dropdown-selector__items">
                                    {child.values.map((childValue, indexFilterValue) => {
                                        return (
                                            <li className="portal-page__filter-title" key={`filterValue-${indexFilterValue}`}>
                                                <input id={`filter_${indexFilter}-${indexFilterValue}`} type="checkbox" onChange={(e) => toggleFilterValue(e, childValue)} /><label htmlFor={`filter_${indexFilter}-${indexFilterValue}`}>{childValue}</label>
                                            </li>
                                        );
                                    })}
                                </ul></div>
                            {/*<input id={`filter_${indexFilter}`} type="checkbox" onChange={(e) => toggleFilter(e, child)} /><label htmlFor={`filter_${indexFilter}`}>{child.title}</label>*/}
                        </li></ul>);
                    })}

                </div>
            }
            <div className="portal-page__data row small-up-2 medium-up-3 large-up-4">
                {children && children.map((child, index) => {
                    return (
                        <div className="column portal-page__child-container" key={`child-${index}`}>
                            <article className="portal-page__child">
                                <figure className="portal-page__child-image">
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
                                    <section className="portal-page__child-text">
                                        <h3>{child.title}</h3>
                                        <div dangerouslySetInnerHTML={{ __html: child.introduction }}></div>
                                    </section>
                                </ConditionalLinkWrapper>
                                {child.button && child.button.href && child.button.linkText &&
                                    <a className={`button button__primary portal-page__child-cta ${child.button.class}`} href={child.button.href} target={`${child.button.target ? child.button.target : ''}`}>{child.button.linkText}</a>
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
export default PortalPageContainer;
