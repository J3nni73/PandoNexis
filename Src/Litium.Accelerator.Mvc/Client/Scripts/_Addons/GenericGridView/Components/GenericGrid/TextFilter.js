import React from 'react';
import PropTypes from 'prop-types';

export const GenericGridTextFilter = ({
    label,
    onChange,
    refer,
    name
}) => {

    const onKeyPress = (event) => {
        if (event.keyCode === 13){
            event.preventDefault();
            onChange();
        }
    };

    return (
        <input className="generic-grid-view__string" type="text" onKeyDown={event => onKeyPress(event)} onBlur={onChange} name={name} placeholder={label} ref={refer}/>
    );
};

GenericGridTextFilter.propTypes = {
    label: PropTypes.string,
    onChange: PropTypes.func,
    name: PropTypes.string
};

GenericGridTextFilter.displayName = 'GenericGridTextFilter';
