import { File } from 'buffer';

export interface IAddAuctionItemFormValues {
  name: string;
  description: string;
  startingPrice: number;
  minimalBid: number;
  photos: File | null;
}
