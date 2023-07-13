import { FormikProps } from 'formik';
import { IRegisterFormValues } from './IRegisterFormValues';

export interface ICustomTextFieldProps {
  field: {
    id: string;
    name: string;
    label: string;
    type: string;
  };
  formik: FormikProps<IRegisterFormValues>;
}
