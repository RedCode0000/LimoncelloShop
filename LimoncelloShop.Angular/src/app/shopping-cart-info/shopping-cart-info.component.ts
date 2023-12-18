import { Component, EventEmitter, Input, Output } from '@angular/core';
import BasketItem from '../models/basketItem';
import { BasketItemService } from '../services/basket-item.service';
import BasketItemUpdate from '../models/basketItemUpdate';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { BasketService } from '../services/basket.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-shopping-cart-info',
  templateUrl: './shopping-cart-info.component.html',
  styleUrls: ['./shopping-cart-info.component.scss']
})
export class ShoppingCartInfoComponent {

  @Input() basketItem!: BasketItem;

  @Input() costs: number = 0;

  @Output() refresh = new EventEmitter<number>;

  @Output() updateTotalCost = new EventEmitter<number>;


  counterValue: number = 1;
  calculatedValue: number = 0;
  totalCost: number = 0;
  difference: number = 0;


  constructor(private shoppingCartService: ShoppingCartService, private basketItemService: BasketItemService) { }

  ngOnInit() {
    this.counterValue = this.basketItem.number;
    this.calculatedValue = this.basketItem.number * this.basketItem.limoncello.price;
  }


  handleInputChange() {
    // Ensure the entered value is within the specified range
    if (this.counterValue < 1) {
      this.counterValue = 1;
    }
    else if (this.counterValue > this.basketItem.limoncello.stock) {
      this.counterValue = this.basketItem.limoncello.stock;
    }
    this.difference = this.counterValue - this.basketItem.number;

    console.log('Input value changed:', this.counterValue);
    let basketItemUpdate: BasketItemUpdate = {
      id: this.basketItem.id,
      number: this.counterValue
    };

    this.basketItemService.updateBasketItem(basketItemUpdate).subscribe((x) => {
      this.calculatedValue = x.number * this.basketItem.limoncello.price;
      var changeValue: number = this.difference * this.basketItem.limoncello.price;
      this.updateTotalCost.emit(changeValue);
      this.shoppingCartService.updateItemCount();
    });
  }

  deleteBasketItem() {
    if (!this.basketItem.id)
      return;

    this.basketItemService.getBasketItem(this.basketItem.id).subscribe({
      next: (x) => {
        var subtotalBasketItemToRemove: number = x.limoncello.price * x.number;
        this.basketItemService.deleteBasketItem(this.basketItem.id).subscribe({
          complete: () => {
            this.updateTotalCost.emit(-subtotalBasketItemToRemove);
          }
        });
      }
    })
  }
}

