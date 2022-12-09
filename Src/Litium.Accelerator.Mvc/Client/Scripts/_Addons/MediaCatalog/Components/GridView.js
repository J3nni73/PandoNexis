import React, { Fragment } from 'react';
import FadeIn from 'react-fade-in';
import { translate } from '../../../Services/translation';

const GridView = ({
    currentFolder,
    allowedImageExtensions,
    formatFileSize
}) => {
    if (!currentFolder || !currentFolder.files || currentFolder.files.length < 1)
        return null;
    return (
        <div className={`row media-catalog__files small-up-2 medium-up-4 large-up-6 align-center align-middle`}>
            {!currentFolder.files || currentFolder.files.length < 1 &&
                <span className="media-catalog__no-files">{translate('addons.mediacatalog.nofilesfound')}</span>
            }
            {currentFolder && currentFolder.files && currentFolder.files.length > 0 && currentFolder.files.map((file, indexFile) => {
                return (
                    <FadeIn className={`column`} key={`media-catalog__folder-Grid${indexFile}`}>
                        <div className="media-catalog__files-card">
                            <figure>
                                <div className="pn-card">
                                    <a href={file.downloadUrl}>
                                        {file.extension &&
                                            <Fragment>
                                                {file.extension &&
                                                    <div className="media-catalog__files-tag-extension"> {file.extension}</div>
                                                }
                                                {file && file.extension && allowedImageExtensions.findIndex(x => x === file.extension.toLowerCase()) !== -1 ?
                                                    (<img src={file.largeThumbnailUrl} alt={file.name} loading="lazy" />) :
                                                    (<div className={`media-catalog__file media-catalog__file--${file.extension.toLowerCase()}`}></div>)
                                                }
                                            </Fragment>
                                        }</a>
                                </div>
                                <figcaption>
                                    <h4>{translate('addons.mediacatalog.info')}</h4>
                                    <ul className="media-catalog__file-data">
                                        <li>{translate('addons.mediacatalog.fileinfo.name')}: <span>{file.name}</span></li>
                                        <li>{translate('addons.mediacatalog.fileinfo.width')}: <span>{file.frameWidth}px</span></li>
                                        <li>{translate('addons.mediacatalog.fileinfo.height')}: <span>{file.frameHeight}px</span></li>
                                        <li>{translate('addons.mediacatalog.fileinfo.filesize')}: <span>{formatFileSize(file.fileSize)}</span></li>
                                        <li>{translate('addons.mediacatalog.fileinfo.fileextension')}: <span>{file.extension}</span></li>
                                    </ul>
                                </figcaption>
                            </figure>
                        </div>
                    </FadeIn>
                )
            }
            )}
        </div>
    )
};

export default GridView;

