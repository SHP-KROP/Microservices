export interface IPhoto {
  name: string;
  url: string;
}

export interface IAddAuctionItemResponse {
  id: string;
  startingPrice: number;
  actualPrice: number;
  minimalBid: number;
  name: string;
  description: string;
  isSellingNow: boolean;
  isSold: boolean;
  photos: IPhoto[];
}
