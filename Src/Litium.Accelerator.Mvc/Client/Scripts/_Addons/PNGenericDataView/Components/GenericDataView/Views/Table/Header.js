import React, { useEffect } from 'react';
import PropTypes from 'prop-types';

export const GridHeader = ({
    fields = [],
    sortColumn,
    sortConfig,
    fieldsToShow,
    isInModal = false,
}) => {

    if (!fields) {
        return null;
    }
    
    //useEffect(() => {

    //    setHiddenFieldsCount(fields.length - [...fieldsToShow].length);
    //}, [fieldsToShow]);

    return (
        <thead> 
            <tr>
                {fields.map((field, index) => (
                    field?.fieldName ? (
                    <th
                        key={index}
                            style={{ backgroundColor: field.settings.backgroundColor || null }}
                            onClick={() => sortColumn(field.fieldName, index)}
                            className={`${sortConfig?.fieldName === field.fieldName ? sortConfig.direction : ''
                            } ${fieldsToShow.includes(index) ? '' : 'fieldToHide'}`}
                    >
                            {field.fieldName}
                        </th>
                    ) : null
                ))}
            </tr>
        </thead>
    );
};

GridHeader.propTypes = {
    fields: PropTypes.array,
};

GridHeader.displayName = 'GridHeader';
export default GridHeader;