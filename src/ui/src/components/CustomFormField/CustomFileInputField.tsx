import React from 'react';

function CustomFileInputField<T>({ field, formik }: ICustomTextFieldProps<T>) {
  const { id, name, label } = field;

  const error =
    formik.touched[name as keyof T] && formik.errors[name as keyof T];
  const helperText = error ? String(error) : '';

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.currentTarget.files?.[0] || null;
    formik.setFieldValue(name as keyof T, file);
  };

  return (
    <div className="flex justify-center items-center w-full">
      <div>
        {label && (
          <label htmlFor={id} className="mr-2">
            {label}
          </label>
        )}
        {helperText && <p className="text-red-600 ml-2">{helperText}</p>}
      </div>
      <input
        id={id}
        name={name as string}
        type="file"
        onChange={handleFileChange}
        onBlur={formik.handleBlur}
        className="w-56"
      />
    </div>
  );
}

export default CustomFileInputField;
