import React, { Fragment } from 'react';
import FadeIn from 'react-fade-in';
import { translate } from '../../../Services/translation';

const ListView = ({
    currentFolder,
    allowedImageExtensions,
    formatFileSize
}) => {
    if (!currentFolder || !currentFolder.files || currentFolder.files.length < 1)
        return (<span className="media-catalog__no-files">{translate('addons.mediacatalog.nofilesfound')}</span>);
    return (
        <Fragment>
            <div className="row media-catalog__table-header">
                <div className="small-12 medium-2 columns">&nbsp;</div>
                <div className="small-12 medium-4 columns">Description</div>
                <div className="small-12 medium-2 columns">File size</div>
                <div className="small-12 medium-2 columns">File extension</div>
                <div className="small-12 medium-2 columns">&nbsp;</div>
            </div>
            <FadeIn className="media-catalog__table-data">
                {currentFolder && currentFolder.files && currentFolder.files.length > 0 && currentFolder.files.map((file, indexFile) => {
                    return (
                        <div className={`row media-catalog__table-row`} key={`media-catalog__folder${indexFile}`}>
                            <div className="small-12 medium-2 columns media-catalog__table-image">
                                <figure>
                                    {file && file.extension && allowedImageExtensions.findIndex(x => x === file.extension.toLowerCase()) !== -1 ?
                                        (<img src={file.mediumThumbnailUrl} alt={file.name} loading="lazy" />) :
                                        (<div className={`media-catalog__file media-catalog__file--small media-catalog__file--${file.extension.toLowerCase()}`}></div>)
                                    }
                                </figure>
                            </div>
                            <div className="small-12 medium-4 columns media-catalog__table-decription">
                                <ul className="media-catalog__file-data">
                                    <li>{translate('addons.mediacatalog.fileinfo.name')}: <span>{file.name}</span></li>
                                    <li>{translate('addons.mediacatalog.fileinfo.width')}: <span>{file.frameWidth}px</span></li>
                                    <li>{translate('addons.mediacatalog.fileinfo.height')}: <span>{file.frameHeight}px</span></li>
                                </ul>
                            </div>
                            <div className="small-12 medium-2 columns media-catalog__table-file-type">{formatFileSize(file.fileSize)}</div>
                            <div className="small-12 medium-2 columns media-catalog__table-file-size">{file.extension.toLowerCase()}</div>
                            <div className="small-12 medium-2 columns media-catalog__table-button text--right"><a href={file.downloadUrl}>{translate('addons.mediacatalog.button.download')}</a></div>
                        </div>
                    )
                }
                )}
            </FadeIn>
        </Fragment>
    )
};

export default ListView;

