import { Component, Input } from '@angular/core';
import BasketItem from '../models/basketItem';
import { BasketItemService } from '../services/basket-item.service';
import BasketItemUpdate from '../models/basketItemUpdate';

@Component({
  selector: 'app-shopping-cart-info',
  templateUrl: './shopping-cart-info.component.html',
  styleUrls: ['./shopping-cart-info.component.scss']
})
export class ShoppingCartInfoComponent {

  @Input() basketItem!: BasketItem;


  counterValue: number = 0;
  calculatedValue: number = 0;
  maxValue: number = 100;


  constructor(private basketItemService: BasketItemService) { }

  ngOnInit() {
    this.counterValue = this.basketItem.number;
    this.calculatedValue = this.basketItem.number * this.basketItem.limoncello.price;
  }


  handleInputChange() {

    // Ensure the entered value is within the specified range
    if (this.counterValue < 1) {
      this.counterValue = 1;
    } else if (this.counterValue > this.maxValue) {
      this.counterValue = this.maxValue;

      console.log('Input value changed:', this.counterValue);
      let basketItemUpdate: BasketItemUpdate = {
        id: this.basketItem.id,
        number: this.counterValue
      };
      this.basketItemService.updateBasketItem(basketItemUpdate).subscribe((x) => {
        this.calculatedValue = x.number * this.basketItem.limoncello.price;
        console.log(this.calculatedValue);
        console.log(x.number);
      });
    }

    // updateAmount() {
    //   debugger;
    //   let basketItemUpdate: BasketItemUpdate = {
    //     id: this.basketItem.id,
    //     number: this.counterValue
    //   };
    //   this.basketItemService.updateBasketItem(basketItemUpdate).subscribe((x) => {
    //     this.calculatedValue = x.number * this.basketItem.limoncello.price;
    //     console.log(this.calculatedValue);
    //     console.log(x.number);
    //   });
    // }
  }
}
