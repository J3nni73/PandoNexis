import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { translate } from '../../../../../Services/translation';
class DropdownField extends Component {
  constructor(props) {
    super(props);
    this.state = { ...props };
  }
  render() {
    const { common, dropDownOptions, isEditable } = this.props;
    return (
      <div className="generic-grid-view__dropdown-container">
        {dropDownOptions && dropDownOptions.length > 0 && isEditable && (
          <select
            className={`generic-grid-view__dropdown-list `}
            value={this.state.selectValue}
            readOnly={!isEditable}
            {...common}
          >
            <option value="-1">
              {translate(
                'addons.genericgridview.field.dropdown.chooseoptiontext'
              )}
            </option>
            {dropDownOptions &&
              dropDownOptions.map((item, dropdownIndex, array) => (
                <Fragment key={`dropdownListIndex-${dropdownIndex}`}>
                  <option value={item.value} className="">
                    {item.text}
                  </option>
                </Fragment>
              ))}
          </select>
        )}
      </div>
    );
  }
}

export default connect()(DropdownField);
