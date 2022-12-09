import React from 'react';
import DatePicker from 'react-multi-date-picker';
import TextError from './TextError';
// import 'react-datepicker/dist/react-datepicker.css'

function DatePickerFiled(props) {
  const { label, name, register, setValue } = props;
  return (
    <div className="row">
    <div className="small-12 columns">
        <label>
          {label}
          <DatePicker
            isClearable
            inputClass="custom-input"
            id={name} 
            {...register(name)}
            name={name}
            onChange={(val) => {
              setValue(name, val.format('YYYY-MM-DD'));
            }}
            format={'YYYY-MM-DD'}
            // format="YYYY-MM-DD, h:mm a"
            // format={language === "en" ? "MM/DD/YYYY" : "YYYY/MM/DD"}
          />
        </label>
        {/* <ErrorMessage component={TextError} name={name} /> */}
      </div>
    </div>
  );
}

export default DatePickerFiled;
