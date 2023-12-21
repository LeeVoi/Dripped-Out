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
  sizes: string[] = ['XS', 'S', 'M', 'L', 'XL', 'XXL'];
  colorFiles: File[] = [];
  genders = ['Male','Female'];
  selectedColors: string[] = [];
  selectedSizes: string[] = [];


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
    if (event.target.files && event.target.files.length > 0) {
      this.colorFiles[index] = event.target.files[0];
    }
  }

  onSizeChange(event: any, index: number) {
    if (event.target.checked) {
      this.selectedSizes.push(this.sizes[index]);
    } else {
      const sizeIndex = this.selectedSizes.indexOf(this.sizes[index]);
      if (sizeIndex > -1) {
        this.selectedSizes.splice(sizeIndex, 1);
      }
    }
  }

  async onSubmit(formValues: any) {
    const { productName: productName, productType: productType, productPrice: productPrice, productGender: productGender, productDescription: productDescription } = formValues;
    await this.productService.createNewProduct(productName, productType, this.selectedColors, this.colorFiles, this.selectedSizes, productPrice, productGender, productDescription);
  }
}
