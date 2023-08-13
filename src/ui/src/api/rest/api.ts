import axios from 'axios';
import { IRegisterFormValues } from '../../interfaces/Forms/IRegisterFormValues';
import { ILoginFormValues } from '../../interfaces/Forms/ILoginFormValues';
import { ILoginResponse } from '../../interfaces/Auth/ILoginResponse';
import { IRegisterResponse } from '../../interfaces/Auth/IRegisterResponse';
import { ICreateAuctionFormValues } from '../../interfaces/Forms/CreateAuction/ICreateAuctionFormValues';
import { ICreateAuctionResponse } from '../../interfaces/Responses/Auction/ICreateAuctionResponse';
import Cookies from 'js-cookie';
import { IAddAuctionItemFormValues } from '../../interfaces/Forms/AddAuctionItem/IAddAuctionItemFormValues';
import { IAddAuctionItemResponse } from '../../interfaces/Responses/AuctionItem/IAddAuctionItemResponse';

export interface APIClient {
  login: (values: ILoginFormValues) => Promise<ILoginResponse>;
  register: (values: IRegisterFormValues) => Promise<IRegisterResponse>;
  createAuction: (
    values: ICreateAuctionFormValues
  ) => Promise<ICreateAuctionResponse>;
  addAuctionItem: (
    values: IAddAuctionsRequestValues
  ) => Promise<IAddAuctionItemResponse>;
}

const gatewayURL = `${import.meta.env.VITE_GATEWAY_URL!}`;

const axiosClient = axios.create({
  baseURL: gatewayURL,
  headers: {
    Authorization: `Bearer ${Cookies.get('token')}`,
  },
});

const apiClient: APIClient = {
  async login(values: ILoginFormValues) {
    const response = await axiosClient.post<ILoginResponse>(
      `${gatewayURL}/api/auth/login`,
      values
    );
    return response.data;
  },
  async register(values: IRegisterFormValues) {
    const response = await axiosClient.post<IRegisterResponse>(
      `${gatewayURL}/api/auth/register`,
      values
    );
    return response.data;
  },
  async createAuction(values: ICreateAuctionFormValues) {
    const response = await axiosClient.post<ICreateAuctionResponse>(
      `${gatewayURL}/api/auctions`,
      values
    );
    return response.data;
  },
  async addAuctionItem(values: IAddAuctionsRequestValues) {
    var formData = toFormData(values);

    const response = await axiosClient.post<IAddAuctionItemResponse>(
      `${gatewayURL}/api/auctions/${values.auctionId}/items`,
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      }
    );

    return response.data;
  },
};

export interface IAddAuctionsRequestValues extends IAddAuctionItemFormValues {
  auctionId: string;
}

function toFormData(formValues) {
  const formData = new FormData();

  for (let key in formValues) {
    formData.append(key, formValues[key]);
  }

  return formData;
}

export default apiClient;
