import React, { useRef, useState, useEffect, useCallback } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { searchByQuotaCategoryNumber, handleKeyDown, setSearchQuery, toggleShowFullForm, toggleShowResult, selectItem } from '../Actions/QuotaQuickSearch.action';
import QuotaSearchResult from './QuotaSearchResult';
import * as debounce from 'lodash.debounce';
import usePrevious from '@react-hook/previous';

import { useFormContext, useController } from "react-hook-form";
import TextError from './TextError';
import { changeSubmitBtnActivity } from '../../PandoNexis/Actions/GenericGridForm.action';

// debouncing function to 200ms so we don't send query request on every key stroke
const debouncedQuery = debounce(
  (dispatch, text) => dispatch(searchByQuotaCategoryNumber(text)),
  200
);

function SearchQuota({ label, name, type, settings, style, control, quotaOnChange }) {

  const { query, result, showResult, showFullForm, selectedItem } = useSelector((state) => state.quotaQuickSearch);

  const { isInEditMode, isExistingQuota } = useSelector((state) => state.quotaArticleFields);

  const dispatch = useDispatch();

  const { register } = useFormContext({ control, name: name });


  const {
    field: { onChange, onBlur, value, ref },
    fieldState: { invalid, isTouched, isDirty },
    formState: { touchedFields, dirtyFields }
  } = useController({
    name,
    control,
    rules: { required: true },
    defaultValue: query || "",
  });

  const previousSelectedItem = usePrevious(selectedItem);

  const [lastClickedNode, setLastClickedNode] = useState(null);

  const searchContainer = useRef(null);
  const searchInput = useRef(null);

  const required = type == 'string' ? required : '';
  const searchUrl = window.__litium.quickSearchUrl + (query.length > 0 ? `?q=${query}` : '');
  const handleChange = (searchText) => {
    setQueryStringToURL(searchText);
    const text = encodeURIComponent(searchText);
    onChange(name, text) // This one to Check if something change and flag to form
    dispatch(setSearchQuery(text));
    debouncedQuery(dispatch, text);
    quotaOnChange(text);
    dispatch(changeSubmitBtnActivity(false));

  };

  const setQueryStringToURL = (searchInput) => {
    let searchParams = new URLSearchParams(window.location.search);
    // searchParams.delete("id");
    if (searchInput) {

      searchParams.set("id", searchInput);

      if (window.history.replaceState) {
        const url = window.location.protocol
          + "//" + window.location.host
          + window.location.pathname
          + "?"
          + searchParams.toString();

        window.history.replaceState({
          path: url
        }, "", url)
      };

    }
  }

  const clickHandler = useCallback((event) => {
    setLastClickedNode(event.target);
  }, []);

  useEffect(() => {
    // setUrlQueryString()
    // listen for click event to hide the search when clicking outside
    window.addEventListener('mousedown', clickHandler);
    return () => window.removeEventListener('mousedown', clickHandler);
  }, [clickHandler]);

  useEffect(() => {
    const q = decodeURIComponent(query);
    if (!q) {
      const params = new URLSearchParams(window.location.search);
      if (params) {
        const id = params.get("id");
        if (id && id.length > 1) {
          handleChange(id);
        }
      }
    }
    if (searchInput.current) searchInput.current.focus();
  }, []);

  useEffect(() => {
    if (selectedItem !== previousSelectedItem) {
      const el = document.querySelector('.quick-search-result__item--selected');
      el &&
        el.scrollIntoView({
          behavior: 'smooth',
          block: 'end',
          inline: 'nearest',
        });
    }
  }, [selectedItem, previousSelectedItem]);

  // console.log('searchContainer.current query --', searchContainer.current);
  getSettingsKeys(settings);
  return (
    <div className="row collapse">
      <div className="small-12 columns quota__search" ref={searchContainer}>
        <label>
          {label}
          <input
            placeholder={label}
            id={name}
            type={type}
            name={name}
            disabled={isInEditMode}
            {...register(name)}
            required={required}
            style={style}
            autoComplete="off"
            value={decodeURIComponent(query)}
            onChange={(event) => handleChange(event.target.value)}
            // onKeyDown={(event) => dispatch(handleKeyDown(event, { searchUrl }))}
            ref={searchInput}
            //onFocus={(event) => handleChange(event.target.value)}
            onBlur={() => {
              if (searchContainer.current && !searchContainer.current.contains(lastClickedNode)) {
                showFullForm && dispatch(toggleShowFullForm());
                dispatch(toggleShowResult(false));
              }
            }}
          />
          <span className="form-error">
            {/* <ErrorMessage component={TextError} name={name} /> */}
          </span>
        </label>
        {settings?.errorFieldMessage && (
          <span className="generic-grid-view__error-field-message"
            dangerouslySetInnerHTML={{
              __html: settings.errorFieldMessage,
            }}
          ></span>
        )}

        {showResult && !isExistingQuota && (
          <QuotaSearchResult
            result={result}
            selectedItem={selectedItem}
            searchUrl={searchUrl}
            handleChange={handleChange}
          />
        )}
      </div>
    </div>
  );
}

export default SearchQuota;

const getSettingsKeys = (data) => {
  if (data === null || data == undefined) return '';
  Object.keys(data).filter((v) => {
    if (data[v] === true) {
      console.log(v);
    }
  });
};
