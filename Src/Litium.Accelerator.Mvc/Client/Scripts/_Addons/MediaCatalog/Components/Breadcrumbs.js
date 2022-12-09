import React from 'react';

const Breadcrumbs = ({
    currentStructure,
    breadcrumbClick
}) => {
    if (!currentStructure)
        return null;

    return (
        <div className="media-catalog__breadcrumb">
            {currentStructure && currentStructure.length > 0 && currentStructure.map((folder, breadcrumbIndex) => {
                return (
                    <span key={`media-folder__breadcrumb${breadcrumbIndex}`} onClick={() => breadcrumbClick(folder)}>{folder.folderName}</span>
                )
            })}
        </div>
    )
};

export default Breadcrumbs;