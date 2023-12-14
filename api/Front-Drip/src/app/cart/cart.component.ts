import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {CartItemComponent} from "../cart-item/cart-item.component";

@Component({
  selector: 'cart',
  standalone: true,
  imports: [CommonModule, TopBarComponent, CartItemComponent],
  templateUrl: 'cart.component.html',
  styleUrls: ['cart.component.css']
})

export class CartComponent{

}
