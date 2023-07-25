import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useMutation } from 'react-query';
import { toast, ToastContainer } from 'react-toastify';
import RegistrationForm from '../../components/forms/RegistrationForm';
import LoginForm from '../../components/forms/LoginForm';
import { IRegisterFormValues } from '../../interfaces/Forms/IRegisterFormValues';
import { ILoginFormValues } from '../../interfaces/Forms/ILoginFormValues';
import { login, register } from '../../api/api';
import setAuthenticationCookies from './Logic/setAuthenticationCookies';

function AuthPage() {
  const [showLoginForm, setShowLoginForm] = useState<boolean>(false);
  const navigate = useNavigate();
  const toggleForm = () => {
    setShowLoginForm((prev) => !prev);
  };
  const loginMutation = useMutation(login);
  const registrationMutation = useMutation(register);

  const handleLogin = async (values: ILoginFormValues) => {
    try {
      const { token } = await loginMutation.mutateAsync(values);

      setAuthenticationCookies(token);

      navigate('/home');
    } catch (error) {
      toast.error('Login failed. Please check your credentials and try again.');
    }
  };

  const handleRegistration = async (values: IRegisterFormValues) => {
    try {
      await registrationMutation.mutateAsync(values);
      await handleLogin(values);
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (error: any) {
      toast.error(
        error.response?.data ?? 'Registration failed. Please try again.'
      );
    }
  };

  const handleGoogleSignIn = () => {
    // Handle Google sign-in logic
  };

  return (
    <div className="h-screen w-full bg-welcome bg-no-repeat bg-cover bg-center flex justify-center items-center">
      <ToastContainer />
      {showLoginForm ? (
        <LoginForm
          onSubmit={handleLogin}
          toggleForm={toggleForm}
          handleGoogleSignIn={handleGoogleSignIn}
        />
      ) : (
        <RegistrationForm
          onSubmit={handleRegistration}
          toggleForm={toggleForm}
          handleGoogleSignIn={handleGoogleSignIn}
        />
      )}
    </div>
  );
}

export default AuthPage;
