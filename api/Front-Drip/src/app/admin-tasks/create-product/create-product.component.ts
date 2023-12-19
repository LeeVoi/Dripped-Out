import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {FormsModule, NgForm} from "@angular/forms";
import {ProductService} from "../../services/productservice";


@Component({
  selector: 'create-product',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: 'create-product.component.html',
  styleUrls: ['create-product.component.css']
})

export class CreateProductComponent{
  productTypes = ['Pants', 'T-Shirt', 'Long Sleeve', 'Jacket', 'Dresses', 'Shorts', 'Hats', 'Jewelery'];
  colors = ['Blue', 'Black', 'White', 'Green', 'Yellow', 'Red', 'Purple', 'Orange'];
  colorFiles: File[] = [];
  genders = ['Male','Female'];
  selectedColors: string[] = [];


  constructor(private productService: ProductService) {
  }

  addColor(color: string, index: number) {
    this.selectedColors.push(color);
    this.colors.splice(index, 1)
  }

  removeColor(color: string, index: number) {
    this.selectedColors.splice(index, 1);
    this.colorFiles.splice(index, 1);
    this.colors.push(color)
  }

  onFileChange(event: any, index: number) {
    this.colorFiles[index] = event.target.file[0];
  }

  onSubmit(form: NgForm) {
    this.productService
  }
}
