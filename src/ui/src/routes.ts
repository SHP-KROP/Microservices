import { IRoute } from "./interfaces/IRoute";
import Home from "./pages/Home/Home";

export const routes: Array<IRoute> = [
    {
        key: 'home-route',
        title: 'Home',
        path: '/',
        enabled: true,
        component: Home
    },
  
]