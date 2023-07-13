import React, { useState } from 'react';
import RegistrationForm from '../../components/forms/RegistrationForm';
import LoginForm from '../../components/forms/LoginForm';

function AuthPage() {
  const [showLoginForm, setShowLoginForm] = useState<boolean>(false);

  const toggleForm = () => {
    setShowLoginForm(!showLoginForm);
  };

  const handleClickRegistration = () => {};
  const handleClickLogin = () => {};
  const handleGoogleSignIn = () => {};

  const renderRegisterForm = () => (
    <RegistrationForm
      onSubmit={handleClickRegistration}
      toggleForm={toggleForm}
      handleGoogleSignIn={handleGoogleSignIn}
    />
  );

  const renderLoginForm = () => (
    <LoginForm
      onSubmit={handleClickLogin}
      toggleForm={toggleForm}
      handleGoogleSignIn={handleGoogleSignIn}
    />
  );

  return (
    <div className="h-screen w-full bg-welcome bg-no-repeat bg-cover bg-center flex justify-center items-center">
      {showLoginForm ? renderLoginForm() : renderRegisterForm()}
    </div>
  );
}

export default AuthPage;
