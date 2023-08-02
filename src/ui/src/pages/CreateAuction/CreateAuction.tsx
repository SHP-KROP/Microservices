import React from 'react';
import CreateAuctionForm from './../../components/forms/auction/CreateAuctionForm';
import apiClient from '../../api/rest/api';
import { useMutation } from 'react-query';
import { ICreateAuctionFormValues } from '../../interfaces/Forms/CreateAuction/ICreateAuctionFormValues';
import { toast } from 'react-toastify';
import { ApiErrorHandler } from '../../api/rest/apiErrorHandler';

function CreateAuction() {
  const createAuctionMutation = useMutation(apiClient.createAuction);

  async function createAuction(values: ICreateAuctionFormValues) {
    try {
      let { name } = await createAuctionMutation.mutateAsync(values);
      toast.success(`Auction ${name} was successfully created`);
    } catch (error) {
      ApiErrorHandler.Handle(error);
    }
  }

  return <CreateAuctionForm onSubmit={createAuction} />;
}

export default CreateAuction;
