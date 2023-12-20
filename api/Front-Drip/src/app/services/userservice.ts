import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {firstValueFrom, Observable} from 'rxjs';
import {Product, User, UserCartItem, UserLikesItem} from "../models";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // A list of all the users
  private users: User[] = [];

  // The current logged in user
  private currentUser: User | null = null;

  // A list of user cart items
  userCartItems: UserCartItem[] = [];

  // A list of user liked items
  private userLikedItems: UserLikesItem[] = [];

  constructor(private http: HttpClient) { }

  // A method to get all the users from the server
  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>('api/users');
  }

  // A method to get a user by id from the server
  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`api/users/${id}`);
  }

  // A method to update a user on the server
  updateUser(user: User): Observable<User> {
    return this.http.put<User>(`api/users/${user.userId}`, user);
  }

  // A method to delete a user on the server
  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`api/users/${id}`);
  }

  // A method to set the current user
  setCurrentUser(user: User): void {
    this.currentUser = user;
  }

  // A method to get the current user
  getCurrentUser(): User | null {
    return this.currentUser;
  }

  // A method to add a product to the user cart
  async addToCart(product: UserCartItem){
    const call = this.http.post<any>('http://localhost:5027/api/AddProductToUserCart', { userId: this.currentUser?.userId, productId: product.productId });
    const result= await firstValueFrom<UserCartItem>(call);
    this.userCartItems.push(result);
  }

  // A method to remove a product from the user cart
  async removeFromCart(product: UserCartItem) {
    const call = this.http.delete<string>(`http://localhost:5027/api/UserCart/RemoveProductFromCart?productId=${product.productId} &colorId=${product.colorId} &sizeId=${product.sizeId} &quantity=${product.quantity}`)
    const result= await firstValueFrom<string>(call);
    if (result==='Product removed from cart successfully.'){
      const index = this.userCartItems.indexOf(product);
      if (index > -1) {
        this.userCartItems.splice(index, 1);
      }
    }
  }

  // A method to get the user cart items
  async getCartItems() {
    const call = this.http.get<UserCartItem[]>(`http://localhost:5027/api/GetUserCartProducts`);
    const result = await firstValueFrom<UserCartItem[]>(call);
    this.userCartItems = result;
  }

  // A method to add a product to the user liked items
  async addToLiked(product: UserLikesItem){
    const call= this.http.post<any>('http://localhost:5027/api/AddProductToUserLikes', { userId: this.currentUser?.userId, productId: product.productId });
    const result = await firstValueFrom<UserLikesItem>(call);
    this.userLikedItems.push(result)
  }

  // A method to remove a product from the user liked items
  async removeFromLiked(product: UserLikesItem){
    const call = this.http.delete<string>(`http://localhost:5027/api/UserCart/RemoveProductFromLiked?productId=${product.productId}`);
    const result = await firstValueFrom<string>(call);
    if (result==='Product removed from likes succesfully'){
      const index = this.userLikedItems.indexOf(product)
      if (index>-1){
        this.userLikedItems.splice(index,1)
      }
    }
  }

  // A method to get the user liked items
  async getLikedItems(): Promise<void> {
    const call= this.http.get<UserLikesItem[]>('http://localhost:5027/api/GetUserLikes');
    const result = await firstValueFrom<UserLikesItem[]>(call);
    this.userLikedItems = result;
  }
}
