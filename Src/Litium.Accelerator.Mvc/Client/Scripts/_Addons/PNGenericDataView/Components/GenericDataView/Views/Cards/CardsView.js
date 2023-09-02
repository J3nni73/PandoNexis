import React, { useState, Fragment, useEffect } from 'react';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import { translate } from '../../../../../../Services/translation';
import { v4 as uuidv4 } from 'uuid';
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
export const CardsView = ({
    dataContainers = [],
    settings = {},
    handleSetfieldsToShow,
    onDataContainerChange,
    fieldsToShow,
    currentPosts,
    isLoading = false,
    isInModal = false,
    error,
    columnsWithContainersSmall = 1,
    columnsWithContainersMedium = 2,
    columnsWithContainersLarge = 3,
    columnsInsideContainerSmall = 1,
    columnsInsideContainerMedium = 2,
    columnsInsideContainerLarge = 3,
    ingressField,
    showTitles,
    dropDownOptions
}) => {
    const { items, requestSort, sortConfig } = useSortableData(dataContainers);
    const sortColumn = (fieldName, index) => {
        requestSort('fieldValue', index, fieldName);
    };
    const [mainSettings, setMainSettings] = useState(settings);
    if (dataContainers && fieldsToShow) {

        return (
            <div className={classNames('generic-data-view__cards', {
                'is-loading': isLoading,
            })}
            >   
                <div className={`row small-up-${columnsWithContainersSmall || 1} medium-up-${columnsWithContainersMedium || 2} large-up-${columnsWithContainersLarge || 3} ${settings.alignContainers ? 'align-' + settings.alignContainers : ''} `}>
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
                                {...{
                                    columnsInsideContainerSmall,
                                    columnsInsideContainerMedium,
                                    columnsInsideContainerLarge,
                                    showTitles,
                                    ingressField,
                                    isInModal
                                }}
                            />
                        ))}
                </div>
            </div>
        );
    }
        else {
        return null;
    }
};

CardsView.propTypes = {
    dataContainers: PropTypes.arrayOf(PropTypes.object),
    settings: PropTypes.object,
    onDataContainerChange: PropTypes.func,
};

CardsView.displayName = 'CardsView';
export default CardsView;