import { IRoute } from './interfaces/IRoute';
import Home from './pages/Home/Home';
import Welcome from './pages/Welcome/Welcome';

const routes: IRoute[] = [
  {
    key: 'home-route',
    title: 'Home',
    path: '/home',
    enabled: true,
    component: Home,
  },
  {
    key: 'welcome-route',
    title: 'Welcome',
    path: '/',
    enabled: true,
    component: Welcome,
  },
];

export default routes;
