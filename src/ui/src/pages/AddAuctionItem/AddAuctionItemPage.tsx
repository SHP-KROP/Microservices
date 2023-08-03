import React from 'react';
import { useParams } from 'react-router-dom';
import AddAuctionItemForm from '../../components/forms/create-auction/AddAuctionItemForm';
import { ApiErrorHandler } from '../../api/rest/apiErrorHandler';
import { IAddAuctionItemFormValues } from '../../interfaces/Forms/AddAuctionItem/IAddAuctionItemFormValues';
import { useMutation } from 'react-query';
import apiClient, { IAddAuctionsRequestValues } from '../../api/rest/api';

function AddAuctionItemPage() {
  let { auctionId } = useParams();

  const addAuctionItemMutation = useMutation(apiClient.addAuctionItem);

  async function addAuctionItem(values: IAddAuctionItemFormValues) {
    try {
      const addAuctionRequestValues: IAddAuctionsRequestValues = values;

      await addAuctionItemMutation.mutateAsync(addAuctionRequestValues);
      //toast.success(`Auction ${name} was successfully created`);
    } catch (error) {
      ApiErrorHandler.Handle(error);
    }
  }

  return (
    <>
      <div className="h-screen w-full bg-welcome bg-no-repeat bg-cover bg-center flex justify-center items-center">
        <AddAuctionItemForm onSubmit={addAuctionItem} />
      </div>
    </>
  );
}

export default AddAuctionItemPage;
