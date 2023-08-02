import { AuctionType } from '../../domain/enums/AuctionType';

export interface IFieldConfig<T> {
  id: string;
  name: keyof T | string;
  label: string;
  type: string;
  options?: Record<string, AuctionType>;
}
