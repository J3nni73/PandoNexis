import React from 'react';
import TextError from './TextError';

function CheckboxGroup(props) {
  const { label, name, register, options } = props;
  return (
    <div className="row">
      <div className="columns small-12">
        <label>{label}</label>
        <div className="row">
          {options.map((option) => {
            return (
              <div key={option.text} className="columns small-6">
                <input
                  type="checkbox"
                  id={option.value}
                  value={option.value}
                  {...register(name)}
                />
                <label htmlFor={option.value}>{option.text}</label>
              </div>
            );
          })}
        </div>
        {/* <ErrorMessage component={TextError} name={name} /> */}
      </div>
    </div>
  );
}

export default CheckboxGroup;
