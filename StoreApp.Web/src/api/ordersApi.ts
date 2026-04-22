import apiClient from './apiClient';

export interface Order {
  id: number;
  orderId: number;
  customerId: number;
  orderStatus: number;
  orderDate: string;
  requiredDate: string;
  shippedDate: string;
  storeId: number;
  staffId: number;
}

export interface Customer {
  id: number;
  customerId: number;
  firstName: string;
  lastName: string;
  email?: string;
  phone?: string;
  city?: string;
  state?: string;
  zipCode?: string;
}

export interface Store {
  id: number;
  storeId: number;
  storeName: string;
  phone?: string;
  email?: string;
  city?: string;
  state?: string;
}

export const getOrders = () => apiClient.get<Order[]>('/api/orders');
export const getOrderById = (id: number) => apiClient.get<Order>(`/api/orders/${id}`);
export const createOrder = (data: Omit<Order, 'id' | 'orderId'>) =>
  apiClient.post<Order>('/api/orders', data);
export const updateOrderStatus = (id: number, status: number) =>
  apiClient.patch<Order>(`/api/orders/${id}/status`, status);

export const getCustomers = () => apiClient.get<Customer[]>('/api/customers');
export const getCustomerById = (id: number) => apiClient.get<Customer>(`/api/customers/${id}`);
export const createCustomer = (data: Omit<Customer, 'id' | 'customerId'>) =>
  apiClient.post<Customer>('/api/customers', data);

export const getStores = () => apiClient.get<Store[]>('/api/stores');
