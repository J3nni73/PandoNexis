import React, { useEffect, useCallback, Fragment } from 'react';
import ImageUploading from 'react-images-uploading';

import Modal from 'react-modal';
import { translate } from '../../../Services/translation';
//import { setIsInEditMode } from '../Actions/QuotaQuickSearch.action';
import { removeImageFromVariant, addImagesToArchiveAndVariant, showEditImages, changeMode, catshImagesWillBeAdded } from '../Actions/ProductImageUpload.action';
import { useDispatch, useSelector } from 'react-redux';

function ImageModalComponent(props) {
    const { dropDownOptions, entitySystemId, innerRef, common, onChange, fieldId, rowIndex, isEditable } = props
    const mode = useSelector((state) => state.genericGridView.showModal)
    const imagesWillBeAdded = useSelector((state) => state.genericGridView.imagesWillBeAdded)
    const [modalIsOpen, setIsOpen] = React.useState(false);

    const dispatch = useDispatch()

    const [images, setImages] = React.useState([]);
    // const [images, setImages] = React.useState({ imageList: [], addUpdateIndex: [] });
    const maxNumber = 20;

    const onChangeLocal = (imageList, addUpdateIndex) => {
        setImages(imageList);
    };

    const catchImageIfReRender = (imageList) => {
        if (imageList) {
            dispatch(catshImagesWillBeAdded(imageList))
        }
    }

    useEffect(() => {
        if (imagesWillBeAdded.length > 0) {
            setImages(imagesWillBeAdded);
        }
    }, [imagesWillBeAdded])

    useEffect(() => {
        // console.log(' Row index ', rowIndex);
        if (mode?.index == rowIndex) {
            setIsOpen(mode?.open)
        } else {
            setIsOpen(false)
        }
    }, [])


    function openModal(index) {
        // setIsOpen(true);
        //dispatch(setIsInEditMode(false));
        dispatch(showEditImages(true, index));
    }

    function afterOpenModal() {
        // references are now sync'd and can be accessed.
        // subtitle.style.color = "#f00";
        // setIsOpen(mode == 'edit');
    }
    // console.log('mode', mode);

    function closeModal() {
        // setIsOpen(false);
        dispatch(showEditImages(false, mode?.index));
        dispatch(catshImagesWillBeAdded([]))
        // console.log(modalIsOpen);
    }

    const removeExistingImageFromVariant = (e, imageId) => {
        e.preventDefault()
        dispatch(removeImageFromVariant(entitySystemId, imageId, rowIndex, fieldId));
    }
    const removeNewAddedImage = (e, index) => {
        e.preventDefault();
        catchImageIfReRender(images.filter((x, i) => i !== index))
        setImages(prevState => prevState.filter((x, i) => i != index))
    }

    const addImageToVariant = (e, index) => {
        e.preventDefault()
        catchImageIfReRender(images.filter((x, i) => i !== index))
        const getImageFromState = images.filter((x, i) => i == index);
        dispatch(addImagesToArchiveAndVariant(entitySystemId, getImageFromState, rowIndex, fieldId));

    }

    const addAllImages = () => {
        dispatch(catshImagesWillBeAdded([]))
        dispatch(addImagesToArchiveAndVariant(entitySystemId, images, rowIndex, fieldId));
    }

    return (
        <div className='modal__image__view'>
            <button className="modal__image__btnOpen" onClick={() => openModal(rowIndex)}>
                View/Update
            </button>
            <Modal
                ariaHideApp={false}
                isOpen={modalIsOpen}
                onAfterOpen={afterOpenModal}
                onRequestClose={closeModal}
                className="modal"
            >
                <div className='modal__overlay'></div>
                <div className="modal-content" >
                    <div className="closeBtn">
                        <button title="Close" type="button" data-dismiss="modal" aria-label="Close" onClick={closeModal} >
                            <span>&times;</span>
                        </button>
                    </div>
                    <div className="modal-content__addAllImages">
                        <button type="button" className={`button button-orange modal-content__addAllImages__add-all-btn ${images.length > 1 ? 'active' : ''}`} onClick={addAllImages} >
                            <span>Add All</span>
                        </button>
                    </div>
                    <div className="modal-content-images__container">
                        <div className="img-grid">
                            {dropDownOptions && dropDownOptions.length > 0 && dropDownOptions.map((item) => {
                                return (
                                    <div className='img-grid-item' key={item.text}>
                                        <div className="content-overlay"></div>
                                        <div className="img-grid-item-crud">
                                            <button title="Remove" className="img-grid-item-crud__remove" onClick={(e) => removeExistingImageFromVariant(e, item.text)} ><span>&times;</span></button>
                                        </div>
                                        <img
                                            name="ProductImageUpload"
                                            className="modal-content-img"
                                            src={item?.value}
                                            alt={item?.text.split('-id-')[0]}
                                        />
                                    </div>
                                )
                            })}
                            {images && images.map((image, index) => (

                                <div key={index} className="img-grid-item">
                                    <div className="img-grid-item-crud">
                                        <button title="Remove" className="img-grid-item-crud__remove" onClick={(e) => removeNewAddedImage(e, index)} ><span>&times;</span></button>
                                    </div>
                                    <img style={{ border: '2px solid #2c2e77' }} src={image['data_url']} alt="" width="100" />
                                    <button className="img-grid-item-crud__add" onClick={(e) => addImageToVariant(e, index)} >{translate('image.modal.addsingleimage')}</button>
                                </div>
                            )
                            )}
                        </div>
                    </div>
                    {isEditable &&
                        <div className="row">
                            <div className="small-12 columns">
                                <hr />
                                {/*<label htmlFor='imag'>Add New Image</label>
                            <input id='imag' type='file' name='imag' />*/}

                                <ImageUploading
                                    multiple
                                    value={images}
                                    onChange={onChangeLocal}
                                    maxNumber={maxNumber}
                                    dataURLKey="data_url"
                                // imgExtension={['.jpg', '.gif', '.png', '.gif']}
                                >
                                    {({
                                        imageList,
                                        onImageUpload,
                                        onImageRemoveAll,
                                        onImageUpdate,
                                        onImageRemove,
                                        isDragging,
                                        dragProps,
                                    }) => (
                                        // write your building UI
                                        <Fragment>
                                            <div className="modal-content-images__upload-image-wrapper">
                                                <button
                                                    style={isDragging ? { color: 'red' } : undefined}
                                                    onClick={onImageUpload}
                                                    {...dragProps}
                                                >
                                                    Click or Drop here
                                                </button>
                                            </div>
                                            {/* <button onClick={onImageRemoveAll}>Remove all images</button>*/}

                                        </Fragment>
                                    )}
                                </ImageUploading>
                                {/* <ErrorMessage component={TextError} name={name} /> */}
                            </div>
                        </div>
                    }
                </div>
            </Modal>
        </div>
    );
}

export default ImageModalComponent;
