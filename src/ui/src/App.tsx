import { Routes, Route } from 'react-router-dom';
import routes from './routes';

export function App() {
  return (
    <Routes>
      {routes.map(({ key, path, component: Component }) => (
        <Route key={key} path={path} element={<Component />} />
      ))}
    </Routes>
  );
}

export default App;
