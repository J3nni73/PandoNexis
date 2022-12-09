import React, { Fragment } from 'react';

const sameCategory = (item, index, array) =>
    (index > 0 && array[index - 1].category === array[index].category) ||
    item.showAll;

const QuotaSearchResult = ({ result, selectedItem, searchUrl, handleChange }) => (
    <ul className="quota__search-result">
        {result &&
            result.map((item, index, array) => (
                <Fragment key={`${item.name}-${index}`}>
                    {item.category === 'NoHit' ||
                        sameCategory(item, index, array) ? null : (
                        <li className="quota__search-result__item quota__search-result__group-header">
                            {item.category}
                        </li>
                    )}
                    <li
                        onClick={() => handleChange(item.name)}
                        className={`quota__search-result__item ${selectedItem === index
                                ? 'quota__search-result__item--selected'
                                : ''
                            }`}
                    >
                        {!item.showAll && (
                            <Fragment>
                                {item.hasImage && item.imageSource && (
                                    <img
                                        className="quota__search-result__image"
                                        src={item.imageSource}
                                    />
                                )}
                                <div>{item.name}</div>
                            </Fragment>
                        )}
                    </li>
                </Fragment>
            ))}
    </ul>
);

export default QuotaSearchResult;
