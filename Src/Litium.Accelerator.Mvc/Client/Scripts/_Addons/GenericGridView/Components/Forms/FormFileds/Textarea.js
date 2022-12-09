import React from 'react';
import TextError from './TextError';

function Textarea(props) {
  const { label, name, register, type } = props;
  return (
    <div className="row">
      <div className="small-12 columns">
        <label htmlFor={name}>{label}</label>
        <textarea id={name} name={name} type={type} {...register(name)} />
        {/* <ErrorMessage component={TextError} name={name} /> */}
      </div>
    </div>
  );
}

export default Textarea;
