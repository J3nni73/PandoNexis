import React, { useState, Fragment, useEffect } from 'react';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import { translate } from '../../../../Services/translation';
import { v4 as uuidv4 } from 'uuid';

import GenericGridFieldSettings from './GenericGridFieldSettings';
import {
    GenericGridRow,
    GenericGridHeader,
    GenericGridSettings,
    GenericGridPagination,
} from '.';
import { getURLSearchParams } from '../../../../_PandoNexis/Services/url';
import PaginationComponent from './PaginationComponent';
import { useSortableData } from './GenericGridHooks/useSortableData';

let oldValue = 0;

// const MOCK_FIELD_IDS = {
//     editableFieldIds: ['variantName', 'PartWeightPackage'],
//     identifierFieldId: 'variantId',
// };

/**
 *
 * @param {boolean} isLoading - State of data fetching for the grid.
 * @param {Array} dataRows - Data rows for the grid.
 * @param {Object} settings - Global grid settings.
 */
export const GenericGridView = ({
    isLoading = false,
    dataRows = [],
    settings = {},
    onSettingsChange,
    onRowChange,
}) => {
    const [fieldsToShow, setfieldsToShow] = useState([]);
    const [hasInitialized, setHasInitialized] = useState(false);
    // Pagination
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage, setPostsPerPage] = useState(20);

    const { items, requestSort, sortConfig } = useSortableData(dataRows);
    // Pagination
    const indexOfLastPost = currentPage * postsPerPage;
    const indexOfFirstPost = indexOfLastPost - postsPerPage;
    const currentPosts = items.slice(indexOfFirstPost, indexOfLastPost);

    const sortColumn = (fieldName, index) => {
        requestSort('fieldValue', index, fieldName);
    };

    const handleSetfieldsToShow = (index) => {
        if (fieldsToShow.includes(index)) {
            setfieldsToShow(fieldsToShow.filter((f) => f !== index));
            return;
        }

        setfieldsToShow((prevState) => [...prevState, index]);
    };

    const hasRows = (items || []).length > 0;
    const { totalHits, pageSize } = settings;
    const pageCount = Math.ceil(totalHits / pageSize) || 1;
    const { page = 1 } = getURLSearchParams();

    // Change page
    const paginate = (pageNumber) => {
        setCurrentPage(pageNumber);
    };
    // Change Number Of Items in view
    const itemsNumber = (num) => {
        setPostsPerPage(num);
        setCurrentPage(num);
    };

    useEffect(() => {
        if (items.length !== 0 && !hasInitialized) {
            const fields = items[0].fields;
            let tempVisibleFields = [];
            for (let i = 0; i < fields.length; i++) {
                tempVisibleFields.push(i);
            }
            setfieldsToShow(tempVisibleFields);
            setHasInitialized(true);
        }
    }, [items]);
    return (
        <div className="generic-grid-view">
            {/* <button className="form__button" onClick={sortColumn}>
        Sort Test
      </button> */}
            <GenericGridSettings onChange={onSettingsChange} {...settings} />
            <GenericGridFieldSettings  {...items[0]}
                handleSetfieldsToShow={handleSetfieldsToShow}
                fieldsToShow={fieldsToShow}

            />
            {isLoading && !hasRows && (
                <Fragment>
                    <p className="generic-grid-view--loading">
                        {translate('addons.genericgridview.loadingtext')}
                    </p>
                    <div className="generic-loader active"></div>
                </Fragment>
            )}
            <div className="generic-grid-view__grid">
              
                {hasRows ? (
                    <table
                        className={classNames('generic-grid-view__table', {
                            'is-loading': isLoading,
                        })}
                    >

                        <GenericGridHeader
                            {...items[0]}
                            sortColumn={sortColumn}
                            sortConfig={sortConfig}
                            handleSetfieldsToShow={handleSetfieldsToShow}
                            fieldsToShow={fieldsToShow}
                        />
                        <tbody>
                            {currentPosts &&
                                currentPosts.map((row, index) => (
                                    <GenericGridRow
                                        key={`${uuidv4()}${index}`}
                                        {...row}
                                        {...settings}
                                        onChange={onRowChange}
                                        rowIndex={index}
                                        fieldsToShow={fieldsToShow}
                                    />
                                ))}
                        </tbody>
                    </table>
                ) : !isLoading ? (
                    <div className="generic-grid-view__no-result">
                        {translate('addons.genericgridview.noresult')}
                    </div>
                ) : null}
            </div>
            {items.length !== 0 && <PaginationComponent
                postsPerPage={postsPerPage}
                totalPosts={items.length}
                paginate={paginate}
            />}
        </div>
    );
};

GenericGridView.propTypes = {
    isLoading: PropTypes.bool,
    dataRows: PropTypes.arrayOf(PropTypes.object),
    settings: PropTypes.object,
    onSettingsChange: PropTypes.func,
    onRowChange: PropTypes.func,
};

GenericGridView.displayName = 'GenericGridView';
