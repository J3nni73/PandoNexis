import { post, get, patch } from '../../../../Services/http';
import { catchError } from '../../../../Actions/Error.action';
import { changeSubmitBtnActivity } from '../../Actions/GenericGridForm.action';
import { categoryById, updateFielsInGrid } from '../../Actions/GenericGridView.action';

import { GENERIC_GRID_FIELD_SET_QUOTA_EXISTING, GENERIC_GRID_FIELD_SET_QUOTA_SEARCH_LIST, GENERIC_GRID_FIELD_SET_QUOTA_EDIT, GENERIC_GRID_FIELD_SET_CURRENT_QUOTA } from '../constants';
import {
    GENERIC_SEARCH_QUERY, GENERIC_SEARCH_RECEIVE, GENERIC_SEARCH_ERROR, GENERIC_SEARCH_SHOW_FULL_FORM, GENERIC_SEARCH_SELECT_ITEM,
    GENERIC_SEARCH_TOGGLE_SHOW_RESULT, GENERIC_SEARCH_QUERY_GRIDTYPE, GENERIC_GRID_VIEW_SET_SEARCH_PENDING_STATUS,
    GENERIC_GRID_ROW_UPDATE, GENERIC_GRID_FIELD_SET_ARTICLES_LIST, GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST, GENERIC_GRID_FIELD_RESET_STATE,
    GENERIC_SEARCH_RESET_STATE, SET_BUTTON_INPROGRESS
} from '../../constants';

const rootRouteTodo = '/api/quotagridview/';

let abortController;

export const searchByQuotaCategoryNumber = (quotaNumber) => (dispatch, getState) => {
  const type = getState().genericGridView.currentDataType;
  // abort any existing, pending request.
  // It's ok to call .abort() after the fetch has already completed, fetch simply ignores it.
  abortController && abortController.abort();
  abortController = new AbortController();
  //return post('/api/quickSearch', q, abortController)

  dispatch(setIsExistingQuota(false))
  return get(rootRouteTodo + `searchQuotaCategoryNumber/${type}/` + encodeURI(quotaNumber))
    .then((response) => response.json())
    .then((result) => {
      if (result.some(x => x.isExactMatch)) {
        // dispatch(changeSubmitBtnActivity(true));
        dispatch(checkQuotaAndGetArticleList(quotaNumber));
        dispatch(setCurrentQuota(quotaNumber));
        dispatch(setIsExistingQuota(true))
      }
      dispatch(receive(result))
    })
    .catch((ex) => dispatch(catchError(ex, (error) => searchError(error))));
};

export const removeArticlesTempListForAdd = (data) => (dispatch, getState) => {

  let articlesTempListForAdd = [...getState().quotaArticleFields.articlesList];

  const filterByReference = (arr1, arr2) => {
    let res = [];
    res = arr1.filter((el) => {
      return !arr2.find((element) => {
        return element.articleNumber === el;
      });
    });
    return res;
  };

  const articlesList = filterByReference(articlesTempListForAdd, data);
  dispatch({
    type: GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    payload: {
      articlesList,
    },
  });
};

const removeArticleFromExistingList = (data) => (dispatch, getState) => {
  dispatch(btnProgress(data))
  let articlesTempListOlds = [
    ...getState().quotaArticleFields.existingArticlesList,
  ];

  const filterByReference = (arr1, arr2) => {
    let res = [];
    res = arr1.filter((el) => {
      return !arr2.find((element) => {
        return element.articleNumber === el.id;
      });
    });
    return res;
  };
  const existingArticlesList = filterByReference(articlesTempListOlds, data);
  if (existingArticlesList.length == 0 || articlesTempListOlds.length == 0) {
    dispatch(changeSubmitBtnActivity(false));
  }
  dispatch({
    type: GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    payload: {
      existingArticlesList,
    },
  });
};

const addArticleToList = (data) => (dispatch, getState) => {
  let articlesTempListOlds = [...getState().quotaArticleFields.articlesList];
  let existingArticlesListOlds = [...getState().quotaArticleFields.existingArticlesList];

  dispatch(changeSubmitBtnActivity(true));

  const existingArticlesList = [...existingArticlesListOlds, ...data];
  dispatch({
    type: GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    payload: {
      existingArticlesList,
    },
  });

  const filterByReference = (arr1, arr2) => {
    let res = [];
    res = arr1.filter((el) => {
      return !arr2.find((element) => {
        return element.id === el;
      });
    });
    return res;
  };

  const articlesList = filterByReference(articlesTempListOlds, data);

  dispatch(btnProgress(data))
  dispatch({
    type: GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    payload: {
      articlesList,
    },
  });
};

export const createQuotaCategory = (type, data, fields) => (dispatch, getState) => {
  dispatch(resetQuotaArticleFieldsState())
  return post(rootRouteTodo + 'createNewQuota/' + type, data)
    .then((response) => response.json())
    .then((response) => {
      // dispatch(checkRowResponse(response, fields));
      // dispatch(resetQuotaArticleFieldsState())
      dispatch(setIsInEditMode(true))
      dispatch(setIsExistingQuota(true))
    })
    .catch((ex) =>
      // add update Error if needed
      dispatch(catchError(ex, (error) => updateError(error, fields)))
    );
};
// Todo Not used
export const setCurrentQuota = (currentQuery) => {
  return {
    type: GENERIC_GRID_FIELD_SET_CURRENT_QUOTA,
    payload: {
      currentQuery,
    },
  };
};

export const addArticleToQuotaCategory = (type, data) => (dispatch, getState) => {
  dispatch(btnProgress(data))
  return patch(rootRouteTodo + 'addArticleToQuotaCategory/' + type, data)
    .then((response) => response.json())
    .then((response) => {
      dispatch(addArticleToList(response));
      dispatch(getAddedFieldsToGridView(type, response));

    })
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));

};

export const getAddedFieldsToGridView = (type, data) => (dispatch, getState) => {
  const getGenericGridViewState = [...getState().genericGridView.dataRows];
  const getSystemId = data.reduce((objArray = [], currentValue) => {
    const data = {
      systemIds: currentValue.systemId,
    };
    objArray.push(data);
    return objArray;
  }, []);

  return post(rootRouteTodo + `getAddedFieldsToGridView/${type}`, getSystemId)
    .then((response) => response.json())
    .then((response) => {
      const fieldsEfterFilter = [...getGenericGridViewState, ...response.dataRows]
      dispatch(updateFielsInGrid(fieldsEfterFilter));
    })
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));
}

export const removeArticleFromOuotaCategory = (type, data) => (dispatch, getState) => {
  const getGenericGridViewState = [...getState().genericGridView.dataRows];
  dispatch(btnProgress(data))
  return patch(rootRouteTodo + 'removeArticleFromOuotaCategory/' + type, data)
    .then((response) => response.json())
    .then((response) => {
      dispatch(removeArticleFromExistingList(data));

      if (getGenericGridViewState.length > 0 && response) {
        const fieldsEfterFilter = getGenericGridViewState.filter((element, index) => {
          if (element.fields[index].entitySystemId) {
            const checkResponse = !response.find(r => r.systemId == element.fields[index].entitySystemId);
            return checkResponse;
          }
        });
        dispatch(updateFielsInGrid(fieldsEfterFilter));

      }
    })
    .catch((ex) => dispatch(catchError(ex, (error) => loadError(error))));

};

export const checkAndSetArticlesNumberToList = (articleNumber) => (dispatch, getState) => {
  return get(rootRouteTodo + `checkAndGetArticle/${type}/` + encodeURI(articleNumber))
    .then((response) => response.json())
    .then((result) => dispatch(receiveAndSetArticle(result)));
};

export const checkQuotaAndGetArticleList = (quotaNumber) => (dispatch, getState) => {
  const type = getState().genericGridView.currentDataType;
  return get(
    rootRouteTodo + `checkQuotaAndGetArticleList/${type}/` + encodeURI(quotaNumber)
  )
    .then((response) => response.json())
    .then((result) => {
      if (result.length > 0) {
        dispatch(changeSubmitBtnActivity(true));
        dispatch(setexistingArticlesList(result))
      } else {
        dispatch(changeSubmitBtnActivity(false));
        dispatch(setexistingArticlesList(result))
      }
    });
};

export const receiveAndSetArticle = (data) => (dispatch, getState) => {
  let articlesList = [...getState().quotaArticleFields.articlesList];
  const items = articlesList.filter((x) => x.id === data.id);

  if (items.length > 0) {
    return false;
  }
  articlesList.push(data);
  dispatch({
    type: GENERIC_GRID_FIELD_SET_ARTICLES_LIST,
    payload: {
      articlesList,
    },
  });
};

export const setexistingArticlesList = (existingArticlesList) => (dispatch, getState) => {
  dispatch({
    type: GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    payload: {
      existingArticlesList,
    },
  });
};
// New Function
export const setArticlesWillBeAddedToCategory = (data) => (dispatch, getState) => {
  let articlesList = [...getState().quotaArticleFields.articlesList];
  articlesList.push(...data);
  dispatch({
    type: GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    payload: {
      articlesList,
    },
  });
};

export const resetArticlesFromAddList = (existingArticlesList) => (dispatch, getState) => {
  dispatch({
    type: GENERIC_GRID_FIELD_SET_EXISTING_ARTICLES_LIST,
    payload: {
      articlesList: [],
    },
  });
};

//--------duplicate-----------

// export const searchQuota = (quotaNumber) => (dispatch, getState) => {
//   return get(rootRouteTodo + 'searchQuota/' + encodeURI(quotaNumber))
//     .then((response) => response.json())
//     .then((result) => dispatch(fillQuotaSearchList(result)));
// };

export const setIsInEditMode = (isInEditMode) => {
  return {
    type: GENERIC_GRID_FIELD_SET_QUOTA_EDIT,
    payload: {
      isInEditMode,
    },
  };
};

/**
 *  This To show Edit Quota Step
 * @param {boolean} isExistingQuota 
 * @returns Boolean
 */
export const setIsExistingQuota = (isExistingQuota) => {
  return {
    type: GENERIC_GRID_FIELD_SET_QUOTA_EXISTING,
    payload: {
      isExistingQuota,
    },
  };
};

// Todo Not used
export const fillQuotaSearchList = (quotaSearchList) => (
  dispatch,
  getState
) => {
  dispatch({
    type: GENERIC_GRID_FIELD_SET_GENERIC_SEARCH_LIST,
    payload: {
      quotaSearchList,
    },
  });
};

export const setSearchQuery = (query) => ({
  type: GENERIC_SEARCH_QUERY,
  payload: {
    query,
  },
});

export const searchError = (error) => ({
  type: GENERIC_SEARCH_ERROR,
  payload: {
    error,
  },
});

export const receive = (result) => ({
  type: GENERIC_SEARCH_RECEIVE,
  payload: {
    result,
    showResult: result && result.length > 0,
  },
});

export const toggleShowResult = (showResult) => ({
  type: GENERIC_SEARCH_TOGGLE_SHOW_RESULT,
  payload: {
    showResult: showResult,
  },
});

export const toggleShowFullForm = () => (dispatch, getState) => {
  dispatch(show(!getState().GENERICSearch.showFullForm));
};

const show = (visible) => ({
  type: GENERIC_SEARCH_SHOW_FULL_FORM,
  payload: {
    showFullForm: visible,
  },
});

export const handleKeyDown = (event, opt) => (dispatch, getState) => {
  const { result, selectedItem } = getState().quotaQuickSearch;

  if (!result || !result.length) {
    return;
  }
  const max = result.length - 1,
    clip = (index) => (index < 0 ? max : index > max ? 0 : index);
  switch (event.keyCode) {
    case 38:
      dispatch(selectItem(clip(selectedItem - 1)));
      break;
    case 40:
      dispatch(selectItem(clip(selectedItem + 1)));
      break;
    case 13:
      //const selectedObject = result[selectedItem];
      //if (selectedObject && selectedObject.url) {
      //  location.href = selectedObject.url;
      //} else {
      //  location.href = opt.searchUrl;
      //}
      break;
    default:
      break;
  }
};

export const handleClickSearch = (opt) => (dispatch, getState) => {
  const { result } = getState().quotaQuickSearch;

  if (!result || !result.length) {
    return;
  }
  location.href = opt.searchUrl;
};

export const selectItem = (selectedItem) => ({
  type: GENERIC_SEARCH_SELECT_ITEM,
  payload: {
    selectedItem,
  },
});

export const loadError = (error) => {
  console.log('loadError', error);
  // type: GENERIC_GRID_FIELD_LOAD_ERROR,
  // payload: {
  //   error,
  // },
};

export const resetQuotaArticleFieldsState = () => ({
  type: GENERIC_GRID_FIELD_RESET_STATE,
  payload: {
    articlesList: [],
    existingArticlesList: [],
    quotaSearchList: [],
  }
});

export const resetQuotaQuickSearchState = () => ({
  type: GENERIC_SEARCH_RESET_STATE,
  payload: {
    gridType: '',
    query: '',
    result: [],
    showResult: false,
    showFullForm: false,
    selectedItem: -1,
  }
})

export const btnProgress = (element) => (dispatch, getState) => {
  const inProgress = getState().quotaArticleFields.inProgress;
  let inProgressNew = [];
  Array.isArray(element) && element.map(el => {
    if (!inProgress.includes(el.articleNumber)) {
      inProgressNew.push(el.articleNumber);
    }
    else {
      inProgressNew = inProgress.filter(x => x !== el.articleNumber)
    }
  })

  dispatch({
    type: SET_BUTTON_INPROGRESS,
    payload: {
      inProgress: inProgressNew,
    },
  });
}