import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BasketService } from './basket.service';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class ShoppingCartService {
  private itemCountSubject = new BehaviorSubject<number>(0);
  itemCount$ = this.itemCountSubject.asObservable();

  constructor(private basketService: BasketService, private cookieService: CookieService) { }

  updateItemCount(): void {
    const cookie = this.cookieService.get('Lemonbros');

    this.basketService.getBasket(cookie).subscribe(
      (basketData) => {
        const itemCount = basketData.totalNumberOfItems;
        if (itemCount > 99) {
          this.itemCountSubject.next(99);
        } else {
          this.itemCountSubject.next(itemCount);
        }
      },
      (error) => {
        console.error('Error updating cart:', error);
      }
    );
  }
}
