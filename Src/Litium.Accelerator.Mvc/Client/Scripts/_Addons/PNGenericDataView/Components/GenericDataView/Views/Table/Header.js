import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';

export const GridHeader = ({
    fields = [],
    sortColumn,
    sortConfig,
    fieldsToShow,
    dataContainers,
    isInModal = false,
}) => {

    const [containerHasPostButton, setContainerHasPostButton] = useState();
    if (!fields) {
        return null;
    }


    useEffect(() => {
        if (dataContainers && dataContainers.length > 0) {
            const usingPostBtn = dataContainers.filter(x => x.settings?.postContainer === true).length;
            setContainerHasPostButton(usingPostBtn>0);
        }
    }, [dataContainers]);
    

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
                {containerHasPostButton && 
                    <th>&nbsp;</th>
                }
            </tr>
        </thead>
    );
};

GridHeader.propTypes = {
    fields: PropTypes.array,
};

GridHeader.displayName = 'GridHeader';
export default GridHeader;