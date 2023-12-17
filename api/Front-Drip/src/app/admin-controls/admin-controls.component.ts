import {Component, OnInit, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {jwtDecode} from "jwt-decode";
import {CreateProductComponent} from "../admin-tasks/create-product/create-product.component";
import {DeleteProductComponent} from "../admin-tasks/delete-product/delete-product.component";
import {EditProductComponent} from "../admin-tasks/edit-product/edit-product.component";
import {DeleteUserComponent} from "../admin-tasks/delete-user/delete-user.component";


@Component({
  selector: 'admin-controls',
  standalone: true,
  imports: [CommonModule, CreateProductComponent, DeleteProductComponent, EditProductComponent, DeleteUserComponent],
  templateUrl: 'admin-controls.component.html',
  styleUrls: ['admin-controls.component.css']
})

export class AdminControlsComponent{
  user: any;
  selectedTask: any;


  ngOnInit(){
    const token = localStorage.getItem('jwtToken')
    if(token){
      const decodedToken = jwtDecode(token);
      console.log(decodedToken);
      const expirationDate = new Date(decodedToken.exp! * 1000);
      if(new Date() < expirationDate)
        this.user = decodedToken;
    }
  }

  changeTask(task: string){
    this.selectedTask = task;
  }

}
