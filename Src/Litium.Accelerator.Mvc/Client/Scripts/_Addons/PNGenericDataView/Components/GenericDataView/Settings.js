import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import PropTypes from 'prop-types';
import { useForm } from 'react-hook-form';
import { translate } from '../../../../Services/translation';
import { GenericDataViewDropdown } from '.';
import { GenericDataViewTextFilter } from '.';
import { getURLSearchParams } from '../../../../_PandoNexis/Services/url';
import { any } from 'array-flat-polyfill';
import { GenericDataViewField } from './Field';
import { buttonClick } from '../../Actions/GenericDataContainerField.action';
import { loadModal } from '../../Actions/GenericDataView.action';

const SORT_BY = 'sortBy';
const EXPORT = 'export';

const getFilterCount = (values) =>
    values && values.length ? ` (${values.length})` : '';

/**
 *
 * @param {Array} filter - List of available filters to apply on the dataView.
 * @param {Array} sortOptions - List of available sorting options for dataView data.
 */
export const GenericDataViewSettings = ({
    filter = [],
    sortOptions = [],
    onChange,
    dataViewButtons
}) => {
    const {
        register,
        handleSubmit,
        watch,
        formState,
        getValues,
        setValue,
    } = useForm({
        defaultValues: getURLSearchParams(),
    });
    const dispatch = useDispatch();
    const [open, setOpen] = useState({});
    const toggleOpen = (name) =>
        setOpen((open) => {
            if (formState.dirtyFields[name]) {
                setValue(open.name, open.cleanValue, { shouldDirty: false });
            }
            return name === open.name ? {} : { name, cleanValue: getValues(name) };
        });

    const hasFilters = filter.length > 0;
    const hasSortOptions = sortOptions.length > 0;
    const onSettingsChange = () => {
        handleSubmit(onChange)();
        toggleOpen();
    };
    const onMainButtonsClick = (form) => {

        const useConfirmation = currGenDW_useConfirmation;
        const fieldSettings = currGenDW_fieldSettings;
        const confirmationText = currGenDW_confirmationText;
        const fieldId = currGenDW_fieldId;
        const entitySystemId = fieldSettings.entitySystemId || fields[0].entitySystemId;
        const identifierField = { entitySystemId };

        if (useConfirmation) {
            if (!confirm(confirmationText)) {
                return false;
            }
        }
        if (fieldSettings?.buttonOpenInModal) {
            const modalSettings = {
                modalPageSystemId: fieldSettings.modalPageSystemId || fieldSettings.pageSystemId,
                entitySystemId,
            };
            dispatch(loadModal(modalSettings));
            return;
        }

        const selectedValueObject = {
            value: '',
            name: fieldId,
            entitySystemId,
        };
        dispatch(buttonClick(fieldId, null, selectedValueObject, false, fieldSettings));
    };
    return (
        <div className="generic-data-view__settings">
            {hasFilters &&
                filter.map(
                    ({ fieldId: name, fieldName, filterType, values, index }) => {
                        const selectedValues = watch(name);

                        if (filterType === 'string') {
                            return (
                                <GenericDataViewTextFilter
                                    key={name}
                                    name={name}
                                    label={fieldName}
                                    onChange={onSettingsChange}
                                    refer={register}
                                ></GenericDataViewTextFilter>
                            );
                        } else {
                            return (
                                <GenericDataViewDropdown
                                    key={name + index}
                                    label={`${fieldName}${getFilterCount(selectedValues)}`}
                                    isOpen={open.name === name}
                                    onClick={() => toggleOpen(name)}
                                    onChange={onSettingsChange}
                                >
                                    {Array.isArray(values) &&
                                        values.map((value, index) => (
                                            <label key={value + index}>
                                                <input
                                                    name={name}
                                                    type="checkbox"
                                                    value={value}
                                                    ref={register}
                                                />
                                                {value}
                                            </label>
                                        ))}
                                </GenericDataViewDropdown>
                            );
                        }
                    }
                )}
            {hasSortOptions && (
                <GenericDataViewDropdown
                    label={translate('facet.header.sortCriteria')}
                    isOpen={open.name === SORT_BY}
                    onClick={() => toggleOpen(SORT_BY)}
                    onChange={onSettingsChange}
                    align="right"
                >
                    {sortOptions.map(({ fieldName, sortType, fieldId }) => (
                        <label key={`${sortType}-${fieldId}`}>
                            <input
                                name="sort_by"
                                type="radio"
                                value={fieldId + '|' + sortType}
                                ref={register}
                            />
                            {fieldName} {sortType}
                        </label>
                    ))}
                </GenericDataViewDropdown>
            )}

            {dataViewButtons?.length > 0 && 
                <GenericDataViewField
                    onButtonClick={onMainButtonsClick}
                    genericButtons={dataViewButtons}
                />
            }
           {/* <ExportButton />*/}
        </div>
    );
};

GenericDataViewSettings.propTypes = {
    onChange: PropTypes.func,
    filters: PropTypes.arrayOf(
        PropTypes.shape({
            fieldId: PropTypes.string,
            values: PropTypes.arrayOf(PropTypes.string),
        })
    ),
    sortOptions: PropTypes.arrayOf(
        PropTypes.shape({
            fieldId: PropTypes.string,
            fieldName: PropTypes.string,
            sortType: PropTypes.string,
        })
    ),
};
GenericDataViewSettings.displayName = 'GenericDataViewSettings';
export default GenericDataViewSettings;

