import React, { Fragment, useEffect, useState, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { translate } from '../../../Services/translation';
//import FolderOpenIcon from '../Icons/folder_open_fill.svg?component';

const InfiniteScrollButton = ({
    elementQs, totalPages, totalCount, currentPageIndex, pageSize, url, scrollType
}) => {
    const [componentInit, setComponentInit] = useState(false);
    const [currentPage, setCurrentPage] = useState(currentPageIndex);
    const [nextPageUrl, setNextPageUrl] = useState(url);
    const [currentTotal, setCurrentTotal] = useState(pageSize > totalCount ? totalCount : pageSize);
    const [isButtonDisabled, setIsButtonDisabled] = useState(currentTotal >= totalCount);
    const showMoreBtn = useRef(null);

    const showingText = translate('addons.infinitescroll.showingtext').replace('[currentTotal]', currentTotal).replace('[totalCount]', totalCount);
    const buttonText = translate('addons.infinitescroll.buttontext');

    const getNextPageItems = () => {
        fetch(nextPageUrl).then(function (response) {
            return response.text();
        }).then(function (html) {
            const parser = new DOMParser();
            const doc = parser.parseFromString(html, "text/html");
            if (doc) {
                const currentElementList = document.querySelector(elementQs);
                const newElementList = doc.querySelector(elementQs);

                if (currentElementList && newElementList) {
                    const elements = newElementList.querySelectorAll("li");
                    if (elements && elements.length > 0) {
                        elements.forEach((liEl) => {
                            // Init eventual new buy button
                            const newBuyButton = liEl.querySelector('buy-button');
                            if (newBuyButton) {
                                window.__pn.initNewBuyButton(newBuyButton);
                            }
                            currentElementList.appendChild(liEl);
                        });

                        // Update "next page" URL
                        const InfiniteScrollEl = doc.getElementById("InfiniteScroll");
                        if (InfiniteScrollEl) {
                            const newUrl = InfiniteScrollEl.dataset.url;
                            if (newUrl) {
                                setNextPageUrl(newUrl);
                            }
                        }
                        const newCurrentTotal = currentTotal + elements.length;
                        setCurrentTotal(newCurrentTotal);
                        setIsButtonDisabled(newCurrentTotal >= totalCount);
                    }
                }
            }
        }).catch(function (err) {
            console.warn('Error getting elements', err);
        });
    }

    useEffect(() => {
        if (!componentInit && scrollType === 'infinitescroll') {
            const InfiniteScrollEl = document.querySelector('#InfiniteScroll');
            if (InfiniteScrollEl) {
                var observer = new IntersectionObserver(function (entries) {
                    if (entries[0].isIntersecting === true && showMoreBtn) {
                        showMoreBtn.current.click();
                    }

                }, { threshold: [1] });
                setComponentInit(true);

                observer.observe(InfiniteScrollEl);
            }
        }
    }, []);

    return (
        <footer className="pn-infinite-scroll">
            <div>{showingText}</div>
            {(!isButtonDisabled || scrollType ==='showmorebutton') &&
                <button ref={showMoreBtn} className="button pn-infinite-scroll__button" onClick={() => getNextPageItems()} disabled={isButtonDisabled}>{buttonText}</button>
            }
        </footer>
    );
};
export default InfiniteScrollButton;
