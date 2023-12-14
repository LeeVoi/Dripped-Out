import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  selector: 'login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: 'login.component.html',
  styleUrls: ['login.component.css']
})

export class LoginComponent{
  emailInput = new FormControl('', [Validators.required]);
  passwordInput = new FormControl('', [Validators.required]);

  formGroup = new FormGroup({
    email: this.emailInput,
    password: this.passwordInput
  });

  constructor() {
  }


  async login() {
    const email = this.formGroup.get('email')?.value;
    const password = this.formGroup.get('password')?.value;

    const response = await fetch('http://localhost:5027/loginuser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        email: email,
        password: password
      })
    });

    if(response.status === 200){
      //retrieve token if login successful
      const data = await response.json();
      const token = data.token;
      console.log(token);
      //store token as "jwtToken" for attaching to future secure requests
      localStorage.setItem('jwtToken', token);

      //successful login, reroute to homepage
      window.location.href = '/';

    }else{
      console.error('Unauthorized')
    }
  }
}
