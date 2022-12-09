import React, { Fragment, useState, useEffect, useRef, memo } from "react";
import CELLS from "./vendor/vanta.cells";
import CLOUDS2 from "./vendor/vanta.clouds";

import * as THREE from 'three';
const Background = (props) => {
    const [vantaEffect, setVantaEffect] = useState(0);
    const [theme, setTheme] = useState(props.theme || 'cells');
    const mainBg = useRef(null);
    const mapOptions = {
        mouseControls: false,
        touchControls: false,
        gyroControls: false,
        scale: 1.0,
        color1: 0x303904,
        color2: 0x52047f,
        THREE: THREE
    };
    const cloudOptions = {
        mouseControls: true,
        touchControls: false,
        gyroControls: true,
        scale: 1.00,
        backgroundColor: 0x0,
        skyColor: 0x4b95b9,// 0x68b8d7, // 0x99b5bf,
        cloudColor: 0xadc1de,
        cloudShadowColor: 0x183550,
        lightColor: 0xffffff,
        speed: .9,
        texturePath: "./images/noise.png",
        THREE: THREE
    };
    const [effectLoaded, setEffectLoaded] = useState(false);
    useEffect(() => {
        if (vantaEffect && effectLoaded) {
            vantaEffect.destroy();
            setVantaEffect(
                theme === 'clouds' ?
                    CLOUDS2({ ...cloudOptions, el: mainBg.current }) :
                    CELLS({ ...mapOptions, el: mainBg.current }));
        }
        return () => {
            if (vantaEffect) vantaEffect.destroy();
        };
    }, [theme]);

    useEffect(() => {
        if (!vantaEffect) {
            setVantaEffect(
                theme === 'clouds' ?
                    CLOUDS2({ ...cloudOptions, el: mainBg.current }) :
                    CELLS({ ...mapOptions, el: mainBg.current }));
            setEffectLoaded(true);
        }
        return () => {
            if (vantaEffect) vantaEffect.destroy();
        };
    }, [vantaEffect]);

    return (
        <Fragment>
            <div className={`pn-main-background__container pn-main-background__${theme}`} ref={mainBg}>
                <div className="pn-main-background__overlay"></div>
                <div className="pn-main-background__canvas"></div>
            </div>
        </Fragment>
    );
};

export default memo(Background);
