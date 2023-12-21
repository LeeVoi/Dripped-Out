import {Component, Input, OnInit} from "@angular/core";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {Product, ProductImageFile, ProductSize, UserCartItems} from "../models";
import {ProductService} from "../services/productservice";
import {ActivatedRoute, Router} from "@angular/router";
import {AsyncPipe, NgForOf} from "@angular/common";
import {ProductColorButtonComponent} from "./product-color-button/product-color-button.component";
import {jwtDecode} from "jwt-decode";
import {FormsModule} from "@angular/forms";
import {UserService} from "../services/userservice";


@Component({
  selector: 'product-details',
  standalone: true,
  imports: [
    TopBarComponent,
    NgForOf,
    ProductColorButtonComponent,
    AsyncPipe,
    FormsModule
  ],
  templateUrl: 'product-details-page.component.html',
  styleUrls:['product-details-page.component.css']
})

export class ProductDetailsPageComponent implements OnInit{

  @Input() product: Product | null | undefined;


  productImages: ProductImageFile[] = [];
  productSizes: ProductSize[] = [];
  selectedSizeIndex: number = 55;
  productId: string | null = "";
  colorId: number = 0;
  selectedQuantity: number = 1;
  user: any


  url: string|undefined
  constructor(private router: Router, private userService: UserService, private productService: ProductService, private route: ActivatedRoute) {

  }
  async ngOnInit() {
    this.productId = this.route.snapshot.paramMap.get('id');
    if (typeof this.productId === "string") {
      await this.productService.getProductById(parseInt(this.productId));
      await this.productService.getProductImages(parseInt(this.productId));
      await this.productService.getProductSizes(parseInt(this.productId));
    }
    this.product = this.productService.currentProduct;
    this.productImages = this.productService.productImageFiles;
    this.productSizes = this.productService.productSizes;

    if (this.productImages[0]) {
      const url = await this.readFile(this.productImages[0].imageFile);
      const imageElement = document.getElementById('myImage') as HTMLImageElement;
      if (imageElement) {
        imageElement.src = url;
        this.colorId = this.productImages[0].colorId;
      }
    }
    const token = localStorage.getItem('jwtToken')
    if(token){
      const decodedToken = jwtDecode(token);
      const expirationDate = new Date(decodedToken.exp! * 1000);
      if(new Date() < expirationDate)
        this.user = decodedToken;
    }
    console.log(this.user.id)
  }

  async switchImage(index: number) {
    const url = await this.readFile(this.productImages[index].imageFile);
    const imageElement = document.getElementById('myImage') as HTMLImageElement;
    if (imageElement) {
      imageElement.src = url;
      this.colorId = this.productImages[index].colorId;
    }
  }
  readFile(file: File): Promise<any> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = (event: any) => resolve(event.target.result);
      reader.onerror = (event: any) => {
        console.log("File could not be read: " + event.target.error.code);
        reject(event.target.error.code);
      };
      reader.readAsDataURL(file);
    });
  }

  selectSize(index: number) {
    this.selectedSizeIndex = index;
  }

  async addToCart(product: Product | null | undefined) {
    var productId = 0;
    var productName = '';
    var price = 0;
    var sizeId = 1;
    if (this.productSizes.length>1){
      sizeId = this.productSizes[this.selectedSizeIndex].sizeId;
    }
    if(product){
      productId = product.productId;
      productName = product.productName;
      price = product.price;
    }

    let cartItem: UserCartItems = {
      productId: productId,
      sizeId: sizeId,
      colorId: this.colorId,
      quantity: Number(this.selectedQuantity),
      productName: productName,
      price: price
    }
    console.log(cartItem);

    await this.userService.addToCart(cartItem);
    this.router.navigate(['' + this.product?.productId])
  }

  getColor(colorId: number) {
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
