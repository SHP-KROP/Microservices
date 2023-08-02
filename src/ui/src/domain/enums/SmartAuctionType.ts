import { AuctionType } from './AuctionType';

const auctionTypes: Record<string, AuctionType> = {
  English: AuctionType.English,
  Holland: AuctionType.Holland,
  'English closed': AuctionType.EnglishClosed,
};

export default auctionTypes;
