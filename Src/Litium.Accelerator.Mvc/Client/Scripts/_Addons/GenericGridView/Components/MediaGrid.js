import React, { useCallback, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getMediaFolderData } from '../Actions/GenericGridView.action';
const MediaGrid = ({
    mediaFolderId
}) => {
    const dispatch = useDispatch();

    useEffect(() => {
        if (mediaFolderId) {
            dispatch(getMediaFolderData(mediaFolderId));
        }
        
    }, []);

    return (
        <div>ID={mediaFolderId}</div>
    );
};
export default MediaGrid;
