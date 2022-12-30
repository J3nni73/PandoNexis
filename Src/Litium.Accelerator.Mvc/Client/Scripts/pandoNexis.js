import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import DynamicComponent from './Components/DynamicComponent';
import PnBackground from './_Addons/PnThreeDeeBg/Background';

// ADDONS IMPORT
import GenericGridViewContainer from './_Addons/GenericGridView/Containers/GenericGridView.container';
import FieldConfiguratorContainer from './_Addons/GenericGridView/Containers/FieldConfigurator.container';
import DropZoneContainer from './_Addons/GenericGridView/Containers/DropZone.container';
import HeaderInformationDataContainer from './_Addons/GenericGridView/Containers/HeaderInformationData.container';
// END ADDONS IMPORT

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
    // Logged on info
    //const loggedOnInfoLabels = document.querySelectorAll('.pn-info-label');
    //if (loggedOnInfoLabels) {
    //    const LoggedOnInfoLabel = DynamicComponent({
    //        loader: () => import('./_Addons/LoggedOnInfoLabel/Components/LoggedOnInfoLabelContainer'),
    //    });

    //    loggedOnInfoLabels.forEach((elem, index) => {
    //        const { heading } = elem.dataset;
    //        renderReact(
    //            <Provider store={window.__pn.store}>
    //                <LoggedOnInfoLabel getData={index === 0} heading={heading || ''} />
    //            </Provider>,
    //            elem
    //        );
    //    });
    //}
    // END Logged on info

    // MediaCatalog
    const mediaCatalog = document.getElementById("media-catalog");
    if (mediaCatalog) {
        const MediaCatalog = DynamicComponent({
            loader: () => import('./_Addons/MediaCatalog/Components/MediaCatalogContainer'),
        });
        const { mediaFolderId, mediaFolderAlternativeName, mediaFolderAlternativeView } = mediaCatalog.dataset;
        const useAltMediaView = mediaFolderAlternativeView?.toLowerCase() === 'true';
        renderReact(
            <Provider store={window.__pn.store}>
                <MediaCatalog mediaFolderId={mediaFolderId} alternativeFolderName={mediaFolderAlternativeName} useAltMediaView={useAltMediaView} />
            </Provider>,
            mediaCatalog
        );
    }
    // END MediaCatalog

    // OrganizationSelector
    const organizationSelectors = document.querySelectorAll(".pn-organization-selector");
    if (organizationSelectors) {

        const OrganizationSelector = DynamicComponent({
            loader: () => import('./_Addons/OrganizationSelector/Components/OrganizationSelectorContainer'),
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

    // Show Infinite scroll Button
    const infiniteScrollButton = document.getElementById("InfiniteScroll");
    if (infiniteScrollButton) {
        const InfiniteScrollButton = DynamicComponent({
            loader: () => import('./_Addons/InfiniteScrollButton/Components/InfiniteScrollButtonContainer'),
        });
        const { elementQs, totalPages, totalCount, currentPageIndex, pageSize, url, scrollType } = infiniteScrollButton.dataset;
        renderReact(
            <Provider store={window.__pn.store}>
                <InfiniteScrollButton {...{ elementQs, url, scrollType }} totalPages={parseInt(totalPages)} totalCount={parseInt(totalCount)} currentPageIndex={parseInt(currentPageIndex)} pageSize={parseInt(pageSize)} />
            </Provider>,
            infiniteScrollButton
        );
    }
    // END Show Infinite Scroll Button

    // CollectionPage
    const collectionPage = document.getElementById("collectionPage");
    if (collectionPage) {
        const CollectionPage = DynamicComponent({
            loader: () => import('./_Addons/CollectionPage/Components/CollectionPageContainer'),
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

    // GenericGridView
    document.querySelectorAll('[data-field-configuration]').forEach((elem) => {
        ReactDOM.render(
            <Provider store={window.__pn.store}>
                <FieldConfiguratorContainer type={elem.dataset.fieldConfiguration} />
            </Provider>,
            elem
        );
    });

    document.querySelectorAll('[data-grid-view]').forEach((elem) => {
        console.log(elem.dataset.gridView);
        ReactDOM.render(
            <Provider store={window.__pn.store}>
                <GenericGridViewContainer type={elem.dataset.gridView} />
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

    const headerInformationData = document.getElementById('headerInformationData');
    if (headerInformationData) {
        ReactDOM.render(
            <Provider store={window.__pn.store}>
                <HeaderInformationDataContainer />
            </Provider>,
            headerInformationData
        );
    }
    // END GenericGridView

    // END ADDONS

}