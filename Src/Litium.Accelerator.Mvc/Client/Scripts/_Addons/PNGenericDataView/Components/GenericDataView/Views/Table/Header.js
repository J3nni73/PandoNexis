import React, { useEffect } from 'react';
import PropTypes from 'prop-types';

export const GridHeader = ({
    fields = [],
    sortColumn,
    sortConfig,
    fieldsToShow,
    isInModal = false,
}) => {
   
    //useEffect(() => {

    //    setHiddenFieldsCount(fields.length - [...fieldsToShow].length);
    //}, [fieldsToShow]);

    return (
        <thead> 
            <tr>
                {fields.map(({ fieldName, settings }, index) => (
                    <th
                        key={index}
                        style={{ backgroundColor: settings.backgroundColor || null }}
                        onClick={() => sortColumn(fieldName, index)}
                        className={`${sortConfig?.fieldName === fieldName ? sortConfig.direction : ''
                            } ${fieldsToShow.includes(index) ? '' : 'fieldToHide'}`}
                    >
                        {fieldName}
                    </th>
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