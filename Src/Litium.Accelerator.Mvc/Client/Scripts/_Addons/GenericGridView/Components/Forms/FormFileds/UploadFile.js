import React from 'react'
// import { Field, ErrorMessage } from 'formik'
import TextError from './TextError'

function UploadFile (props) {
  const { label, name, register, type } = props
  return (
    <div className="row">
    <div className="small-12 columns">
      <label htmlFor={name}>{label}</label>
      <input id={name} type={type} name={name} {...register(name)}  />
      {/* <ErrorMessage component={TextError} name={name} /> */}
    </div>
    </div>
  )
}

export default UploadFile