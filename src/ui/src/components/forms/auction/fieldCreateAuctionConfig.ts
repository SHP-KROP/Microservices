import { IFieldConfig } from './../../../interfaces/Forms/IFieldConfig';
import { ICreateAuctionFormValues } from './../../../interfaces/Forms/CreateAuction/ICreateAuctionFormValues';
import auctionTypes from '../../../domain/enums/SmartAuctionType';

const fieldCreateAuctionConfig: IFieldConfig<ICreateAuctionFormValues>[] = [
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
    id: 'startTime',
    name: 'startTime',
    label: 'Start time',
    type: 'date',
  },
  {
    id: 'auctionType',
    name: 'auctionType',
    label: 'Auction type',
    type: 'select',
    options: auctionTypes,
  },
];

export default fieldCreateAuctionConfig;
