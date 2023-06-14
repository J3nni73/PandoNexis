import React, { useState, Fragment, useEffect } from 'react';
import { useSelector } from 'react-redux';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import { translate } from '../../../../Services/translation';
import { v4 as uuidv4 } from 'uuid';
import CardsIcon from '../../Icons/cards_view.svg?component';
import TableIcon from '../../Icons/table_view.svg?component';
import KanbanIcon from '../../Icons/kanban_view.svg?component';

import GenericDataViewFieldSettings from './GenericDataViewFieldSettings';
import {
    GenericDataViewSettings,
    GenericDataViewPagination,
} from '.';
import { getURLSearchParams } from '../../../../_PandoNexis/Services/url';
import PaginationComponent from './PaginationComponent';
import { useSortableData } from './GenericDataHooks/useSortableData';
import * as views from './Views';
let oldValue = 0;

// const MOCK_FIELD_IDS = {
//     editableFieldIds: ['variantName', 'PartWeightPackage'],
//     identifierFieldId: 'variantId',
// };

/**
 *
 * @param {boolean} isLoading - State of data fetching for the data-view.
 * @param {Array} dataContainers - Data DataContainers for the data-view.
 * @param {Object} settings - Global data-view settings.
 */

export const GenericDataView = ({
    isLoading = false,
    isInModal = false,
    dataContainers = [],
    settings = {},
    onSettingsChange,
    onDataContainerChange,
    dataType,
}) => {
    const genericDataView = useSelector((state) => state.genericDataView);
    const [fieldsToShow, setfieldsToShow] = useState([]);
    const [currentView, setCurrentView] = useState("table");

    const [columnsInsideContainerSmall, setColumnsInsideContainerSmall] = useState(isInModal ? 1 : 1);
    const [columnsInsideContainerMedium, setColumnsInsideContainerMedium] = useState(isInModal ? 1 : 2);
    const [columnsInsideContainerLarge, setColumnsInsideContainerLarge] = useState(isInModal ? 1 : 3);

    const [columnsWithContainersSmall, setColumnsWithContainersSmall] = useState(isInModal ? 1 : 1);
    const [columnsWithContainersMedium, setColumnsWithContainersMedium] = useState(isInModal ? 1 : 2);
    const [columnsWithContainersLarge, setColumnsWithContainersLarge] = useState(isInModal ? 1 : 3);


    const [hasInitialized, setHasInitialized] = useState(false);
    const [viewsList, setViewsList] = useState([]);
    const { totalHits, pageSize } = settings;

    const { items, requestSort, sortConfig } = useSortableData(dataContainers);
    // Pagination
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage, setPostsPerPage] = useState(pageSize);    
    const [currentPosts, setCurrentPosts] = useState(items);
  
    
    const sortColumn = (fieldName, index) => {
        requestSort('fieldValue', index, fieldName);
    };
    const hasDataContainers = (items || []).length > 0;
    
    //const pageCount = Math.ceil(totalHits / pageSize) || 1;
    //const { page = 1 } = getURLSearchParams();

    // ColumnsInsideContainer
    const handleSetfieldsToShow = (index) => {
        if (fieldsToShow.includes(index)) {
            setfieldsToShow(fieldsToShow.filter((f) => f !== index));
            return;
        }

        setfieldsToShow((prevState) => [...prevState, index]);
    };
    
    
    // Change page
    const paginate = (pageNumber) => {
        setCurrentPage(pageNumber);
    };
    // Change Number Of Items in view
    const itemsNumber = (num) => {
        setPostsPerPage(num);
        setCurrentPage(num);
    };

    const toggleView = (name) => {
        if (name !== currentView) {
            setCurrentView(name);
        }
    };

    const ViewIcon = ({ name }) => {
        if (!name) {
            return null;
        }
        return ({
            'table': <TableIcon alt={name} title={name} width={24} height={24} className={`${(name === currentView) ? 'active' : ''}`} />,
            'cards': <CardsIcon alt={name} title={name} width={24} height={24} className={`${(name === currentView) ? 'active' : ''}`} />,
            'kanban': <KanbanIcon alt={name} title={name} width={24} height={24} className={`${(name === currentView) ? 'active' : ''}`} />
        }[name]
        );
    };

    useEffect(() => {
        if (settings) {
            const _displayTypes = settings?.displayTypes; //?.trim().split(',');
            if (!_displayTypes) {
                return;
            }
            // Split to Array and trim strings

            //_displayTypes.trim();
            //if (_displayTypes.length < 1) { }
            //_displayTypes = _displayTypes.replace(/ /g, '').toLowerCase();
            //let displayTypesList = _displayTypes?.split(",") || ["table"];
            //if (!_displayTypes || !displayTypesList || displayTypesList.length < 1) {
            //    displayTypesList = ["table"];
            //}

            setViewsList(_displayTypes);
            setCurrentView(_displayTypes[0]);
            setColumnsInsideContainerSmall(settings?.columnsInsideContainerSmall || 1);
            setColumnsInsideContainerMedium(settings?.columnsInsideContainerMedium || 2);
            setColumnsInsideContainerLarge(settings?.columnsInsideContainerLarge || 3);

            setColumnsWithContainersSmall(settings?.columnsWithContainersSmall || 1);
            setColumnsWithContainersMedium(settings?.columnsWithContainersMedium || 2);
            setColumnsWithContainersLarge(settings?.columnsWithContainersLarge || 3);
        }
    }, [settings]);



    useEffect(() => {
        if (items.length !== 0 && !hasInitialized) {
            const fields = items[0].fields;
            let tempVisibleFields = [];
            for (let i = 0; i < fields.length; i++) {
                tempVisibleFields.push(i);
            }
            setfieldsToShow(tempVisibleFields);
            setHasInitialized(true);

            // REMOVE FOLLOWING ROW WHEN USING PAGENATION
            setCurrentPosts(items);
            //// WHEN USING PAGE SIZE
            //if (pageSize) {
            //    // Pagination
            //    const indexOfLastPost = currentPage * pageSize;
            //    const indexOfFirstPost = indexOfLastPost - pageSize;
            //    setCurrentPosts(items.slice(indexOfFirstPost, indexOfLastPost));
            //}
        }
    }, [items]);


    if (!currentView || !items || !settings || !viewsList || viewsList.length<1) {
        return null;
    }
    
    return (
        <Fragment>
            {viewsList && viewsList.length > 1 &&
                <div className="generic-data-view__views">
                    {console.log("viewsList", viewsList) }
                    <ul className="generic-data-view__view-selector">
                        {viewsList.map((viewItem, index) => (
                            <li key={`view${index}}`} onClick={() => toggleView(viewItem)}> <ViewIcon name={viewItem} /></li>
                        ))}
                    </ul>
                </div>
            }

            <div className={`generic-data-view generic-data-view__${currentView}-view`}>
                {/* <button className="form__button" onClick={sortColumn}>
        Sort Test
      </button> */}

                <GenericDataViewSettings onChange={onSettingsChange} {...settings} />
                {settings.changeVisibleFields && 
                    <GenericDataViewFieldSettings  {...items[0]}
                        handleSetfieldsToShow={handleSetfieldsToShow}
                        fieldsToShow={fieldsToShow}

                    />
                }
                {isLoading && !hasDataContainers && (
                    <Fragment>
                        <p className="generic-data-view--loading">
                            {translate('addons.GenericDataview.loadingtext')}
                        </p>
                        <div className="generic-loader active"></div>
                    </Fragment>
                )}

                <div className="generic-data-view__wrapper">
                    {hasDataContainers ? (
                        <Fragment>
                            {
                                {
                                    'table': <views.TableView
                                        {...{ dataContainers, settings, handleSetfieldsToShow, fieldsToShow, currentPosts, isLoading, isInModal }}
                                        onDataContainerChange={onDataContainerChange}
                                    />,
                                    'cards': <views.CardsView
                                        columnsWithContainersSmall={columnsWithContainersSmall || 1} columnsWithContainersMedium={columnsWithContainersMedium || 2} columnsWithContainersLarge={columnsWithContainersLarge || 3}
                                        columnsInsideContainerSmall={columnsInsideContainerSmall || 1} columnsInsideContainerMedium={columnsInsideContainerMedium || 2} columnsInsideContainerLarge={columnsInsideContainerLarge || 3} ingressField="Description" showTitles={true}
                                        {...{ dataContainers, settings, handleSetfieldsToShow, fieldsToShow, currentPosts, isLoading, isInModal }}
                                        onDataContainerChange={onDataContainerChange}
                                    />,
                                    'kanban': <views.KanbanView
                                        {...{ dataContainers, settings, handleSetfieldsToShow, fieldsToShow, currentPosts, isLoading, isInModal}}
                                        columnsInsideContainerSmall={columnsInsideContainerSmall || 1} columnsInsideContainerMedium={columnsInsideContainerMedium || 2} columnsInsideContainerLarge={columnsInsideContainerLarge || 3} onlyShowTaskName={true}
                                        onDataContainerChange={onDataContainerChange}
                                    />
                                }[currentView]
                            }
                        </Fragment>

                    ) : !isLoading ? (
                        <div className="generic-data-view__no-result">
                            {translate('addons.GenericDataview.noresult')}
                        </div>
                    ) : null}
                </div>
                {items.length !== 0 && <PaginationComponent
                    postsPerPage={postsPerPage}
                    totalPosts={items.length}
                    paginate={paginate}
                />}
            </div>
        </Fragment>
    );
};

GenericDataView.propTypes = {
    isLoading: PropTypes.bool,
    dataContainers: PropTypes.arrayOf(PropTypes.object),
    settings: PropTypes.object,
    onSettingsChange: PropTypes.func,
    onDataContainerChange: PropTypes.func,
};

GenericDataView.displayName = 'GenericDataView';
