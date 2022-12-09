import React, { Component } from 'react';
import { connect } from 'react-redux';
import { translate } from '../../../Services/translation';
import {
  load,
  addField,
  removeField,
} from '../Actions/FieldConfigurator.action';

class FieldConfiguratorContainer extends Component {
  constructor(props) {
    super(props);
    this.state = { ...props };
  }

  componentDidMount() {
    const { load, type } = this.props;
    load(type);
    document.addEventListener('mousedown', this.clickHandler);
  }

  componentWillUnmount() {
    document.removeEventListener('mousedown', this.clickHandler);
  }

  componentWillUpdate({ showFullForm } = props) {}

  clickHandler(event) {
    this.lastClickedNode = event.target;
  }

  addSelectedField(event) {
    if (event.target.selectedIndex > 0) {
      const { addField, type } = this.props;
      var item = this.props.fields[event.target.selectedIndex - 1];
      if (this.props.selectedFields.find((i) => i.id == item.id)) {
        alert(translate('common.fieldconfiguration.exists'));
      } else {
        if (addField) {
          addField(item, type);
        }
      }
    }
  }

  removeSelectedField(event) {
    const sourceIndex = event.target.dataset.sourceindex;
    const { removeField, type } = this.props;
    if (removeField) {
      removeField(sourceIndex, type);
    }
  }

  toggle() {
    this.setState({ ...this.state, showInfo: !this.state.showInfo });
  }

  render() {
    const { fields, type, selectedFields } = this.props;
    return (
      <section className="field-configuration">
        <span
          className="field-configuration__title"
          onClick={() => this.toggle()}
        >
          {translate('common.fieldconfiguration.title.' + type.toLowerCase())}
        </span>
        <span
          className={`arrow-down ${this.state.showInfo ? 'arrow-up' : ''}`}
          onClick={() => this.toggle()}
        ></span>
        <div
          className={`field-configuration ${
            !this.state.showInfo ? 'field-configuration--hidden' : ''
          }`}
        >
          <select
            name="fieldConfiguration"
            onChange={(event) => this.addSelectedField(event)}
          >
            <option value="">
              {translate('common.fieldconfiguration.selectfield')}
            </option>
            {fields.length > 0 &&
              fields.map((fields, fieldNameLeftIndex) => {
                return (
                  <option
                    key={'fieldName' + fieldNameLeftIndex}
                    data-id={fields.id}
                    data-isbaseproduct={fields.isBaseProduct}
                    value={fields.name}
                    className={!fields.isBaseProduct ? 'is-variant' : ''}
                  >
                    {fields.name} (
                    {fields.isBaseProduct
                      ? translate(
                          'common.fieldconfiguration.initials.baseproduct'
                        )
                      : translate('common.fieldconfiguration.initials.variant')}
                    )
                  </option>
                );
              })}
          </select>
          <h5>{translate('common.fieldconfiguration.selectedfields')}</h5>
          <div className="row">
            {selectedFields && (
              <ol className="fieldConfiguration__selectedFields">
                {selectedFields.map((selectedFields, selectedFieldsIndex) => {
                  return (
                    <li key={selectedFields.id}>
                      <span
                        dangerouslySetInnerHTML={{
                          __html: selectedFields.name,
                        }}
                      ></span>
                      <i
                        className="fas fa-times-circle"
                        data-sourceindex={selectedFieldsIndex}
                        onClick={(event) => this.removeSelectedField(event)}
                      ></i>
                    </li>
                  );
                })}
              </ol>
            )}
          </div>
        </div>
      </section>
    );
  }
}

const mapStateToProps = (state) => {
  const { fieldConfiguration } = state;
  return {
    ...fieldConfiguration,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    load: (type) => dispatch(load(type)),
    addField: (field, type) => dispatch(addField(field, type)),
    removeField: (index, type) => dispatch(removeField(index, type)),
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(FieldConfiguratorContainer);
