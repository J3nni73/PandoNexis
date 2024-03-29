import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import DynamicComponent from './Components/DynamicComponent';
import 'react-tooltip/dist/react-tooltip.css';

//PandoNexis: BEGIN IMPORT
import PNGenericDataViewContainer from './_Addons/PNGenericDataView/Components/PNGenericDataView.container';
//PandoNexis: END IMPORT

import './pandoNexis.events.js';
import './pandoNexis.functions.js';

const renderReact = (element, container, callback) => {
    window.__pn.registeredContainers.push(container);
    ReactDOM.render(element, container, callback);
};

window.__pn = {
    ...window.__pn,
    initNewBuyButton: (button) => {
        initNewBuyButton(button);
    },
    cache: {}, // for storing cache data for current request
};

const initNewBuyButton = (button) => {
    const BuyButton = DynamicComponent({
        loader: () => import('./Components/BuyButton'),
    });
    const {
        articleNumber,
        quantityFieldId,
        href,
        cssClass,
        label,
    } = button.dataset;
    renderReact(
        <Provider store={window.__pn.store}>
            <BuyButton
                {...{
                    label,
                    articleNumber,
                    quantityFieldId,
                    href,
                    cssClass,
                }}
            />
        </Provider>,
        button
    );
}

export const bootstrapPNComponents = () => {
    // PRODUCT PAGE IMAGES
    const lightBoxImagesEl = document.getElementById('lightBoxImages');
    if (lightBoxImagesEl) {
        const LightboxImages = DynamicComponent({
            loader: () => import('./_Solution/Components/LightboxImages'),
        });
        import('./Reducers/LightboxImages.reducer').then(
            ({ lightboxImages }) => {
                window.__pn.store.injectReducer('lightboxImages', lightboxImages);
                const rootElement = lightBoxImagesEl;
                const images = Array.from(
                    rootElement.querySelectorAll('template')
                ).map((img) => ({
                    html: img.innerHTML,
                    src: img.dataset.src,
                }));
                renderReact(
                    <Provider store={window.__pn.store}>
                        <LightboxImages images={images} />
                    </Provider>,
                    lightBoxImagesEl
                );
            }
        );
    }
    // END PRODUCT PAGE IMAGES

    // ICONS
    const iconEls = document.querySelectorAll('.pn-icon');
    if (iconEls) {
        const PnIcon = DynamicComponent({
            loader: () => import('./_PandoNexis/Components/PnIcon'),
        });
        iconEls.forEach((elem) => {
            const { iconName, title, width, height } = elem.dataset;
            ReactDOM.render(
                <Provider store={window.__pn.store}>
                    <PnIcon iconName={iconName} title={title} width={width} height={height} />
                </Provider>,
                elem
            );
        });
    }

    // ADDONS

//PandoNexis: BEGIN  COMPONENT
    // CollectionPage
    const collectionPage = document.getElementById("collectionPage");
    if (collectionPage) {
        const CollectionPage = DynamicComponent({
            loader: () => import('./_Addons/PNCollectionPage/Components/CollectionPageContainer'),
        });

        const { collectionPageSystemId, link, linkText } = collectionPage.dataset;
        renderReact(
            <Provider store={window.__pn.store}>
                <CollectionPage {...{ collectionPageSystemId, link, linkText }} />
            </Provider>,
            collectionPage
        );
    }
    // END CollectionPage
    // PNGenericDataView
    const pnGenericDataView = document.querySelectorAll(".generic-data-view__container");

    if (pnGenericDataView) {

        pnGenericDataView.forEach((elem, index) => {

            const { appendToQuerySelector, page, viewTypes, dataType } = elem.dataset;
            renderReact(
                <Provider store={window.__pn.store}>
                    <PNGenericDataViewContainer pageSystemId={page} {...{ viewTypes, dataType }} />
                </Provider>,
                elem
            );
        });

        document.querySelectorAll('[data-dropzone]').forEach((elem) => {
            ReactDOM.render(
                <Provider store={window.__pn.store}>
                    <DropZoneContainer type={elem.dataset.dropzone} />
                </Provider>,
                elem
            );
        });

        const reorderBtn = document.querySelectorAll('reorder-button');
        if (reorderBtn.length > 0) {
            const ReorderButton = DynamicComponent({
                loader: () => import('./Components/ReorderButton'),
            });
            Array.from(reorderBtn).forEach(
                (button) => {
                    const { cssClass, orderId, title } = button.dataset;
                    const label = button.innerText;
                    renderReact(
                        <Provider store={window.__pn.store}>
                            <ReorderButton {...{ label, title, cssClass, orderId }} />
                        </Provider>,
                        button
                    );
                }
            );
        }
    } 
    // END PNGenericDataView
    // Logged on info
    const loggedOnInfoLabels = document.querySelectorAll('.pn-info-label');
    if (loggedOnInfoLabels) {
        const LoggedOnInfoLabel = DynamicComponent({
            loader: () => import('./_Addons/PNLoggedOnInfoLabel/Components/LoggedOnInfoLabelContainer'),
        });

        loggedOnInfoLabels.forEach((elem, index) => {
            const { heading } = elem.dataset;
            renderReact(
                <Provider store={window.__pn.store}>
                    <LoggedOnInfoLabel getData={index === 0} heading={heading || ''} />
                </Provider>,
                elem
            );
        });
    }
    // END Logged on info
    // OrganizationSelector
    const organizationSelectors = document.querySelectorAll(".pn-organization-selector");
    if (organizationSelectors) {

        const OrganizationSelector = DynamicComponent({
            loader: () => import('./_Addons/PNOrganizationSelector/Components/OrganizationSelectorContainer'),
        });
        Array.from(organizationSelectors).forEach(
            (organizationSelector) => {
                renderReact(
                    <Provider store={window.__pn.store}>
                        <OrganizationSelector />
                    </Provider>,
                    organizationSelector
                );
            });
    }
    // END OrganizationSelector
 
    // 3D Background
    const threeDBgs = document.querySelectorAll('.pn-main-background'); //getElementById("pnMainBackground");
    if (threeDBgs) {
        const ThreeDBg = DynamicComponent({
            loader: () => import('./_Addons/PNThreeDeeBg/Background'),
        });

        Array.from(threeDBgs).forEach(
            (threeDBg) => {
                const { theme } = threeDBg.dataset;
                renderReact(
                    <Provider store={window.__pn.store}>
                        <ThreeDBg theme={theme} />
                    </Provider>,
                    threeDBg
                );
            });
        
        //renderReact(
        //    <Provider store={window.__pn.store}>
        //        <ThreeDBg theme={theme} />
        //    </Provider>,
        //    threeDBg
        //);
    }
    // END 3D Background

//PandoNexis: END COMPONENT
}
