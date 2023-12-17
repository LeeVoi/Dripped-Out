import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Product} from "../models";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  // A list of all the products
  private products: Product[] = [];

  // The current selected product
  private currentProduct: Product | null = null;

  constructor(private http: HttpClient) { }

  // A method to get all the products from the server
  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>('api/products');
  }

  // A method to get a product by id from the server
  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`api/products/${id}`);
  }

  // A method to update a product on the server
  updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(`api/products/${product.productId}`, product);
  }

  // A method to delete a product on the server
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`api/products/${id}`);
  }

  // A method to set the current product
  setCurrentProduct(product: Product): void {
    this.currentProduct = product;
  }

  // A method to get the current product
  getCurrentProduct(): Product | null {
    return this.currentProduct;
  }

}
