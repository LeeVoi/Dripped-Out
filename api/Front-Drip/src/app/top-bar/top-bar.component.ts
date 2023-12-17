import {Component, inject, OnInit} from "@angular/core";
import {CommonModule} from "@angular/common";
import {jwtDecode} from "jwt-decode";

@Component({
  selector: 'top-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: 'top-bar.component.html',
  styleUrls: ['top-bar.component.css']
})

export class TopBarComponent{
  user: any;


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

  logout(){
    localStorage.removeItem('jwtToken')
  }
}
