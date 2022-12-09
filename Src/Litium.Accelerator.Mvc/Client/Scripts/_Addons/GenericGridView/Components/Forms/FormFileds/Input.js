import React from 'react';
// import { Field, ErrorMessage } from 'formik'
import TextError from './TextError';

function Input(props) {
  const { label, name, register, type, settings, style } = props;
  const required = type == 'strint' ? required : '';

  getSettingsKeys(settings);
  return (
    // <div className='form-control'>
    //   <label htmlFor={name}>{label}</label>
    //   <input id={name} type={type} name={name} {...register(name)}  />
    //   {/* <ErrorMessage component={TextError} name={name} /> */}
    // </div>
    <div className="row">
      <div className="small-12 columns">
        <label>
          {label}
          <input
            placeholder={name}
            id={name}
            type={type === 'decimal' ? 'number' : 'text'}
            name={name}
            {...register(name)}
            required={required}
            style={style}
          />
          <span className="form-error">
            {/* <ErrorMessage component={TextError} name={name} /> */}
          </span>
        </label>
        {settings?.errorFieldMessage && (
          <span
            className="generic-grid-view__error-field-message"
            dangerouslySetInnerHTML={{
              __html: settings.errorFieldMessage,
            }}
          ></span>
        )}
      </div>
    </div>
  );
}

export default Input;

const getSettingsKeys = (data) => {
  if (data === null || data == undefined) return '';
  Object.keys(data).filter((v) => {
    if (data[v] === true) {
      console.log(v);
    }
  });
};
