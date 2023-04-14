import React, { useEffect } from 'react';
import PropTypes from 'prop-types';

export const GridHeader = ({
    fields = [],
    sortColumn,
    sortConfig,
    fieldsToShow,
    statusList,
    isInModal = false,
}) => {
   
    //useEffect(() => {

    //    setHiddenFieldsCount(fields.length - [...fieldsToShow].length);
    //}, [fieldsToShow]);

    if (!statusList || statusList.length < 1) {
        return null;
    }

    return (
        <thead> 
            <tr>
                {statusList.map((status, index) => (
                    <th
                        key={`header-${index}`}
                        style={{ backgroundColor: status.backgroundColor || null }}
                        onClick={() => sortColumn(status.name, index)}
                        className={`${sortConfig?.name === status.name ? sortConfig.direction : ''
                            } ${fieldsToShow.includes(index) ? '' : 'fieldToHide'}`}
                    >
                        {status.name}
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