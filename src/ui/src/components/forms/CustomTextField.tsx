import React from 'react';
import { TextField } from '@material-ui/core';
import { ICustomTextFieldProps } from '../../interfaces/Forms/ICustomTextFieldProps';
import { MenuItem } from '@mui/material';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { renderTimeViewClock } from '@mui/x-date-pickers/timeViewRenderers';

function CustomTextField<T>({ field, formik }: ICustomTextFieldProps<T>) {
  const { id, name, label, type, options } = field;
  const error =
    formik.touched[name as keyof T] && formik.errors[name as keyof T];
  const helperText = error ? String(error) : '';

  // TODO: Refactor and create custom field per each type
  switch (type) {
    case 'select':
      return (
        <div className="w-9/12">
          <TextField
            id={id}
            name={name as string}
            label={label}
            type={type}
            select
            variant="outlined"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values[name as keyof T]}
            error={Boolean(error)}
            helperText={helperText}
            className="w-full"
          >
            {Object.keys(options)?.map((x) => (
              <MenuItem key={options[x]} value={options[x]}>
                {x}
              </MenuItem>
            ))}
          </TextField>
        </div>
      );
    case 'date':
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
              onChange={formik.handleChange}
              value={formik.values[name as keyof T]}
              className="w-full"
            />
          </LocalizationProvider>
        </div>
      );
    default:
      return (
        <div className="w-9/12">
          <TextField
            id={id}
            name={name as string}
            label={label}
            type={type}
            variant="outlined"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values[name as keyof T]}
            error={Boolean(error)}
            helperText={helperText}
            className="w-full"
          />
        </div>
      );
  }
}

export default CustomTextField;
