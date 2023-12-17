import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Product, User} from "../models";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // A list of all the users
  private users: User[] = [];

  // The current logged in user
  private currentUser: User | null = null;

  // A list of user cart items
  private userCartItems: Product[] = [];

  // A list of user liked items
  private userLikedItems: Product[] = [];

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
  addToCart(product: Product): void {
    this.userCartItems.push(product);
  }

  // A method to remove a product from the user cart
  removeFromCart(product: Product): void {
    const index = this.userCartItems.indexOf(product);
    if (index > -1) {
      this.userCartItems.splice(index, 1);
    }
  }

  // A method to get the user cart items
  getCartItems(): Product[] {
    return this.userCartItems;
  }

  // A method to add a product to the user liked items
  addToLiked(product: Product): void {
    this.userLikedItems.push(product);
  }

  // A method to remove a product from the user liked items
  removeFromLiked(product: Product): void {
    const index = this.userLikedItems.indexOf(product);
    if (index > -1) {
      this.userLikedItems.splice(index, 1);
    }
  }

  // A method to get the user liked items
  getLikedItems(): Product[] {
    return this.userLikedItems;
  }
}
