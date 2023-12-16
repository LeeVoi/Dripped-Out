import {Injectable} from "@angular/core";
import {Product} from "../models";

@Injectable({
  providedIn: 'root'
})
export class Productservice{
  allProducts: Product[] = [];
  selectedProduct: Product | undefined;

  getSelectedProduct(){
    return this.selectedProduct;
  }
  setSelectedProduct(product: Product){
    this.selectedProduct = product;
  }

  updateAllProducts(){
    //TODO retrieve current list of all products from the database
  }

  getAllProducts(){
    return this.allProducts;
  }
}
