import React, { useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Cookies from 'js-cookie';

export default function Home() {
  useEffect(() => {
    const isAuthenticated = Cookies.get('isAuthenticated') === 'true';
    if (isAuthenticated) {
      toast.success('Login successful. Welcome!');
    }
  }, []);

  return (
    <div className="div">
      <h1 className="uppercase text-emerald-600">Home</h1>
      <ToastContainer />
    </div>
  );
}
