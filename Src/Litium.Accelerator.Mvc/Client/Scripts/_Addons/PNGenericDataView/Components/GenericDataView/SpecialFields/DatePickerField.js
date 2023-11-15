import React, { useState } from 'react';
import { connect } from 'react-redux';
import DatePicker, { DateObject } from 'react-multi-date-picker';
import Icon from 'react-multi-date-picker/components/icon';
// import 'react-datepicker/dist/react-datepicker.css'

export const DatePickerField = (
  {
    common,
    fieldSettings,
    isEditable,
    defaultValue,
    onFocus,
    fieldId,
    autofocus,
    ...props
  },
  ref
) => {
  const [dateValue, setDateValue] = useState(new DateObject(defaultValue));
  const handleChange = (value) => {
    setDateValue(value);
    if (value instanceof DateObject) {
      setDateValue(value);
      window.currGenDW_lastClickedFieldId = fieldId;
      window.currGenDW_lastClickedFieldValue = value.toDate();

      props.onEnterKeyPress(e);
    }
  };
  const onOpen = () => {
    var $body = document.querySelector('body');
    if ($body) {
      setTimeout(() => {
        $body.classList.add('no-backdrop');
      }, 400);
    }
  };
  const onClose = () => {
    var $body = document.querySelector('body');
    $body.classList.remove('no-backdrop');
  };
  return (
    <div className="generic-data-view__datepicker">
      <DatePicker
        value={dateValue}
        editable={true}
        inputMode="none"
        inputClass="generic-data-view__datepicker-input"
        onChange={handleChange}
        onOpen={(e) => onOpen(e)}
        onClose={(e) => onClose(e)}
        format={'YYYY-MM-DD'}
        // format="YYYY-MM-DD, h:mm a"
        // format={language === "en" ? "MM/DD/YYYY" : "YYYY/MM/DD"}
        markFocused={autofocus}
        {...common}
        style={{
          //input style
          width: '100%',
          height: '26px',
          boxSizing: 'border-box',
        }}
        mainPosition="left"
        relativePosition="end"
        fixMainPosition={false}
        fixRelativePosition={false}
      />
    </div>
  );
};

const mapStateToProps = ({ genericDataView }) => genericDataView;
DatePickerField.displayName = 'DatePickerField';
export default connect(mapStateToProps)(DatePickerField);
