import React, { useState } from 'react';
import { useFormik } from 'formik';
import { TextField } from '@material-ui/core';
import { ILoginFormProps } from '../../interfaces/Forms/ILoginFormProps';
import { ILoginFormValues } from '../../interfaces/Forms/ILoginFormValues';
import validateLoginForm from '../../Validation/validateAuthForms/validationLoginForm';
import GoogleIcon from '../../images/Google.svg';

function LoginForm({
  onSubmit,
  toggleForm,
  handleGoogleSignIn,
}: ILoginFormProps) {
  const [isClicked, setIsClicked] = useState(false);
  const handleButtonClick = () => {
    setIsClicked(true);
    setTimeout(() => {
      setIsClicked(false);
    }, 200);
  };
  const formik = useFormik<ILoginFormValues>({
    initialValues: {
      email: '',
      password: '',
    },
    validate: validateLoginForm,
    onSubmit: (values) => {
      onSubmit(values);
    },
  });
  const emailError = formik.touched.email && formik.errors.email;
  const passwordError = formik.touched.password && formik.errors.password;
  return (
    <form
      onSubmit={formik.handleSubmit}
      className="border rounded-lg flex flex-col items-center justify-center bg-white h-4/6 w-2/4 gap-4 max-w-xl"
    >
      <h1 className="text-black text-center text-lg not-italic font-semibold uppercase">
        Login
      </h1>
      <div className="w-9/12">
        <TextField
          id="email"
          name="email"
          label="Email"
          type="email"
          variant="outlined"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.email}
          error={!!emailError}
          helperText={emailError}
          className="w-full"
        />
      </div>

      <div className="w-9/12">
        <TextField
          id="password"
          name="password"
          label="Password"
          type="password"
          variant="outlined"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.password}
          error={!!passwordError}
          helperText={passwordError}
          className="w-full"
        />
      </div>
      <button
        type="submit"
        className={`w-9/12 h-10 rounded text-white uppercase transition-transform ${
          isClicked ? 'transform scale-105' : ''
        }`}
        style={{ background: 'rgba(5, 81, 81, 0.80)' }}
        onClick={handleButtonClick}
      >
        Sign in
      </button>
      <p className="text-blue-500">
        <button
          type="button"
          className="link-button underline"
          onClick={toggleForm}
        >
          Don&apos;t have an account? Register
        </button>
      </p>

      <button
        type="button"
        onClick={handleGoogleSignIn}
        className="border border-emerald-600 rounded text-black text-center text-xs not-italic font-normal capitalize w-8/12 h-10 flex items-center justify-center gap-1"
      >
        <img src={GoogleIcon} alt="Google Icon" className="w-6" />
        Continue with Google
      </button>
    </form>
  );
}

export default LoginForm;
