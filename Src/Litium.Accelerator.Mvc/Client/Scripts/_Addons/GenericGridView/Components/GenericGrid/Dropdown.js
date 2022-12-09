import React from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import { translate } from '../../../../Services/translation';

const className = {
  button: (isOpen) =>
    classNames('generic-grid-view__button', 'dropdown', 'secondary', {
      hollow: isOpen,
    }),
  pane: (isOpen) =>
    classNames('generic-grid-view__pane', {
      'is-open': isOpen,
    }),
};

export const GenericGridDropdown = ({
  children,
  label,
  isOpen,
  onClick,
  onChange,
  align = 'left',
}) => {
  return (
    <div className="generic-grid-view__dropdown">
      <button className={className.button(isOpen)} onClick={onClick}>
        {label}
      </button>
      <div className={className.pane(isOpen)} style={{ [align]: 0 }}>
        <div className={`children-container is-open-${isOpen}`}>{children}</div>
        <button className="generic-grid-view__button" onClick={onChange}>
          {translate('general.select')}
        </button>
      </div>
    </div>
  );
};

GenericGridDropdown.propTypes = {
  children: PropTypes.node,
  label: PropTypes.string,
  isOpen: PropTypes.bool,
  onClick: PropTypes.func,
  onChange: PropTypes.func,
  align: PropTypes.oneOf(['left', 'right']),
};

GenericGridDropdown.displayName = 'GenericGridDropdown';
