import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { UnitOfMeasure } from 'src/app/models/unit-of-measure';

import { EnvironmentUrlService } from './environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class UnitOfMeasureServiceService {

  constructor(public http: HttpClient, private _envUrl: EnvironmentUrlService) { }

  public getAllUnits(): Observable<UnitOfMeasure[]> {
    return this.http.get<UnitOfMeasure[]>(this._envUrl.urlAddress + '/api/UOM');
  }

  public getUnitById(id: number) {
    return this.http.get<UnitOfMeasure>(this._envUrl.urlAddress + '/api/UOM/' + id).toPromise();
  }

  public addUnit(unit: UnitOfMeasure) {
    return this.http.post<UnitOfMeasure>(this._envUrl.urlAddress + '/api/UOM', unit);
  }

  public editUnit(unit: UnitOfMeasure) {
    return this.http.put<UnitOfMeasure>(this._envUrl.urlAddress + '/api/UOM/' + unit.id,  unit);
  }

  public deleteUnit(unitId: number) {
    return this.http.delete(this._envUrl.urlAddress + '/api/UOM/' + unitId);
  }
}
