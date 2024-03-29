import React, { useState, Fragment, useEffect } from 'react';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import { translate } from '../../../../../../Services/translation';
import { v4 as uuidv4 } from 'uuid';
import GridHeader from './Header';
import DataContainer from './DataContainer';
import { useSortableData } from '../../GenericDataHooks/useSortableData';

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
export const TableView = ({
    dataContainers = [],
    settings = {},
    isLoading = false,
    isInModal = false,
    handleSetfieldsToShow,
    onDataContainerChange,
    fieldsToShow,
    currentPosts,
    columnsWithContainersSmall = 1,
    columnsWithContainersMedium = 1,
    columnsWithContainersLarge = 1,
    dropDownOptions,
    error,
    rowIndex,
}) => {
    const { items, requestSort, sortConfig } = useSortableData(dataContainers);
    const [mainSettings, setMainSettings] = useState(settings);

    const sortColumn = (fieldName, index) => {
        requestSort('fieldValue', index, fieldName);
    };

    if (dataContainers && fieldsToShow) {
        return (
            <div className={`row collapse align-center small-up-${columnsWithContainersSmall || 1} medium-up-${columnsWithContainersMedium || 1} large-up-${columnsWithContainersLarge || 1}`}>
                <div className="columns">
                    <table className={classNames('generic-data-view__table', {
                        'is-loading': isLoading,
                    })}
                    >
                        <GridHeader
                            {...items[0]}
                            sortColumn={sortColumn}
                            sortConfig={sortConfig}
                            handleSetfieldsToShow={handleSetfieldsToShow}
                            fieldsToShow={fieldsToShow}
                            dataContainers={dataContainers}
                        />
                        <tbody>
                            {currentPosts &&
                                currentPosts.map((dataContainer, index) => (
                                    <DataContainer
                                        key={`${uuidv4()}${index}`}
                                        {...dataContainer}
                                        {...settings}
                                        mainSettings={mainSettings}
                                        onDataContainerChange={onDataContainerChange}
                                        dataContainerIndex={index}
                                        fieldsToShow={fieldsToShow}
                                        dropDownOptions={dataContainer.options}
                                        {...{ isInModal }}
                                    />
                                ))}
                        </tbody>
                    </table>
                </div>
            </div>
        );
    }
    else {
        return null;
    }
};

TableView.propTypes = {
    dataContainers: PropTypes.arrayOf(PropTypes.object),
    settings: PropTypes.object,
    onDataContainerChange: PropTypes.func,
};

TableView.displayName = 'TableView';
export default TableView;