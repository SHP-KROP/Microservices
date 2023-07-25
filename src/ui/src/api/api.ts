import axios from 'axios';
import { IRegisterFormValues } from '../interfaces/Forms/IRegisterFormValues';
import { ILoginFormValues } from '../interfaces/Forms/ILoginFormValues';

const baseURL = 'https://localhost:5001/api/auth';

export const login = async (values: ILoginFormValues) => {
  const response = await axios.post(`${baseURL}/login`, values);
  return response.data;
};

export const register = async (values: IRegisterFormValues) => {
  const response = await axios.post(`${baseURL}/register`, values);
  return response.data;
};
