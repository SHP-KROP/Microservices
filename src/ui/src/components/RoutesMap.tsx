import React from 'react';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Routes, Route } from 'react-router-dom';
import routes from '../routes';

export default function RoutesMap() {
  return (
    <>
      <ToastContainer />
      <Routes>
        {routes.map(({ key, path, component: Component }) => (
          <Route key={key} path={path} element={<Component />} />
        ))}
      </Routes>
    </>
  );
}
