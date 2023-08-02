import dayjs from 'dayjs';
import auctionTypes from '../../domain/enums/SmartAuctionType';
import { ICreateAuctionFormValues } from '../../interfaces/Forms/CreateAuction/ICreateAuctionFormValues';

function validateCreateAuction(values: ICreateAuctionFormValues): any {
  const errors: any = {};

  if (!Object.values(auctionTypes).includes(values?.auctionType)) {
    errors.auctionType = `Unavailable auction type: available types are: ${Object.keys(
      auctionTypes
    ).join(', ')}\n`;
  }

  if (values?.description?.length >= 200) {
    errors.description += 'Description maximum length is 200 characters\n';
  }

  if (values?.name?.length < 3 || values?.name?.length > 100) {
    errors.name += 'Name length should be between 3 and 100 characters\n';
  }

  if (!values?.name) {
    errors.name += 'Name is required\n';
  }

  if (!values?.startTime) {
    errors.startTime += 'Start time is required\n';
  } else if (dayjs().diff(values.startTime) > 0) {
    errors.startTime += 'Start time cannot be in the past';
  }

  return errors;
}

export default validateCreateAuction;
