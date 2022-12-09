import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

// ADDONS IMPORT
import GenericGridViewContainer from './_Addons/GenericGridView/Containers/GenericGridView.container';
import FieldConfiguratorContainer from './_Addons/GenericGridView/Containers/FieldConfigurator.container';
import DropZoneContainer from './_Addons/GenericGridView/Containers/DropZone.container';
import HeaderInformationDataContainer from './_Addons/GenericGridView/Containers/HeaderInformationData.container';
// END ADDONS IMPORT

import './pandoNexis.events.js';
import './pandoNexis.functions.js';

export const pnBootstrapComponents = () => {

    // ADDONS
    // MediaCatalog
    const mediaCatalog = document.getElementById("media-catalog");
    if (mediaCatalog) {
        const MediaCatalog = DynamicComponent({
            loader: () => import('./_Addons/MediaCatalog/Components/MediaCatalogContainer'),
        });
        const { mediaFolderId, mediaFolderAlternativeName, mediaFolderAlternativeView } = mediaCatalog.dataset;
        const useAltMediaView = mediaFolderAlternativeView?.toLowerCase() === 'true';
        renderReact(
            <Provider store={store}>
                <MediaCatalog mediaFolderId={mediaFolderId} alternativeFolderName={mediaFolderAlternativeName} useAltMediaView={useAltMediaView} />
            </Provider>,
            mediaCatalogs
        );
    }
    // END MediaCatalog
    // CollectionPage
    const collectionPage = document.getElementById("collectionPage");
    if (collectionPage) {
        const CollectionPage = DynamicComponent({
            loader: () => import('./_Addons/CollectionPage/Components/CollectionPageContainer'),
        });

        const { collectionPageSystemId } = collectionPage.dataset;
        renderReact(
            <Provider store={store}>
                <CollectionPage {...{ collectionPageSystemId }} />
            </Provider>,
            collectionPage
        );
    }
    // END CollectionPage

    // GenericGridView
    document.querySelectorAll('[data-field-configuration]').forEach((elem) => {
        ReactDOM.render(
            <Provider store={store}>
                <FieldConfiguratorContainer type={elem.dataset.fieldConfiguration} />
            </Provider>,
            elem
        );
    });

    document.querySelectorAll('[data-grid-view]').forEach((elem) => {
        console.log(elem.dataset.gridView);
        ReactDOM.render(
            <Provider store={store}>
                <GenericGridViewContainer type={elem.dataset.gridView} />
            </Provider>,
            elem
        );
    });

    document.querySelectorAll('[data-dropzone]').forEach((elem) => {
        ReactDOM.render(
            <Provider store={store}>
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
                    <Provider store={store}>
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
            <Provider store={store}>
                <HeaderInformationDataContainer />
            </Provider>,
            headerInformationData
        );
    }
    // END GenericGridView

    // END ADDONS
};
