import { IAddAuctionItemFormValues } from './IAddAuctionItemFormValues';

export interface IAddAuctionItemFormProps {
  onSubmit: (values: IAddAuctionItemFormValues) => Promise<void>;
}
