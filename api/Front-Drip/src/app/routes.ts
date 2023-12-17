import {Routes} from "@angular/router";
import {HomeComponent} from "./home/home.component";
import {SignupComponent} from "./signup/signup.component";
import {LoginComponent} from "./login/login.component";
import {CartComponent} from "./cart/cart.component";
import {ProductDetailsPageComponent} from "./product-details-page/product-details-page.component";
import {AdminControlsComponent} from "./admin-controls/admin-controls.component";

const routeConfig: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'Dripped Out'
  },

  {
    path: 'signup',
    component: SignupComponent,
    title: 'Signup'
  },

  {
    path: 'login',
    component: LoginComponent,
    title: 'Login'
  },

  {
    path: 'cart',
    component: CartComponent,
    title: 'Shopping Cart'
  },

  {
    path: 'product-details',
    component: ProductDetailsPageComponent,
    title: 'Product Details'
  },
  {
    path: 'admincontrols',
    component: AdminControlsComponent,
    title: 'Admin Controls'
  }
];

export default routeConfig;
