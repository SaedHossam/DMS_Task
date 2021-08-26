import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { EnvironmentUrlService } from './environment-url.service';
import { Cart } from 'src/app/models/cart';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(public http: HttpClient, private _envUrl: EnvironmentUrlService) { }


  public getCartItems(): Observable<Cart> {
    return this.http.get<Cart>(this._envUrl.urlAddress + '/api/Carts');
  }

  public AddToCart(itemId: number, quantity: number): Observable<Cart> {
    return this.http.post<Cart>(this._envUrl.urlAddress + '/api/Carts', {itemId, quantity});
  }

  public DeleteFromCart(itemId: number): Observable<Cart> {
    return this.http.delete<Cart>(this._envUrl.urlAddress + '/api/Carts/' + itemId);
  }

  
}
