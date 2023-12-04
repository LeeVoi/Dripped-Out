import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'product-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: 'product-card.component.html',
  styleUrls: ['product-card.component.css']
})

export class ProductCardComponent{
  constructor(private router: Router) {
  }
  clickProductCard(){
    this.router.navigate(['product-details'])
  }
}
