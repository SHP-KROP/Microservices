import { IFieldConfig } from './../../../interfaces/Forms/IFieldConfig';
import { ICreateAuctionFormValues } from './../../../interfaces/Forms/CreateAuction/ICreateAuctionFormValues';
import auctionTypes from '../../../domain/enums/SmartAuctionType';

const fieldAddAuctionItemConfig: IFieldConfig<ICreateAuctionFormValues>[] = [
  {
    id: 'name',
    name: 'name',
    label: 'Name',
    type: 'text',
  },
  {
    id: 'description',
    name: 'description',
    label: 'Description',
    type: 'text',
  },
  {
    id: 'startingPrice',
    name: 'startingPrice',
    label: 'Starting price',
    type: 'number',
  },
  {
    id: 'minimalBid',
    name: 'minimalBid',
    label: 'Minimal bid',
    type: 'number',
  },
  {
    id: 'photos',
    name: 'photos',
    label: 'Photos',
    type: 'file',
  },
];

export default fieldAddAuctionItemConfig;
