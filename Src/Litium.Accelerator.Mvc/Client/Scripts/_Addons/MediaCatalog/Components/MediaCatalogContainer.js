import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getMediaCatalogData } from '../Actions/MediaCatalog.action';
import Fuse from 'fuse.js'

import { translate } from '../../../Services/translation';

import Breadcrumbs from './Breadcrumbs';
import GridView from './GridView';
import ListView from './ListView';
import Search from './Search';
import FolderIcon from '../Icons/folder_fill.svg?component';
import FolderOpenIcon from '../Icons/folder_open_fill.svg?component';

const MediaCatalogContainer = ({
    mediaFolderId,
    alternativeFolderName,
    useAltMediaView
}) => {
    const dispatch = useDispatch();
    const [currentFolder, setCurrentFolder] = useState({});
    const [firstFolderName, setFirstFolderName] = useState(alternativeFolderName);
    const [useAlternativeFolderView, setUseAlternativeFolderView] = useState(useAltMediaView);
    const [viewType, setViewType] = useState('grid');
    const [currentStructure, setCurrentStructure] = useState([]);
    const [fuse, setFuse] = useState(null);

    const { fileStructure } = useSelector(
        (state) => state.mediaCatalog
    );
    const updateStructure = (folderName) => {
        const obj = { folderName };
        setCurrentStructure([...currentStructure, obj]);
    };
    const allowedImageExtensions = ["jpg", "jpeg", "png", "gif", "webp", "svg"];
    const breadcrumbClick = (folder) => {
        var folderIndexTo = currentStructure.findIndex(x => x.folderName == folder.folderName);
        if (folderIndexTo === null || folderIndexTo === undefined || folderIndexTo < 0) {
            return;
        }
        if (folderIndexTo === 0) {
            setCurrentFolder(fileStructure);
        }
        else {
            setCurrentFolder(fileStructure.folderData[folderIndexTo - 1]);
        }

        // Set breadcrumbs
        setCurrentStructure(currentStructure.slice(0, folderIndexTo + 1));
    };

    const clearStructure = () => {
        const obj = { folderName: firstFolderName || fileStructure.folderName };
        setCurrentStructure([obj]);
    };

    const formatFileSize = (fileSize) => {
        if (!fileSize)
            return '';
        const type = Math.floor((fileSize.toString().length - 1) / 3);
        switch (type) {
            case 0:
            case 1:
                return formatNumber(fileSize / 1000) + "KB";
                break
            case 2:
                return formatNumber(fileSize / 1000000) + "MB";
                break
            case 3:
                return formatNumber(fileSize / 1000000000) + "TB";
                break;
            default: break
        }

        return '';
    }
    const formatNumber = (numb) => {
        return numb.toFixed(2).replace('.00', '');
    };

    const filterItems = (searchQuery) => {
        clearStructure();
        if (!searchQuery || searchQuery.trim().length === '') {
            setCurrentFolder(fileStructure);
            return;
        }
        if (fuse) {
            const q = searchQuery.trim();
            const result = fuse.search(q);
            if (result) {
                const files = result.map(e => e.item);
                const newStructure = {
                    folderName: firstFolderName || fileStructure.folderName,
                    files
                }
                setCurrentFolder(newStructure);
            }
        }
    };

    useEffect(() => {
        if (mediaFolderId) {
            dispatch(getMediaCatalogData(mediaFolderId));
        }
    }, []);

    const constructFlatFileArray = (obj) => {
        let files = [];
        if (obj) {
            if (obj.files) {
                obj.files.map((file, index) => {
                    files.push({ ...file, parentName: obj.parentName });
                });
            }

            if (obj.folderData) {
                obj.folderData.map((folder, index) => {
                    files = files.concat(constructFlatFileArray(folder));
                });
            }
        }
        return files;
    };

    useEffect(() => {
        if (fileStructure) {
            setCurrentFolder(fileStructure);

            if (fileStructure.folderName) {
                // Set Breadcrumbs - start-folder
                clearStructure();
            }

            // Is this needed?
            const filesArray = constructFlatFileArray(fileStructure);

            // Fuzzy search
            setFuse(new Fuse(filesArray, {
                keys: [
                    'name',
                    'extension',
                ],
                threshold: 0.3,
                distance: 10,
            }));
        }
    }, [fileStructure]);
    return (
        <div className="media-catalog">
            <div className="row text--center media-catalog__header">
                <section className="small-12 columns"><Search filterItems={filterItems} /></section>
                <section className="small-12 columns"><Breadcrumbs currentStructure={currentStructure} breadcrumbClick={breadcrumbClick} /></section>
            </div>
            {currentFolder.folderData &&
                <div className="media-catalog__folder-data-container">
                    <div className={`row media-catalog__folder-data align-center ${useAlternativeFolderView ? 'small-up-2 medium-up-4 large-up-6' : ''}`}>
                        {
                            currentFolder.folderData && currentFolder.folderData.length > 0 && currentFolder.folderData.map((folder, index) => {
                                if (!useAlternativeFolderView)
                                    return (
                                        <div className="column small-6 medium-1" key={`media-folder__folder${index}`} onClick={() => { setCurrentFolder(folder), updateStructure(folder.folderName) }}>
                                            <div className="media-catalog__icon-folder">
                                                <FolderIcon width="48" height="48" className="media-catalog__icon-folder-item media-catalog__icon-folder-item--closed" alt={translate('addons.mediacatalog.button.folder')} />
                                                <FolderOpenIcon width="48" height="48" className="media-catalog__icon-folder-item media-catalog__icon-folder-item--open" alt={translate('addons.mediacatalog.button.folderopen')} />
                                             </div>
                                            <div className="media-catalog__icon-folder-text">{folder.folderName}</div>
                                        </div>
                                    );
                                return (
                                    <div className="column" key={`media-folder__folder${index}`}>
                                        <div>
                                            <div className={`pn-card`} onClick={() => { setCurrentFolder(folder), updateStructure(folder.folderName) }}>
                                                <div className="media-catalog__folder-data-item">
                                                    {folder.folderName}
                                                </div>
                                                <div className="media-catalog__folder-text">Folder</div>
                                            </div>
                                        </div>
                                    </div>
                                );
                            })
                        }
                    </div>
                </div>
            }
            {fileStructure.files &&
                <section className={`${viewType}-view`}>
                    <div className="row">
                        <h2 className="column text--center media-catalog__folder-heading">
                            <div className="media-catalog__view-toggler"><span className={`media-catalog__view-toggler-grid ${viewType === 'grid' ? 'media-catalog__view-toggler-grid--active' : ''}`} onClick={() => setViewType('grid')}></span><span className={`media-catalog__view-toggler-list ${viewType === 'list' ? 'media-catalog__view-toggler-list--active' : ''}`} onClick={() => setViewType('list')}></span></div>
                            {
                                currentStructure && currentStructure.length > 1 &&
                                <span className="media-catalog__folder-back-link" onClick={() => breadcrumbClick(currentStructure[currentStructure.length - 2])}>{translate('addons.mediacatalog.button.stepoutoffolder')}</span>
                            }
                            Files in folder: <span>{currentStructure.length === 1 ? (firstFolderName || fileStructure.folderName) : currentFolder.folderName}</span></h2></div>

                    {viewType === 'list' ?
                        (<ListView currentFolder={currentFolder} allowedImageExtensions={allowedImageExtensions} formatFileSize={formatFileSize} />)
                        :
                        (<GridView currentFolder={currentFolder} allowedImageExtensions={allowedImageExtensions} formatFileSize={formatFileSize} />)
                    }
                </section>
            }
        </div>
    );
};
export default MediaCatalogContainer;
