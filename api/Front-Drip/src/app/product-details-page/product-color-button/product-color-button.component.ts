import {Component, Input, OnInit} from "@angular/core";
import {TopBarComponent} from "../../top-bar/top-bar.component";
import {Product, ProductImageFile} from "../../models";
import {ProductService} from "../../services/productservice";
import {ActivatedRoute} from "@angular/router";
import {NgForOf, NgStyle} from "@angular/common";


@Component({
  selector: 'product-color-button',
  standalone: true,
  imports: [
    TopBarComponent,
    NgForOf,
    NgStyle
  ],
  templateUrl: 'product-color-button.component.html',
  styleUrls:['product-color-button.component.css']
})

export class ProductColorButtonComponent implements OnInit{

  @Input() productImage: ProductImageFile | undefined;
  @Input() ngStyle!: { 'background-color': any };


  constructor() {
  }
  ngOnInit() {

  }


  getColor(colorId: number | undefined) {
    let color: string | undefined;
    switch (colorId){
      case 1:
        color='blue';
        break;
      case 2:
        color='black';
        break;
      case 3:
        color='white';
        break;
      case 4:
        color='green';
        break;
      case 5:
        color='yellow';
        break;
      case 6:
        color='red';
        break;
      case 7:
        color='purple';
        break;
      case 8:
        color='orange';
        break;
    }
    return color;
  }
}
