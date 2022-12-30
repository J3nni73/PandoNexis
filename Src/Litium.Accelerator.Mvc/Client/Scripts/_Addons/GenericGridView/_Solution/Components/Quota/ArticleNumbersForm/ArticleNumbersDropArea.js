import React, { Fragment, useState, useEffect, useRef, useCallback, } from 'react';

import { useSelector, useDispatch } from 'react-redux';

import { addArticleToQuotaCategory, removeArticleFromOuotaCategory, checkAndSetArticlesNumberToList, removeArticlesTempListForAdd, setArticlesWillBeAddedToCategory } from '../../../Actions/Quota/QuotaQuickSearch.action';
import RemoveAllArticlesComponent from './RemoveAllArticles';
import { translate } from '../../../../../../Services/translation';
import useBtnInProgress from '../../../../Hooks/useBtnInProgress';
import RemoveOneArticle from './RemoveOneArticle';


function ArticleNumbersDropArea({ label, name, register, type }) {
  // const { label, name, register, type } = props;
  const { articlesList, existingArticlesList } = useSelector((state) => state.quotaArticleFields);
  const { query } = useSelector((state) => state.quotaQuickSearch);
  const { currentDataType } = useSelector((state) => state.genericGridView);
  const dispatch = useDispatch();
  const { setInProgress } = useBtnInProgress();

  const articleNumbersEl = useRef(null);
  const [articleNumbers, setArticleNumbers] = useState([]);
  const [isChecked, setIsChecked] = useState({});
  const [removeData, setRemoveData] = useState([]);
  const [alreadyAdded, setAlreadyAdded] = useState([]);


  useEffect(() => {
    articleNumbersEl.current.focus();
    // Bind the event listener
    document.addEventListener("mousedown", detectedByKeyPress);
    return () => {
      // Unbind the event listener on clean up
      document.removeEventListener("mousedown", detectedByKeyPress);
    };

  }, [articleNumbersEl, articlesList, existingArticlesList])


  const checkBoxOnChange = useCallback((event, variant) => {

    setIsChecked({ ...isChecked, [event.target.name]: event.target.checked });
    const data = {
      type: currentDataType,
      articleNumber: variant,
      baseProductSystemId: '',
      quotaId: query,
    };
    if (event.target.checked) {
      setRemoveData((preveState) => [...preveState, data]);
    } else {
      setRemoveData(removeData.filter((x) => x.articleNumber !== event.target.name));
    }
  }, [setIsChecked, isChecked, setRemoveData]);

  const handleAddArticle = (event, variant) => {
    event.preventDefault();
    const data = [
      {
        articleNumber: variant,
        quotaId: query,
      },
    ];
    dispatch(addArticleToQuotaCategory((type = currentDataType), data));
  };

  const detectedByKeyPress = useCallback((e) => {

    const onClickOutSide = articleNumbersEl.current && !articleNumbersEl.current.contains(e.target) && articleNumbersEl.current.value !== ''

    let charCode = String.fromCharCode(e.which).toLowerCase();
    if ((e.ctrlKey || e.metaKey) && charCode === 'v' || e.key === 'Enter' || e.key === ' ' || onClickOutSide) {
      setTimeout(() => {
        const textValue = articleNumbersEl.current.value;
        const articlesStringList = getListOfStrings(textValue);
        // e.preventDefault();
        articlesStringList.forEach((articleNumber, index) => {
          const isAlreadyExisting = existingArticlesList.some((element, i) => {
            if (element.id) {
              let che = element.id == articleNumber
              if (che) {
                setAlreadyAdded(preveState => ([...preveState, { articleNumber: articleNumber, info: `This is the article that has already been added to the ${query}` }]));
              }
              return che;
            }
          });
          const isAlreadyInAddList = articlesList.some((element, i) => {
            if (element) {
              let che = element == articleNumber
              if (che) {
                setAlreadyAdded(preveState => ([...preveState, { articleNumber: articleNumber, info: 'This is article Already in additional steps' }]));
              }
              return che;
            }
          });
          if (articleNumber.length < 0 || (!isAlreadyExisting && !isAlreadyInAddList)) {
            // dispatch(checkAndSetArticlesNumberToList(articleNumber));
            dispatch(setArticlesWillBeAddedToCategory([articleNumber]))
          }

        });
        articleNumbersEl.current.value = '';
      }, 200);
    }
  }, [articlesList, existingArticlesList])

  const getListOfStrings = (val) => {
    // Get value and replace with space
    const articles = val
      .replaceAll('\t', ' ')
      .replaceAll('\n', ' ')
      .replaceAll('\r', ' ')
      .replaceAll(',', ' ')
      .replaceAll(';', ' ')
      .trim();
    return articles.split(' ');
  };


  return (
    <Fragment>
      <hr />
      <div className="small-12 columns">
        <label htmlFor="articleNumbers">Article numbers</label>
        <textarea
          id="articleNumbers"
          name="articleNumbers"
          ref={articleNumbersEl}
          onChange={(event) => { event.stopPropagation(), event.preventDefault() }}
          onKeyDown={(e) => detectedByKeyPress(e)}
        />
      </div>
      <div className="small-12 columns variant-list">
        <ul
          className={`article-numbers article-numbers--info`} >
          {alreadyAdded && alreadyAdded.length > 0 && alreadyAdded.map((variant, i) => (
            <li className="" key={`articleIndex-${variant}-${i}`}>
              <span className='button button-small'>
                <span className='material-icons'>&#8505;</span>
                <span className='label-hidden' >{variant.info} </span>
              </span>
              <span>
                {variant.articleNumber}
              </span>
            </li>
          )
          )}
        </ul>
      </div>
      <div className="small-12 columns variant-list">
        {(articlesList && articlesList.length > 0) || (existingArticlesList && existingArticlesList.length > 0) &&
          <h4>Variants</h4>
        }
        {
          removeData.length > 0 &&
          <RemoveAllArticlesComponent
            items={removeData} type={currentDataType}
            setRemoveData={setRemoveData} />
        }
        {articlesList && articlesList.length > 0 && (
          // <ArticlesWillBeAddedToCategory articles={articlesList} />
          <Fragment>
            <h5>New Articles</h5>
            <ul
              className={`article-numbers article-numbers--pending`} >
              {articlesList &&
                articlesList?.map((variant, index2) => (
                  <li className="" key={`articleIndex-${variant}-${index2}`}>
                    <span className='pending--article'>
                      {variant}
                    </span>
                    <span className="actions">
                      <input type="checkbox" id={variant} name={variant} value={variant} onChange={(event) => checkBoxOnChange(event, variant)} title={translate('Check to remove')} />
                      <button className="article-numbers__add" name={variant} {...setInProgress(variant)} onClick={(event) => handleAddArticle(event, variant)}>
                        <span className="button__text">Add</span>
                      </button>
                    </span>
                  </li>
                )
                )}
            </ul>
          </Fragment>
        )}
        {existingArticlesList && existingArticlesList.length > 0 && (
          // <ExistingArticlesInCategory articles={existingArticlesList} />
          <Fragment>
            <ul className={`article-numbers article-numbers--existing`} >
              {existingArticlesList &&
                existingArticlesList?.map((variant, index2) => (
                  <li className="" key={`articleIndex-${variant?.id}-${index2}`}>
                    {variant &&
                      variant.fields &&
                      variant.fields.SupplierItemNumber ? (
                      <span className='existing--article' > {variant.fields.SupplierItemNumber['*']} </span>
                    ) : (
                      <span className='existing--article'> {variant.id} </span>
                    )}
                    <RemoveOneArticle variant={variant} type={currentDataType} query={query} />
                  </li>
                ))}
            </ul>
          </Fragment>
        )}
      </div>
    </Fragment>
  );
}

export default ArticleNumbersDropArea;
