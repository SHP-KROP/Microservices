import axios from 'axios';
import { IRegisterFormValues } from '../../interfaces/Forms/IRegisterFormValues';
import { ILoginFormValues } from '../../interfaces/Forms/ILoginFormValues';

export interface APIClient {
  login: (values: ILoginFormValues) => Promise<any>;
  register: (values: IRegisterFormValues) => Promise<any>;
}

const gatewayURL = `${import.meta.env.VITE_GATEWAY_URL!}/api/auth`;

const apiClient: APIClient = {
  async login(values: ILoginFormValues) {
    const response = await axios.post(`${gatewayURL}/login`, values);
    return response.data;
  },
  async register(values: IRegisterFormValues) {
    const response = await axios.post(`${gatewayURL}/register`, values);
    return response.data;
  },
};

export default apiClient;
