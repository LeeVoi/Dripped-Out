import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {CartItemComponent} from "../cart-item/cart-item.component";
import {ProductCardComponent} from "../product-card/product-card.component";

@Component({
  selector: 'cart',
  standalone: true,
  imports: [CommonModule, TopBarComponent, CartItemComponent, ProductCardComponent],
  templateUrl: 'cart.component.html',
  styleUrls: ['cart.component.css']
})

export class CartComponent{

}
