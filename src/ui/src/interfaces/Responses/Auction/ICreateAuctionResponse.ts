import { Dayjs } from 'dayjs';

export interface ICreateAuctionResponse {
  id: string;
  userId: string;
  name: string;
  description: string;
  startTime: Dayjs;
  endTime: Dayjs;
  auctionType: string;
  photoUrl: string;
}
