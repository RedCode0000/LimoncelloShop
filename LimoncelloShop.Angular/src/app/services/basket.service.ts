import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import Basket from '../models/basket';
import { environment } from 'environment';
import { HttpParams } from '@angular/common/http';
import DatabaseBasket from '../models/databaseBasket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private apiService: ApiService) { }

  createBasket(basket: Basket): Observable<Basket> {
    return this.apiService.post<Basket>(environment.apiUrl + "/Basket/Create", basket);
  }

  getBasket(key?: string): Observable<DatabaseBasket> {
    let params = new HttpParams();

    if (key) {
      params = params.set('key', key);
    }
    return this.apiService.get<DatabaseBasket>(environment.apiUrl + "/Basket/", params);
  }

  deleteBasket(id: number): Observable<Basket> {
    return this.apiService.delete<Basket>(environment.apiUrl + "/Basket/Delete/" + id);
  }
}
