import React from 'react';
import PropTypes from 'prop-types';

export const GenericDataViewPagination = ({ current, total, onChange }) => (
    <nav aria-label="Pagination">
        <ul className="generic-data-view__pagination">
            <li>
                <button
                    className="button secondary small"
                    aria-label="Previous Page"
                    disabled={current === 1}
                    onClick={() => onChange(current - 1)}
                >
                    <i className={`fas fa-angle-left`} role="presentation"></i>
                </button>
            </li>
            <li>
                <select
                    value={current}
                    onChange={({ target }) => onChange(target.value)}
                >
                    {new Array(total).fill().map((e, index) => (
                        <option key={index} value={index + 1}>
                            {index + 1}
                        </option>
                    ))}
                </select>
            </li>
            <li>
                <button
                    className="button secondary small"
                    aria-label="Next Page"
                    disabled={current === total}
                    onClick={() => onChange(current + 1)}
                >
                    <i className={`fas fa-angle-right`} role="presentation"></i>
                </button>
            </li>
        </ul>
    </nav>
);

GenericDataViewPagination.propTypes = {
    onChange: PropTypes.func,
    current: PropTypes.number,
    total: PropTypes.number,
};
