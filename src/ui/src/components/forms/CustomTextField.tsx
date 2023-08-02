import React from 'react';
import { TextField } from '@material-ui/core';
import { ICustomTextFieldProps } from '../../interfaces/Forms/ICustomTextFieldProps';
import { MenuItem } from '@mui/material';

function CustomTextField<T>({ field, formik }: ICustomTextFieldProps<T>) {
  const { id, name, label, type, options } = field;
  const error =
    formik.touched[name as keyof T] && formik.errors[name as keyof T];
  const helperText = error ? String(error) : '';

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
            {options.map((x) => (
              <MenuItem key={x} value={x}>
                {x}
              </MenuItem>
            ))}
          </TextField>
        </div>
      );
    case 'date':
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
