import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { translate } from '../../../../../Services/translation';
import { setFieldData } from '../../../Actions/GenericGridSpecialField.action';
class ButtonField extends Component {
  constructor(props) {
    super(props);
    this.state = { ...props };
    this.clickHandler = this.clickHandler.bind(this);
    this.lastClickedNode = null;
    this.state.selectedValueObject = null;
    this.state.updating = false;
    this.state.toFewChars = true;
  }

  componentDidMount() {
    document.addEventListener('mousedown', this.clickHandler);
  }

  componentWillUnmount() {
    document.removeEventListener('mousedown', this.clickHandler);
  }

  clickHandler(event) {
    this.lastClickedNode = event.target;
  }

  postValue() {
    const currentProps = this.props;
    const useConfirmation = currentProps.fieldSettings.useConfirmation;
    const confirmationText = currentProps.fieldSettings.confirmationText;

    if (useConfirmation) {
      if (!confirm(confirmationText)) {
        return false;
      }
    }
    const entitySystemId = currentProps.entitySystemId;
    const rowIndex = currentProps.rowIndex;
    const selectedValueObject = {
      value: '',
      name: '',
      entitySystemId,
      rowIndex,
    };

    const fieldId = currentProps.fieldId;
    const { setFieldData } = currentProps;
    if (setFieldData) {
      setFieldData(fieldId, rowIndex, selectedValueObject);
    }
  }

  componentWillUpdate({ showFullForm } = props) {}

  render() {
    const {
      fieldId,
      entitySystemId,
      defaultValue,
      fieldSettings,
      rowIndex,
      isSearching,
    } = this.props;
    return (
      <div
        className="generic-grid-view__button-field-container"
        ref={(elem) => (this.buttonFieldContainer = elem)}
      >
        {defaultValue && defaultValue.length > 0 && (
          <div className="generic-grid-view__button-field-description">
            {defaultValue || fieldSettings.placeholderText}
          </div>
        )}
        <div className="generic-grid-view__button-field-button">
          {!fieldSettings.hideButton && (
            <Fragment>
              {fieldSettings.iconClass ? (
                <i
                  className={fieldSettings.iconClass}
                  onClick={(event) => this.postValue()}
                  title={fieldSettings.buttonText}
                ></i>
              ) : (
                <button
                  className="generic-grid-view__button-field-post-button"
                  onClick={(event) => this.postValue()}
                >
                  {fieldSettings.buttonText}
                </button>
              )}
            </Fragment>
          )}
        </div>
      </div>
    );
  }
}

const mapStateToProps = ({ genericGridView }) => genericGridView;

const mapDispatchToProps = (dispatch) => {
  return {
    setFieldData: (fieldId, rowIndex, setValueObject) =>
      dispatch(setFieldData(fieldId, rowIndex, setValueObject)),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(ButtonField);
