import React from 'react';

function Hidden(props) {
  const { name, register, type } = props;
  const required = type == 'strint' ? required : '';

  return (    
          <input
            id={name}
            type="hidden"
            name={name}
            {...register(name)}
          />
  );
}

export default Hidden;

