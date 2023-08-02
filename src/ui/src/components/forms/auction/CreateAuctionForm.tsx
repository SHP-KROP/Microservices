import React from 'react';
import { useFormik } from 'formik';
import { ICreateAuctionFormValues } from '../../../interfaces/Forms/CreateAuction/ICreateAuctionFormValues';
import { ICreateAuctionFormProps } from '../../../interfaces/Forms/CreateAuction/ICreateAuctionFormProps';
import CustomTextField from './../CustomTextField';
import fieldCreateAuctionConfig from './../auction/fieldCreateAuctionConfig';
import { AuctionType } from '../../../domain/enums/AuctionType';

function CreateAuctionForm({ onSubmit }: ICreateAuctionFormProps) {
  const formik = useFormik<ICreateAuctionFormValues>({
    initialValues: {
      name: '',
      description: '',
      startTime: new Date(),
      auctionType: AuctionType[AuctionType.English].toString(),
    },
    //validate: validateLoginForm,
    onSubmit: (values) => {
      onSubmit(values);
    },
  });

  return (
    <form
      onSubmit={formik.handleSubmit}
      className="border rounded-lg flex flex-col items-center justify-center bg-white h-4/6 w-2/4 gap-4 max-w-xl"
    >
      <h1 className="text-black text-center text-lg not-italic font-semibold uppercase">
        Create auction
      </h1>
      {fieldCreateAuctionConfig.map((field) => (
        <CustomTextField field={field} formik={formik} key={field.id} />
      ))}

      <button
        type="submit"
        className={
          'w-9/12 h-10 rounded text-white uppercase transition-transform'
        }
        style={{ background: 'rgba(5, 81, 81, 0.80)' }}
        onClick={() => {}}
      >
        Create auction
      </button>
    </form>
  );
}

export default CreateAuctionForm;
