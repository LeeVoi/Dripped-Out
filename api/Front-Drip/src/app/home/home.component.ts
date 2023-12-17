import {Component, HostListener, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ProductCardComponent} from "../product-card/product-card.component";
import {TopBarComponent} from "../top-bar/top-bar.component";
import {CategoryBarComponent} from "../category-bar/category-bar.component";
import routes from "../routes";
import {Router} from "@angular/router";
import {ProductService} from "../services/productservice";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ProductCardComponent, TopBarComponent, CategoryBarComponent],
  templateUrl: 'home.component.html',
  styleUrls:['./home.component.css']
})

export class HomeComponent implements OnInit{

  showBackground = true;

  products: any[] = [];

  constructor(private httpClient: HttpClient) {
    this.fetchProducts();

  }

  fetchProducts() {
    this.httpClient.get<any[]>('api/Products').subscribe((data: any[]) => {
      this.products = data;
    });
  }

  ngOnInit() {

  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e: Event) {
    let element = document.querySelector('.background') as HTMLElement;
    let scrollPosition = window.scrollY;
    let windowHeight = window.innerHeight;
    let opacity = 1 - 3 * scrollPosition / windowHeight;

    if (opacity < 0) {
      opacity = 0;
    } else if (opacity > 1) {
      opacity = 1;
    }

    element.style.opacity = opacity.toString();
  }


}
