import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private _http: HttpClient, private _envUrl: EnvironmentUrlService) { }

  public getApplications = () => {
    return;
  }

  public withdrawApplication = (applicationId: number) => {
    return;
  };

  // TODO: update api
  public archiveApplication = (applicationId: number, archive: boolean) => {
    return;
  };
}
