import { Component, Input, OnInit } from '@angular/core';
import Limoncello from '../models/limoncello';
import { ActivatedRoute, Router } from '@angular/router';
import { LimoncelloService } from '../services/limoncello.service';
import { BasketItemService } from '../services/basket-item.service';
import BasketItem from '../models/basketItemCreate';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-limoncello-details',
  templateUrl: './limoncello-details.component.html',
  styleUrls: ['./limoncello-details.component.scss']
})
export class LimoncelloDetailsComponent implements OnInit {

  @Input() limoncello!: Limoncello;

  // export default interface BasketItem {

  //   id: number;
  //   nameOfLimoncello: string;
  //   basketId: number;
  //   number: number;
  //   cookie?: string;

  constructor(private route: ActivatedRoute, private router: Router, private limoncelloService: LimoncelloService,
    private basketItemService: BasketItemService, private cookieService: CookieService) { };


  ngOnInit(): void {
    this.getLimoncelloDetails();
  }

  getLimoncelloDetails() {
    const phoneId = this.route.snapshot.paramMap.get('id');
    this.limoncelloService.getLimoncello(parseInt(phoneId!)).subscribe({
      next: (data) => {
        this.limoncello = data;
        console.log(data);
      },
      error: () => {
        this.router.navigate(['/404']);
      }
    });
  }
  getStockText() {
    if (this.limoncello.stock <= 0)
      return "Uitverkocht!";
    if (this.limoncello.stock <= 5)
      return `Nog maar ${this.limoncello.stock} over!`;
    return this.limoncello.stock + " in voorraad.";
  }

  createBasketItem() {
    var cookieValue = this.cookieService.get('Lemonbros');
    var basketItem: BasketItem = {
      id: 0,
      nameOfLimoncello: this.limoncello.name,
      number: 0,
      cookie: cookieValue
    }
    this.basketItemService.createBasketItem(basketItem).subscribe({
      next: () => {
        window.alert('Item(s) put in shoppingcart');
        this.router.navigateByUrl('shoppingcart');
      },
      error: (error) => {
        console.log("Error!!!!!!", error);
      }
    });

  }

}


