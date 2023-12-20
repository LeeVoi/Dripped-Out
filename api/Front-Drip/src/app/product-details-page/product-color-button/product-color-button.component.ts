import {Component, Input, OnInit} from "@angular/core";
import {TopBarComponent} from "../../top-bar/top-bar.component";
import {Product, ProductImageFile} from "../../models";
import {ProductService} from "../../services/productservice";
import {ActivatedRoute} from "@angular/router";
import {NgForOf} from "@angular/common";


@Component({
  selector: 'product-color-button',
  standalone: true,
  imports: [
    TopBarComponent,
    NgForOf
  ],
  templateUrl: 'product-color-button.component.html',
  styleUrls:['product-color-button.component.css']
})

export class ProductColorButtonComponent implements OnInit{

  @Input() productImage: ProductImageFile | undefined;


  constructor() {
  }
  ngOnInit() {

  }


}
