import React from 'react';
import EnrollmentForm from './Forms/EnrollmentForm';
import Modal from 'react-modal';
import { translate } from '../../../Services/translation';
//import { setIsInEditMode } from '../Actions/QuotaQuickSearch.action';
import { useDispatch } from 'react-redux';
import { loadingToggleActionForm } from '../Actions/GenericGridForm.action';


function ModalComponent({ gridType, handlecategoryById }) {
  const [modalIsOpen, setIsOpen] = React.useState(false);
  const dispatch = useDispatch()
  function openModal() {
    setIsOpen(true);
    dispatch(loadingToggleActionForm(true));
   // dispatch(setIsInEditMode(false));
  }

  function afterOpenModal() {
    // references are now sync'd and can be accessed.
    // subtitle.style.color = "#f00";
  }

  function afterCloseModal() {
    // references are now sync'd and can be accessed.
    // subtitle.style.color = "#f00";
  }

  function closeModal() {
    setIsOpen(false);
    // console.log(modalIsOpen);
  }

  return (
      <div className=" text--center">
      <button className="form__button " onClick={openModal}>
        {translate('addons.genericgridview.form.button.open.' + gridType.toLowerCase())}
      </button>
      <Modal ariaHideApp={false} isOpen={modalIsOpen} onAfterClose={afterCloseModal} onAfterOpen={afterOpenModal} onRequestClose={closeModal} className={`modal ${gridType}`}  >
        <div className="modal-content">
          <div className="closeBtn">
            <button type="button" data-dismiss="modal" aria-label="Close" onClick={closeModal} >
              <span>&times;</span>
            </button>
          </div>
          <EnrollmentForm
            gridType={gridType}
            hide={closeModal}
            handlecategoryById={handlecategoryById}
          />
        </div>
      </Modal>
    </div>
  );
}

export default ModalComponent;
