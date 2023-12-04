import {Component} from "@angular/core";
import {TopBarComponent} from "../top-bar/top-bar.component";


@Component({
  selector: 'product-details',
  standalone: true,
  imports: [
    TopBarComponent
  ],
  templateUrl: 'product-details-page.component.html',
  styleUrls:['product-details-page.component.css']
})

export class ProductDetailsPageComponent{

}
