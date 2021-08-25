import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { Item } from 'src/app/models/item';

import { EnvironmentUrlService } from './environment-url.service';
@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(public http: HttpClient, private _envUrl: EnvironmentUrlService) { }

  public getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this._envUrl.urlAddress + '/api/Item');
  }

  public getAvalibleItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this._envUrl.urlAddress + '/api/Item/avalible');
  }

  public matchAllItems(term:string): Observable<Item[]> {
    return this.http.get<Item[]>(this._envUrl.urlAddress + '/api/Item/search/' + term);
  }

  public getItemById(id: number) {
    return this.http.get<Item>(this._envUrl.urlAddress + '/api/Item/' + id).toPromise();
  }

  public addItem(Item: Item) {
    return this.http.post<Item>(this._envUrl.urlAddress + '/api/Item', Item);
  }

  public editItem(Item: Item) {
    return this.http.put<Item>(this._envUrl.urlAddress + '/api/Item/' + Item.id,  Item);
  }

  public deleteItem(ItemId: number) {
    return this.http.delete(this._envUrl.urlAddress + '/api/Item/' + ItemId);
  }
}
