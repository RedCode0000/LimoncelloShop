import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import Basket from '../models/basket';
import { environment } from 'environment';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private apiService: ApiService) { }

  createBasket(basket: Basket): Observable<Basket> {
    return this.apiService.post<Basket>(environment.apiUrl + "/Basket/Create", basket);
  }

  getBasket(id: number): Observable<Basket> {
    return this.apiService.get<Basket>(environment.apiUrl + "/Basket/" + id);
  }

  deleteBasket(id: number): Observable<Basket> {
    return this.apiService.delete<Basket>(environment.apiUrl + "/Basket/Delete/" + id);
  }
}
