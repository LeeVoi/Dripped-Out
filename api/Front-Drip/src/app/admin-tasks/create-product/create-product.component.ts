import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {FormsModule, NgForm} from "@angular/forms";


@Component({
  selector: 'create-product',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: 'create-product.component.html',
  styleUrls: ['create-product.component.css']
})

export class CreateProductComponent{
  productTypes = ['Pants', 'T-Shirt', 'Long Sleeve', 'Jacket', 'Dresses', 'Shorts', 'Hats', 'Jewelery'];
  colors = ['Red', 'Green', 'Blue'];
  genders = ['Male','Female'];
  selectedColors: string[] = [];

  addColor(color: string) {
    this.selectedColors.push(color);
  }

  removeColor(index: number) {
    this.selectedColors.splice(index, 1);
  }

  onFileChange(event: any, index: number) {
    // Handle file change
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
  }
}
