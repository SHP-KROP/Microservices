import React, { useState } from 'react';
import RegistrationForm from '../../components/forms/RegistrationForm';
import LoginForm from '../../components/forms/LoginForm';
import { IRegisterFormValues } from '../../interfaces/Forms/IRegisterFormValues';
import { ILoginFormValues } from '../../interfaces/Forms/ILoginFormValues';

function AuthPage() {
  const [showLoginForm, setShowLoginForm] = useState<boolean>(false);

  const toggleForm = () => {
    setShowLoginForm((prev) => !prev);
  };

  const handleClickRegistration = (values: IRegisterFormValues) => {
    console.log('Registration Form Values:', values);
  };

  const handleClickLogin = (values: ILoginFormValues) => {
    console.log('Login Form Values:', values);
  };

  const handleGoogleSignIn = () => {
    // Handle Google sign-in logic
  };

  return (
    <div className="h-screen w-full bg-welcome bg-no-repeat bg-cover bg-center flex justify-center items-center">
      {showLoginForm ? (
        <LoginForm
          onSubmit={handleClickLogin}
          toggleForm={toggleForm}
          handleGoogleSignIn={handleGoogleSignIn}
        />
      ) : (
        <RegistrationForm
          onSubmit={handleClickRegistration}
          toggleForm={toggleForm}
          handleGoogleSignIn={handleGoogleSignIn}
        />
      )}
    </div>
  );
}

export default AuthPage;
