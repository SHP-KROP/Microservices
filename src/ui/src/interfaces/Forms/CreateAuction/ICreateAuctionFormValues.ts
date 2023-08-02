import { Dayjs } from 'dayjs';

export interface ICreateAuctionFormValues {
  name: string;
  description: string;
  startTime: Dayjs;
  auctionType: string;
}
