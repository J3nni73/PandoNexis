import React, { useState, useEffect, useCallback } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import useCustomRef from './useCustomRef';

export default function useBtnInProgress() {
    const dispatch = useDispatch();
    const inProgress = useSelector((state) => state.quotaArticleFields.inProgress);
    const { setRef, elementRef } = useCustomRef();

    const [btnName, setbtnName] = useState([]);
    const [isInProgress, setIsInProgress] = useState([])

    const setInProgress = (name) => {
        if (name && !btnName.includes(name)) {
            setbtnName([...btnName, name])
        }
        return setRef(name);
    }

    useEffect(() => {
        inProgress.map(x => {
            if (elementRef.current[x] && !isInProgress.some(el => el === x)) {
                let span = document.createElement("span");
                span.classList.add('loader__container--btn')
                elementRef.current[x].insertAdjacentElement('afterbegin', span)
                elementRef.current[x].setAttribute("disabled", "")
                elementRef.current[x].style = 'pointer-events: none'
                setIsInProgress([...isInProgress, x])
            }
        })

    }, [elementRef.current, inProgress])
    return { isInProgress, setInProgress }
}
