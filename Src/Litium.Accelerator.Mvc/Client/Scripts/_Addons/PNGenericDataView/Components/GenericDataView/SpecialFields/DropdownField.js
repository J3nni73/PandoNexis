import React, { Fragment, useState } from 'react';
import { connect } from 'react-redux';
import { translate } from '../../../../../Services/translation';
import { Tooltip as ReactTooltip } from 'react-tooltip';

export const DropdownField = (
  {
    common,
    fieldSettings,
    dropDownOptions,
    isEditable,
    defaultValue,
    onFocus,
    fieldId,
    autoFocus,
    ...props
  },
  ref
) => {
  const [selectedValue, setSelectedValue] = useState(defaultValue);
  //const values = Array.from(options).map(({ value }) => value);
  //var selected = [...select.options]
  //    .filter(option => option.selected)
  //    .map(option => option.value);

  const onChanging = (e) => {
    const tempVal = e.target.value;
    window.currGenDW_lastClickedFieldId = fieldId;
    window.currGenDW_lastClickedFieldValue = tempVal;
    setSelectedValue(tempVal);
    if (props.onChange) {
      props.onChange(e);
    }
  };

  return (
    <div className="generic-data-view__dropdown-container">
      {dropDownOptions && dropDownOptions.length > 0 && (
        <select
          className={`generic-data-view__dropdown-list `}
          readOnly={!isEditable}
          onFocus={onFocus}
          {...autoFocus}
          multiple={fieldSettings.multiple}
          {...common}
          onChange={(e) => onChanging(e)}
        >
          <option value="-1">
            {translate(
              'addons.genericdataview.field.dropdown.chooseoptiontext'
            )}
          </option>
          {dropDownOptions &&
            dropDownOptions.map((item, dropdownIndex, array) => (
              <Fragment key={`dropdownListIndex-${dropdownIndex}`}>
                <option value={item.key} className="">
                  {item.value}
                </option>
              </Fragment>
            ))}
        </select>
      )}
      {fieldSettings.fieldTooltipMessage &&
        fieldSettings.fieldTooltipMessage.length > 0 && (
          <ReactTooltip
            className="generic-data-view__tooltip"
            float={true}
            delayShow="800"
            delayHide="300"
            anchorId={`dropdown-${
              fieldSettings.entitySystemId || entitySystemId
            }${fieldSettings.fieldId || fieldId}${rndNo}${
              dataContainerIndex || 0
            }`}
            variant={fieldSettings.fieldTooltipType || 'dark'}
            positionStrategy="fixed"
            offset="32"
            place="left"
          >
            {fieldSettings.fieldTooltipMessage}
          </ReactTooltip>
        )}
    </div>
  );
};

const mapStateToProps = ({ genericDataView }) => genericDataView;
DropdownField.displayName = 'DropdownField';
export default connect(mapStateToProps)(DropdownField);
