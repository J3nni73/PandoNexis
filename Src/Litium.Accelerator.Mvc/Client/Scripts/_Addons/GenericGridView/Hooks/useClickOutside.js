import { useRef, useState, useEffect } from "react";
import useCustomRef from "./useCustomRef";
import useEventListener from "./useEventListener"


export default function useClickOutside() {
    const { setRef, elementRef } = useCustomRef();
    const [elementName, setElementName] = useState('')
    const [isOutSide, setIsOutSide] = useState(false);

    useEffect(() => {
        if (elementRef.current && elementName) {
            elementRef.current[elementName].focus();

            document.addEventListener("click", (e) => {
                if (!elementRef.current[elementName].contains(e.target) && elementRef.current[elementName].value !== '') {
                    setIsOutSide(true)
                }
                else {
                    setIsOutSide(false)
                }
            }, elementRef.current[elementName]);
        }
    }, [elementRef, setElementName, elementName])

    const refIfClickedOut = (name) => {
        if (elementRef.current[name]) {
            name && name !== elementName && setElementName(name)
        }
        return setRef(name)
    }
    return { refIfClickedOut, isOutSide }
}