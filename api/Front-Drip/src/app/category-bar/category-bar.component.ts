import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {ProductService} from "../services/productservice";

@Component({
  selector: 'category-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: 'category-bar.component.html',
  styleUrls: ['category-bar.component.css']
})

export class CategoryBarComponent{

  constructor(private productservice : ProductService) {}

  async clickType(type : string)
  {
    await this.productservice.getProductByTypeId(type);
    console.log(this.productservice.products);
  }
  async clickGenderType(gender : string , type : string)
  {
    await this.productservice.getProductByGenderAndTypeId(gender, type);
    console.log(this.productservice.products)
  }


  protected readonly String = String;
}
