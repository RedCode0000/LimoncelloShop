import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { environment } from 'environment';
import Cookie from '../models/cookie';

@Injectable({
  providedIn: 'root'
})
export class CookieServiceAPI {

  constructor(private apiService: ApiService) { }

  createCookie(): Observable<Cookie> {
    return this.apiService.get<Cookie>(environment.apiUrl + "/CreateCookie");
  }

  getCookie(): Observable<Cookie> {
    return this.apiService.get<Cookie>(environment.apiUrl + "/GetCookie");
  }

  // deleteBasketItem(id: number): Observable<Cookie> {
  //   return this.apiService.delete<Cookie>(environment.apiUrl + "/Basket/Delete/" + id);
  // }
}
