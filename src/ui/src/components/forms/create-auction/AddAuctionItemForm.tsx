import React from 'react';
import { useFormik } from 'formik';
import { IAddAuctionItemFormValues } from '../../../interfaces/Forms/AddAuctionItem/IAddAuctionItemFormValues';
import { IAddAuctionItemFormProps } from '../../../interfaces/Forms/AddAuctionItem/IAddAuctionItemFormProps';
import fieldAddAuctionItemConfig from './fieldAddAuctionItemConfig';
import CustomFormField from '../../CustomFormField/CustomFormField';

function AddAuctionItemForm({ onSubmit }: IAddAuctionItemFormProps) {
  const formik = useFormik<IAddAuctionItemFormValues>({
    initialValues: {
      name: '',
      description: '',
      startingPrice: 0,
      minimalBid: 0,
      photos: null,
    },
    //validate: validateCreateAuction,
    onSubmit: onSubmit,
  });

  return (
    <form
      onSubmit={formik.handleSubmit}
      className="border rounded-lg flex flex-col items-center justify-center bg-white h-4/6 w-2/4 gap-4 max-w-xl"
    >
      <h1 className="text-black text-center text-lg not-italic font-semibold uppercase">
        Add auction item
      </h1>
      {fieldAddAuctionItemConfig.map((field) => (
        <CustomFormField field={field} formik={formik} key={field.id} />
      ))}

      <button
        type="submit"
        className={
          'w-9/12 h-10 rounded text-white uppercase transition-transform'
        }
        style={{ background: 'rgba(5, 81, 81, 0.80)' }}
      >
        Add item
      </button>
    </form>
  );
}

export default AddAuctionItemForm;
