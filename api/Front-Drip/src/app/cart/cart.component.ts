import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {TopBarComponent} from "../top-bar/top-bar.component";

@Component({
  selector: 'cart',
  standalone: true,
  imports: [CommonModule, TopBarComponent],
  templateUrl: 'cart.component.html',
  styleUrls: ['cart.component.css']
})

export class CartComponent{

}
