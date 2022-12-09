import React, { useCallback, Fragment } from 'react'
import ImageModalComponent from '../../ImageModalComponent';

const ProductImageUpload = (props) => {
    const { entitySystemId, dropDownOptions, fieldId, rowIndex, isEditable } = props;

    const ProductImage = useCallback(({ imageData }) => {

        return (
            imageData && imageData.length > 0 ?
                <img className={`generic-grid-view__image ${dropDownOptions.length > 1 ? 'generic-grid-view__image--stack' : ''}`}
                    src={imageData[0].value}
                    alt={imageData[0].text}
                />
                :
                <img className="generic-grid-view__image"
                    src={'https://via.placeholder.com/600x400'}
                    alt={'Product has not Image'}
                />
        );
    }, [dropDownOptions]);
    return (
        <div className='generic-grid-view__image__container'>
            <div className="img__wrap__content">
                {/* <div className="content-overlay"></div> */}
                <ProductImage imageData={dropDownOptions} />
                <span className="generic-grid-view__image__amount-text" >
                    {dropDownOptions.length}
                    {dropDownOptions.length === 1 ? (
                        <Fragment> image</Fragment>
                    ) : (<Fragment> images</Fragment>)
                    }
                </span>
                <ImageModalComponent  {...props} />
            </div>
        </div>
    )
}

export default ProductImageUpload