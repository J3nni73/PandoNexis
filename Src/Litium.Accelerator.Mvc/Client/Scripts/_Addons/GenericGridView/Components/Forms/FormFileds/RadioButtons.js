import React from 'react';
import PropTypes from 'prop-types';
import TextError from './TextError';

function RadioButtons(props) {
  const { label, name, options, register } = props;
  return (
    <div className="row">
      <div className="small-12 columns">
        <label>{label} </label>
        <React.Fragment>
          {options.map((option) => {
            return (
              <React.Fragment key={option.key}>
                <input
                  type="radio"
                  id={option.value}
                  value={option.value}
                  {...register('radio')}
                />
                <label htmlFor={option.value}>{option.key}</label>
              </React.Fragment>
            );
          })}
        </React.Fragment>

        {/* <ErrorMessage component={TextError} name={name} /> */}
      </div>
    </div>
  );
}

RadioButtons.propTypes = {
  options: PropTypes.array,
  label: PropTypes.string,
  name: PropTypes.string,
  register: PropTypes.func,
};
export default RadioButtons;
