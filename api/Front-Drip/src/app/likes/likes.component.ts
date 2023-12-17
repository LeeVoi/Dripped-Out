import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {CartItemComponent} from "../cart-item/cart-item.component";
import {ProductCardComponent} from "../product-card/product-card.component";

@Component({
  selector: 'likes',
  standalone: true,
  imports: [CommonModule, TopBarComponent],
  templateUrl: 'likes.component.html',
  styleUrls: ['likes.component.css']
})

export class LikesComponent{

}
