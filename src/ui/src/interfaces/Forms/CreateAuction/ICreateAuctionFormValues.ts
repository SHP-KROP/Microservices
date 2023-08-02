import { Dayjs } from 'dayjs';
import { AuctionType } from '../../../domain/enums/AuctionType';

export interface ICreateAuctionFormValues {
  name: string;
  description: string;
  startTime: Dayjs;
  auctionType: AuctionType;
}
