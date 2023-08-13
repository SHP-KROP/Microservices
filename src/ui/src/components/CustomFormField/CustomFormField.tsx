import React from 'react';
import { ICustomTextFieldProps } from '../../interfaces/Forms/ICustomTextFieldProps';
import CustomSelectField from './CustomSelectField';
import CustomDateTimeField from './CustomDateTimeField';
import CustomTextField from './CustomTextField';
import CustomFileInputField from './CustomFileInputField';
import CustomNumberField from './CustomNumberField';

function CustomFormField<T>({ field, formik }: ICustomTextFieldProps<T>) {
  const { type } = field;

  switch (type) {
    case 'select':
      return <CustomSelectField field={field} formik={formik} />;
    case 'date':
      return <CustomDateTimeField field={field} formik={formik} />;
    case 'text':
      return <CustomTextField field={field} formik={formik} />;
    case 'file':
      return <CustomFileInputField field={field} formik={formik} />;
    case 'number':
      return <CustomNumberField field={field} formik={formik} />;
    default:
      return <div>Wrong field type: {type}</div>;
  }
}

export default CustomFormField;
