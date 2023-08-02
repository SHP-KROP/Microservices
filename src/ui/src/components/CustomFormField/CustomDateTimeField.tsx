import {
  DateTimePicker,
  LocalizationProvider,
  renderTimeViewClock,
} from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import React from 'react';
import { ICustomTextFieldProps } from '../../interfaces/Forms/ICustomTextFieldProps';

function CustomDateTimeField<T>({ field, formik }: ICustomTextFieldProps<T>) {
  const { name, label } = field;

  const handleChange = (value: any) => {
    const changeEvent = {
      target: {
        name: name as string,
        value: value,
      },
    };

    formik.handleChange(changeEvent);
  };

  return (
    <div className="w-9/12">
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DateTimePicker
          viewRenderers={{
            hours: renderTimeViewClock,
            minutes: renderTimeViewClock,
            seconds: renderTimeViewClock,
          }}
          label={label}
          onChange={handleChange}
          value={formik.values[name as keyof T]}
          className="w-full"
          disablePast
        />
      </LocalizationProvider>
    </div>
  );
}

export default CustomDateTimeField;
