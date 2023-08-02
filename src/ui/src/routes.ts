import AuctionMessaging from './api/ws/auctionClient';
import { IRoute } from './interfaces/Routes/IRoute';
import AuthPage from './pages/AuthPage/AuthPage';
import Home from './pages/Home/Home';
import Welcome from './pages/Welcome/Welcome';
import CreateAuction from './pages/CreateAuction/CreateAuction';
import AddAuctionItemPage from './pages/AddAuctionItem/AddAuctionItemPage';

const routes: IRoute[] = [
  {
    key: 'home',
    title: 'Home',
    path: '/home',
    enabled: true,
    component: Home,
  },
  {
    key: 'welcome',
    title: 'Welcome',
    path: '/',
    enabled: true,
    component: Welcome,
  },
  {
    key: 'auth',
    title: 'AuthPage',
    path: '/auth',
    enabled: true,
    component: AuthPage,
  },
  {
    key: 'create-auction',
    title: 'CreateAuction',
    path: '/auctions/new',
    enabled: true,
    component: CreateAuction,
  },
  {
    key: 'add-auction-item',
    title: 'AddAuctionItem',
    path: '/auctions/:auctionId/new-item',
    enabled: true,
    component: AddAuctionItemPage,
  },
  {
    key: 'auction-messaging',
    title: 'AuctionMessaging',
    path: '/auction-messaging',
    enabled: true,
    component: AuctionMessaging,
  },
];

export default routes;
