import {Component, inject} from "@angular/core";
import {CommonModule} from "@angular/common";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  selector: 'login',
  standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
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

  clickSubmit() {
    console.log("logged in");
  }
}
