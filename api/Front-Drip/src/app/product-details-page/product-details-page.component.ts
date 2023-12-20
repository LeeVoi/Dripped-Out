import {Component, Input, OnInit} from "@angular/core";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {Product, ProductImageFile} from "../models";
import {ProductService} from "../services/productservice";
import {ActivatedRoute} from "@angular/router";
import {NgForOf} from "@angular/common";
import {ProductColorButtonComponent} from "./product-color-button/product-color-button.component";


@Component({
  selector: 'product-details',
  standalone: true,
  imports: [
    TopBarComponent,
    NgForOf,
    ProductColorButtonComponent
  ],
  templateUrl: 'product-details-page.component.html',
  styleUrls:['product-details-page.component.css']
})

export class ProductDetailsPageComponent implements OnInit{

  @Input() product: Product | null | undefined;

  productImages: ProductImageFile[] = [];
  productId: string | null = "";


  constructor(private productService: ProductService, private route: ActivatedRoute) {

  }
  async ngOnInit() {
    this.productId = this.route.snapshot.paramMap.get('id');
    if (typeof this.productId === "string") {
      await this.productService.getProductById(parseInt(this.productId));
      await this.productService.getProductImages(parseInt(this.productId));
    }
    this.product = this.productService.currentProduct;
    this.productImages = this.productService.productImageFiles;
    console.log(this.productImages)
  }


}
