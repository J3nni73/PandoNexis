import React, { useEffect, useRef, useState } from 'react';

export default function LoadingSpinner({ text }) {

    const ref = useRef(null);

    const [height, setHeight] = useState(0);
    const [width, setWidth] = useState(0);

    useEffect(() => {
        setHeight(ref.current.offsetHeight);
        setWidth(ref.current.offsetWidth);

        // 👇️ if you need access to parent
        // of the element on which you set the ref
        // console.log(ref.current.parentElement);
        // console.log(ref.current.parentElement.offsetHeight);
        // console.log(ref.current.parentElement.offsetWidth);
        // ref.current.parentElement.classList.add('parentSpinner')
    }, []);

    return (
        <div className='loader__container' ref={ref}>
            <div className="loader__container__loadingspinner" id="loader"></div>
            <p className="loader__container--text"> {text} </p>
        </div>
    )
}
