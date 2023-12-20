import {Component, inject, OnInit} from "@angular/core";
import {CommonModule} from "@angular/common";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {CartItemComponent} from "../cart-item/cart-item.component";
import {ProductCardComponent} from "../product-card/product-card.component";
import {Product, UserCartItem, UserLikesItem} from "../models";
import {UserService} from "../services/userservice";



@Component({
  selector: 'cart',
  standalone: true,
  imports: [CommonModule, TopBarComponent, CartItemComponent, ProductCardComponent],
  templateUrl: 'cart.component.html',
  styleUrls: ['cart.component.css']
})

export class CartComponent implements OnInit {
  showBackground: boolean = true;
  userCartItems: UserCartItem[] = [];

  constructor(private userService: UserService) {
  }

  async ngOnInit() {
    await this.userService.getCartItems();
    this.userCartItems = this.userService.userCartItems;
  }

  async removeProduct(item: UserCartItem) {
    await this.userService.removeFromCart(item);
    await this.userService.getCartItems();
    this.userCartItems = this.userService.userCartItems;
  }

  async addToFavorites(item: UserCartItem) {
   await this.userService.addToLiked(item);

  }
}
