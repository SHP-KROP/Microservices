import { ICreateAuctionFormValues } from './ICreateAuctionFormValues';

export interface ICreateAuctionFormProps {
  onSubmit: (values: ICreateAuctionFormValues) => Promise<void>;
}
