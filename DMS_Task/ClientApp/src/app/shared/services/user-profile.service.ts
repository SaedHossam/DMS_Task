import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';

import { Employee } from '../../models/employee';

import { UpdateEmployeeDto } from '../../models/update-emplyee-dto';

import { EnvironmentUrlService } from './environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  constructor(public http: HttpClient, private _envUrl: EnvironmentUrlService) { }

  public getEmpById(id: number) {
    return this.http.get<Employee>(this._envUrl.urlAddress + '/api/Employees/' + id);
  }

  public getMyProfileData():Observable<Employee> {
    return this.http.get<Employee>(this._envUrl.urlAddress + '/api/Employees/me');
  }

  public editEmpProfile(empData: UpdateEmployeeDto) {
    return this.http.put(this._envUrl.urlAddress + '/api/Employees/UpdateEmployee/', empData);
  }

}
