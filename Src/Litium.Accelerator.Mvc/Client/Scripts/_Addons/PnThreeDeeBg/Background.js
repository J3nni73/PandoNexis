import React, { Fragment, useState, useEffect, useRef, memo } from 'react';
import CELLS from './vendor/vanta.cells';
import CLOUDS2 from './vendor/vanta.clouds';
import HALO from './vendor/vanta.halo';
import RIPPLE from './vendor/vanta.ripple';
import WAVES from './vendor/vanta.waves';

import * as THREE from 'three';
const Background = (props) => {
    const [vantaEffect, setVantaEffect] = useState(0);
  const [theme, setTheme] = useState(props.theme || 'waves');
    const mainBg = useRef(null);
    const cellsOptions = {
    backgroundColor: 0xff11aa,
        mouseControls: false,
        touchControls: false,
        gyroControls: false,
    scale: 1.0,
        color1: 0x120096, 
        color2: 0xff11aa,
    THREE: THREE,
    };
    const cloudOptions = {
        mouseControls: true,
        touchControls: false,
        gyroControls: true,
    scale: 1.0,
        backgroundColor: 0x0,
    skyColor: 0x4b95b9, // 0x68b8d7, // 0x99b5bf,
        cloudColor: 0xadc1de,
        cloudShadowColor: 0x183550,
        lightColor: 0xffffff,
    speed: 0.9,
    texturePath: './images/noise.png',
    THREE: THREE,
    };
    const haloOptions = {
        mouseEase: false,
        mouseControls: false,
        touchControls: false,
        gyroControls: false,
    minHeight: 200.0,
    minWidth: 200.0,
    THREE: THREE,
    };
    const rippleOptions = {
        color1: 0xeeaaff,
        color2: 0xcceeff,
        backgroundColor: 0x000010,
        amplitudeFactor: 1.0,
        ringFactor: 4.0,
        rotationFactor: 0.1,
        speed: 1.0,
        scaleMobile: 4,
    THREE: THREE,
    };

    const waveOptions = {
        mouseControls: false,
        touchControls: false,
        gyroControls: false,
    minHeight: 200.0,
    minWidth: 200.0,
    scale: 1.0,
    scaleMobile: 1.0,
    color: 0x888888,
        shininess: 30,
        waveHeight: 15,
    waveSpeed: 0.4,
    THREE: THREE,
    };
    const [effectLoaded, setEffectLoaded] = useState(false);
    useEffect(() => {
        if (vantaEffect && effectLoaded) {
            vantaEffect.destroy();
            switch (theme) {
                case 'clouds':
                    setVantaEffect(CLOUDS2({ ...cloudOptions, el: mainBg.current }));
                    break;
                case 'cells':
                    setVantaEffect(CELLS({ ...cellsOptions, el: mainBg.current }));
                    break;
                case 'halo':
                    setVantaEffect(HALO({ ...haloOptions, el: mainBg.current }));
                    break;
                case 'ripple':
                    setVantaEffect(RIPPLE({ ...haloOptions, el: mainBg.current }));
                    break;
                case 'waves':
                    setVantaEffect(WAVES({ ...waveOptions, el: mainBg.current }));
                    break;

        default:
          break;
            }
        }
        return () => {
            if (vantaEffect) vantaEffect.destroy();
        };
    }, [theme]);

    useEffect(() => {
        if (!vantaEffect) {
            switch (theme) {
                case 'clouds':
                    setVantaEffect(CLOUDS2({ ...cloudOptions, el: mainBg.current }));
                    break;
                case 'cells':
                    setVantaEffect(CELLS({ ...cellsOptions, el: mainBg.current }));
                    break;
                case 'halo':
                    setVantaEffect(HALO({ ...haloOptions, el: mainBg.current }));
                    break;
                case 'ripple':
                    setVantaEffect(RIPPLE({ ...rippleOptions, el: mainBg.current }));
                    break;
                case 'waves':
                    setVantaEffect(WAVES({ ...waveOptions, el: mainBg.current }));
                    break;
        default:
          break;
            }
            setEffectLoaded(true);
        }
        return () => {
            if (vantaEffect) vantaEffect.destroy();
        };
    }, [vantaEffect]);
    //return null;
    return (
        <Fragment>
      <div
        className={`pn-main-background__container pn-main-background__${theme}`}
        ref={mainBg}
      >
                <div className="pn-main-background__overlay"></div>
                <div className="pn-main-background__canvas"></div>
            </div>
        </Fragment>
    );
};

export default memo(Background);
