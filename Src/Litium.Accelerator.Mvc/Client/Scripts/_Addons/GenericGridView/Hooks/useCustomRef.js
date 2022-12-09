import { useRef, useState, useEffect } from 'react'


export default function useCustomRef() {
    const elementRef = useRef({});

    const [ready, setReady] = useState(false);
    const [elementsName, setElementsName] = useState([])

    useEffect(() => {
        if (elementRef.current) {
            setReady(true)
        }
        else {
            setReady(false)
        }
    }, [elementRef]);
    const setRef = (name = null) => {

        if (name && ready && !elementsName.some(x => x.toLowerCase() === name.toLowerCase())) setElementsName(prevState => [...prevState, name]);
        return {
            name: name,
            ref: (element) => {
                elementRef.current[name] = element;
            }

        }
    }

    return { setRef, elementsName, ready, elementRef };
}
