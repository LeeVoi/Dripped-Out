import {Component, inject, OnInit} from "@angular/core";
import {CommonModule} from "@angular/common";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {CartItemComponent} from "../cart-item/cart-item.component";
import {ProductCardComponent} from "../product-card/product-card.component";
import {Product, UserCartItems, UserLikesItem} from "../models";
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
  userCartItems: UserCartItems[] = [];
  totalPrice: number= 0;

  constructor(private userService: UserService) {
  }

  async ngOnInit() {
    await this.userService.getCartItems();
    this.userCartItems = this.userService.userCartItems;
    for (let i = 0; i < this.userCartItems.length; i++) {
      this.totalPrice += this.userCartItems[i].price;
    }
  }

  async removeProduct(item: UserCartItems) {
    await this.userService.removeFromCart(item);
    await this.userService.getCartItems();
    this.userCartItems = this.userService.userCartItems;
  }

  async addToFavorites(item: UserCartItems) {
    let cartLike: UserLikesItem = {
      userId: this.userService.currentUser.userId,
      productId: item.productId
    };
   await this.userService.addToLiked(cartLike);

  }
}
