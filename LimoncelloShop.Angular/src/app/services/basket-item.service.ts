import { Injectable } from '@angular/core';
import BasketItem from '../models/basketItemCreate';
import { environment } from 'environment';
import { Observable, map } from 'rxjs';
import { ApiService } from './api.service';
import { HttpParams } from '@angular/common/http';
import BasketItemCreate from '../models/basketItemCreate';
import DatabaseBasketItem from '../models/databaseBasketItem';
import BasketItemUpdate from '../models/basketItemUpdate';

@Injectable({
  providedIn: 'root'
})
export class BasketItemService {

  constructor(private apiService: ApiService) { }

  createBasketItem(basketItem: BasketItemCreate): Observable<BasketItemCreate> {
    return this.apiService.post<BasketItemCreate>(environment.apiUrl + "/BasketItem/Create", basketItem);
  }

  updateBasketItem(basketItem: BasketItemUpdate): Observable<BasketItemUpdate> {
    return this.apiService.put<BasketItemUpdate>(environment.apiUrl + "/BasketItem/Edit", basketItem);
  }

  getBasketItem(id: number, cookieValue?: string): Observable<DatabaseBasketItem> {
    let params = new HttpParams();

    if (cookieValue) {
      params = params.set('cookieValue', cookieValue);
    }

    return this.apiService.get<DatabaseBasketItem>(environment.apiUrl + "/BasketItem/" + id, params)
      .pipe(
        map(data => {
          if (data) {
            data.limoncello.imageFileName = `assets/${data.limoncello.imageFileName}`;
          }
          return data;
        })
      );
  }

  getAllBasketItems(cookieValue?: string): Observable<DatabaseBasketItem[]> {
    let params = new HttpParams();

    if (cookieValue) {
      params = params.set('cookieValue', cookieValue);
    }

    return this.apiService.get<DatabaseBasketItem[]>(environment.apiUrl + "/BasketItem", params)
      .pipe(
        map(data => {
          return data.map(item => {
            item.limoncello.imageFileName = `assets/${item.limoncello.imageFileName}` // Update the property
            return {
              ...item, // Spread the existing properties from the item
            };
          });
        })
      );
  }

  deleteBasketItem(id: number): Observable<BasketItem> {
    return this.apiService.delete<BasketItem>(environment.apiUrl + "/BasketItem/Delete/" + id);
  }
}
