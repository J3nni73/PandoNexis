import React from 'react';
import TextError from './TextError';

function Select(props) {
  const { label, name, register, options, type } = props;
  return (
    // <div className='form-control'>
    //   <label htmlFor={name}>{label}</label>
    //   <select id={name} name={name} type={type} {...register(name)}>
    //     {options.map(option => {
    //       return (
    //         <option key={option.value} value={option.value}>
    //           {option.key}
    //         </option>
    //       )
    //     })}
    //   </select>
    //   {/* <ErrorMessage component={TextError} name={name} /> */}
    // </div>
    <div className="row">
    <div className="small-12 columns">
      <label>
        {label}
        <select id={name} name={name} type={type} {...register(name)}  >
          <option value=""></option>
          {options.map((option) => {
            return (
              <option key={option.value} value={option.value}>
                {option.text}
              </option>
            );
          })}
        </select>
      </label>
      {/* <ErrorMessage component={TextError} name={name} /> */}
    </div>
    </div>
  );
}

export default Select;
