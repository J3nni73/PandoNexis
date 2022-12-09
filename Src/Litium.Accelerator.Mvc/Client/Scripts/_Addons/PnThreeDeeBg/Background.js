import React, { Fragment, useState, useEffect, useRef, memo } from "react";
import CELLS from "./vendor/vanta.cells";
import CLOUDS2 from "./vendor/vanta.clouds";

import * as THREE from 'three';
const Background = (props) => {
    const { theme } = props
    const [vantaEffect, setVantaEffect] = useState(0);
    const mainBg = useRef(null);
    const mapOptions = {
        mouseControls: true,
        touchControls: true,
        gyroControls: false,
        minHeight: 200.0,
        minWidth: 200.0,
        scale: 1.0,
        color1: 0x303904,
        THREE: THREE
    };
    const cloudOptions = {
        mouseControls: true,
        touchControls: false,
        gyroControls: true,
        minHeight: 200.00,
        minWidth: 200.00,
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
            <div className="main-background" ref={mainBg}>
                <div className="main-background__overlay"></div>
                <div className="main-background__map"></div>
            </div>
        </Fragment>
    );
};

export default memo(Background);
