import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable, map } from 'rxjs';
import Limoncello from '../models/limoncello';
import { environment } from 'environment';

@Injectable({
  providedIn: 'root'
})
export class LimoncelloService {

  constructor(private apiService: ApiService) { }

  create(limoncello: Limoncello): Observable<Limoncello> {
    return this.apiService.post<Limoncello>(environment.apiUrl + "/Limoncello/Create", limoncello);
  }

  update(limoncello: Limoncello): Observable<Limoncello> {
    return this.apiService.put<Limoncello>(environment.apiUrl + "/Limoncello/Edit", limoncello);
  }

  getLimoncello(id: number): Observable<Limoncello> {
    return this.apiService.get<Limoncello>(environment.apiUrl + "/Limoncello/" + id)
      .pipe(
        map(data => {
          data.imageFileName = `assets/${data.imageFileName}`;
          return data;
        }));
  }

  getAllLimoncello(): Observable<Limoncello[]> {
    return this.apiService.get<Limoncello[]>(environment.apiUrl + "/Limoncello")
      .pipe(
        map(data => {
          return data.map(item => {
            return {
              ...item, // Spread the existing properties from the item
              imageFileName: `assets/${item.imageFileName}` // Update the property
            };
          });
        })
      );
  }


  deleteLimoncello(id: number): Observable<Limoncello> {
    return this.apiService.delete<Limoncello>(environment.apiUrl + "/Limoncello/Delete/" + id);
  }
}
