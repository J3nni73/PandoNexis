import React from 'react';
import CheckboxGroup from './FormFileds/CheckboxGroup';
import DatePickerFiled from './FormFileds/DatePickerFiled';
import Input from './FormFileds/Input';
import RadioButtons from './FormFileds/RadioButtons';
import Search from './FormFileds/Search';
import Select from './FormFileds/Select';
import Textarea from './FormFileds/Textarea';
import UploadFile from './FormFileds/UploadFile';

import Hidden from './FormFileds/Hidden';

// SOLUTION
import QuotaFormContainer from '../../_Solution/Components/Quota/QuotaFormContainer';

const FormControl = (props) => {
  const { controlForm, ...rest } = props;
  switch (controlForm) {
    case 'decimal':
    case 'string':
      return <Input {...rest} />;
    case 'search':
      return <Search {...rest} />;
    case 'dropdown':
      return <Select {...rest} />;
    case 'textarea':
      return <Textarea {...rest} />;
    case 'radio':
      return <RadioButtons {...rest} />;
    case 'checkbox':
      return <CheckboxGroup {...rest} />;
    case 'date':
      return <DatePickerFiled {...rest} />;
    case 'file':
      return <UploadFile {...rest} />;
    case 'hidden':
          return <Hidden {...rest} />;

    // SOLUTION FORMS
    case 'quotaformfield':
      return <QuotaFormContainer {...rest} />;

    default:
      return null;
  }
};

export default FormControl;
