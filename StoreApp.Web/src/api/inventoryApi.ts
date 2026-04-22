import apiClient from './apiClient';

export interface Product {
  id: number;
  productId: number;
  productName: string;
  brandId: number;
  categoryId: number;
  modelYear: number;
  listPrice: number;
}

export interface Brand {
  id: number;
  brandId: number;
  brandName: string;
}

export interface Category {
  id: number;
  categoryId: number;
  categoryName: string;
}

export interface Stock {
  storeId: number;
  productId: number;
  quantity: number;
}

export const getProducts = () => apiClient.get<Product[]>('/api/products');
export const getProductById = (id: number) => apiClient.get<Product>(`/api/products/${id}`);
export const createProduct = (data: Omit<Product, 'id' | 'productId'>) =>
  apiClient.post<Product>('/api/products', data);
export const updateProduct = (id: number, data: Product) =>
  apiClient.put<Product>(`/api/products/${id}`, data);
export const deleteProduct = (id: number) => apiClient.delete(`/api/products/${id}`);

export const getBrands = () => apiClient.get<Brand[]>('/api/brands');
export const getCategories = () => apiClient.get<Category[]>('/api/categories');

export const getStocks = () => apiClient.get<Stock[]>('/api/stocks');
export const getStockByProduct = (productId: number) =>
  apiClient.get<Stock[]>(`/api/stocks/product/${productId}`);
export const updateStock = (data: Stock) => apiClient.put<Stock>('/api/stocks', data);
