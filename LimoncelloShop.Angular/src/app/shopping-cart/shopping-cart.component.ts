import { Component } from '@angular/core';
import BasketItem from '../models/basketItem';
import { BasketItemService } from '../services/basket-item.service';
import { CookieService } from 'ngx-cookie-service';
import { ToBasketItem } from '../models/databaseBasketItem';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent {

  allBasketItems: BasketItem[] = [];

  constructor(private basketItemService: BasketItemService, private cookieService: CookieService) { }

  ngOnInit(): void {
    this.getAllItems();

  }

  getAllItems() {
    var cookieValue = this.cookieService.get('Lemonbros');
    this.basketItemService.getAllBasketItems(cookieValue).subscribe(
      (x) => {
        this.allBasketItems = x.map(t => ToBasketItem(t));
      }
    );
  }
}
