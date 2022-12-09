import React, { Fragment, useState, useEffect, useRef, useCallback, } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import {
  setIsInEditMode, checkQuotaAndGetArticleList, addArticleToQuotaCategory, createQuotaCategory,
  setIsExistingQuota, resetArticlesFromAddList,
} from '../Actions/QuotaQuickSearch.action';
import { translate } from '../../Services/translation';
import SearchQuota from '../Components/SearchQuota';
import ArticleNumbersDropArea from '../Components/ArticleNumbersDropArea';

function QuotaFormContainer(props) {
  const { name, setValue, gridType } = props;
  const { query, result } = useSelector((state) => state.quotaQuickSearch);
  const { articlesList, existingArticlesList, isInEditMode, isExistingQuota } = useSelector((state) => state.quotaArticleFields);
  const btnIsActive = useSelector((state) => state.genericGridForm.btnIsActive);

  const dispatch = useDispatch();


  const quotaOnChange = (theQuotaNumber) => {
    // setQuotaNumber(theQuotaNumber);
    setValue(name, theQuotaNumber);
  };

  const saveChanges = (e) => {
    e.preventDefault();
    const type = gridType;

    if (articlesList.length > 0) {
      const result = articlesList.reduce((objArray = [], currentValue) => {
        const data = {
          type: gridType,
          articleNumber: currentValue,
          quotaId: query,
        };
        objArray.push(data);
        return objArray;
      }, []);

      dispatch(addArticleToQuotaCategory(type, result));
    }
    dispatch(setIsInEditMode(false))
  };


  const addNewQuotaCategory = (e) => {
    e.preventDefault();
    if (query.length > 0) {
      dispatch(createQuotaCategory(gridType, { QuotaFormField: query }));
      // dispatch(setIsInEditMode(true))
    }
  };

  const goBackToSearch = () => {
    dispatch(setIsInEditMode(false))
    dispatch(resetArticlesFromAddList())
  }

  const onEdit = (e) => {
    e.preventDefault();
    dispatch(setIsInEditMode(true))
    // e.currentTarget.disabled = true;
    // if (e.currentTarget.disabled) {
    //   console.log('Is Desabled ');
    // }
  }

  return (
    <Fragment>
      <div className="row quota__container collapse">
        <input id={name} type="hidden" defaultValue={query} />
        <div className="small-6 columns">
          {SearchQuota && (
            <SearchQuota
              label="Quota number"
              id={name}
              name={name}
              type="text"
              quotaOnChange={quotaOnChange}
            // isExistingQuota={isExistingQuota}
            // isInEditMode={isInEditMode}
            />
          )}
        </div>
        <div className="small-6 columns text--right">
          <label>&nbsp;</label>
          {isInEditMode ? (
            <div className="quota__container-top-buttons">
              <button className="button button-cyan" onClick={goBackToSearch}>
                Go back
              </button>
              <button className="button button-orange" disabled={articlesList.length == 0} onClick={(e) => saveChanges(e)}>
                Save changes
              </button>
            </div>
          ) : isExistingQuota ? (
            <button
              className="button button-green" onClick={(e) => onEdit(e)} >
              Edit Quota ({query})
            </button>
          ) : (
            <button className="button button-magenta" disabled={!isExistingQuota && query.length < 1} onClick={(e) => addNewQuotaCategory(e)} >
              Create new Quota
            </button>
          )}
        </div>
        {!isInEditMode && isExistingQuota && existingArticlesList.length > 0 && <div>{translate('quota.form.existingarticles')} {existingArticlesList.length} </div>}
        {isInEditMode && <ArticleNumbersDropArea />}
      </div>
      <div className="row">
        <div className="small-12 columns text--center">
          <input className={`form__button ${!btnIsActive ? "form__button--expand--disabled" : ""}`} disabled={!btnIsActive} type="submit"
            value={`${query.length == 0 ? translate('addons.genericgridview.form.button.submit.message') : translate('addons.genericgridview.form.button.submit.' + gridType.toLowerCase())} ${query.length > 0 ? query : ''}`} />
        </div>
      </div>

    </Fragment>
  );
}

export default QuotaFormContainer;
