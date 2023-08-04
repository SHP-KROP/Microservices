import { toast } from 'react-toastify';

export class ApiErrorHandler {
  static Handle(error: any) {
    let statusCode = error?.response?.status ?? 500;
    if (statusCode >= 500) {
      toast.error('Internal server error');
      return;
    }
    if (statusCode >= 400) {
      toast.warning(error?.response?.data ?? 'Unexpected error occurred');
    }
  }
}
