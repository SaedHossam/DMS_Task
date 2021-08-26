import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { EnvironmentUrlService } from './environment-url.service';
import { Order } from 'src/app/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(public http: HttpClient, private _envUrl: EnvironmentUrlService) { }

  public getMyOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this._envUrl.urlAddress + '/api/Orders/');
  }

  public getOrderById(id: number) {
    return this.http.get<Order>(this._envUrl.urlAddress + '/api/Orders/' + id).toPromise();
  }

  public addOrder() {
    return this.http.post<Order>(this._envUrl.urlAddress + '/api/Orders', {});
  }
}
