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
export const KanbanView = ({
    dataContainers = [],
    settings = {},
    handleSetfieldsToShow,
    onDataContainerChange,
    fieldsToShow,
    currentPosts,
    isLoading = false,
    isInModal = false,
    error,
    rowIndex,
    columnsInsideContainerSmall = 1,
    columnsInsideContainerMedium = 2,
    columnsInsideContainerLarge = 3,
}) => {
    const { items, requestSort, sortConfig } = useSortableData(dataContainers);
    const sortColumn = (fieldName, index) => {
        requestSort('fieldValue', index, fieldName);
    };
    const mainSettings = settings;
    if (dataContainers && fieldsToShow) {

        return (
            <table className={classNames('generic-data-view__table kanban', {
                'is-loading': isLoading,
            })}
                 style={{ maxWidth: settings?.dataViewMaxWidth ? settings.dataViewMaxWidth : null }}
            >
                <GridHeader
                    {...items[0]}
                    sortColumn={sortColumn}
                    sortConfig={sortConfig}
                    handleSetfieldsToShow={handleSetfieldsToShow}
                    fieldsToShow={fieldsToShow}
                    statusList={settings.kanbanData?.genericDataContainerStateList}
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
                                columnsInsideContainerSmall={columnsInsideContainerSmall}
                                columnsInsideContainerMedium={columnsInsideContainerMedium}
                                columnsInsideContainerLarge={columnsInsideContainerLarge}
                            />
                        ))}
                </tbody>
            </table>
        );
    }
        else {
        return null;
    }
};

KanbanView.propTypes = {
    dataContainers: PropTypes.arrayOf(PropTypes.object),
    settings: PropTypes.object,
    onDataContainerChange: PropTypes.func,
};

KanbanView.displayName = 'KanbanView';
export default KanbanView;