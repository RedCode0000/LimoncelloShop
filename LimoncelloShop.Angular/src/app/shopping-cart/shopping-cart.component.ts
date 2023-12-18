import { Component, Input } from '@angular/core';
import BasketItem from '../models/basketItem';
import { BasketItemService } from '../services/basket-item.service';
import { CookieService } from 'ngx-cookie-service';
import { ToBasketItem } from '../models/databaseBasketItem';
import { ShoppingCartService } from '../services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent {

  allBasketItems: BasketItem[] = [];
  foo: number = 0;
  total: number = 0;

  constructor(private basketItemService: BasketItemService, private cookieService: CookieService, private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    this.getAllItemsInitial();
  }

  getAllItemsInitial() {
    var cookieValue = this.cookieService.get('Lemonbros');
    this.basketItemService.getAllBasketItems(cookieValue).subscribe(
      (x) => {
        this.allBasketItems = x.map(t => ToBasketItem(t));
        this.allBasketItems.map(y => this.total += y.limoncello.price * y.number);
        this.shoppingCartService.updateItemCount();
      }
    );
  }

  getAllItems() {
    var cookieValue = this.cookieService.get('Lemonbros');
    this.basketItemService.getAllBasketItems(cookieValue).subscribe(
      (x) => {
        this.allBasketItems = x.map(t => ToBasketItem(t));
        this.shoppingCartService.updateItemCount();
      }
    );
  }

  totalCostChanged(event: any) {
    this.total += event;
    this.getAllItems();
    console.log('Total Updated:', this.total);

  }
}
