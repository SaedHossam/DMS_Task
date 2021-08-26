import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocialAuthService } from "angularx-social-login";
import { ToastrService } from 'ngx-toastr';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { AuthenticationService } from "../shared/services/authentication.service";
import { Employee } from "../models/employee";


@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  public isUserAuthenticated: boolean;
  public isExternalAuth: boolean;
  public isMenuCollapsed: boolean = true;
  public isUserAdmin: boolean = false;
  public isUserEmployee: boolean = false;
  public isUserCompany: boolean = false;
  Emp: Employee = new Employee();

  public searchForm: FormGroup;
  public term: string;
  constructor(
    private _authService: AuthenticationService, 
    private _router: Router, 
    private _socialAuthService: SocialAuthService,
    private toastrService: ToastrService,
    ) {
    this._authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  }


  ngOnInit(): void {
    this._authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;

        this.isUserAdmin = this._authService.isUserAdmin();
        this.isUserEmployee = this._authService.isUserEmployee();
        this.isUserCompany = this._authService.isUserCompany();
      });
   

    this._socialAuthService.authState.subscribe(user => {
      this.isExternalAuth = user != null;
    });

    this.isUserAdmin = this._authService.isUserAdmin();
    this.isUserEmployee = this._authService.isUserEmployee();
    this.isUserCompany = this._authService.isUserCompany();

    this.searchForm = new FormGroup({
      'term': new FormControl(null, [Validators.required])
    });
  }

  searchItem(searchForm){
    this.term = searchForm.value.term;
    this._router.navigate(["/search-items/" + this.term]);
  }
  public logout = () => {
    this._authService.logout();

    if (this.isExternalAuth)
      this._authService.signOutExternal();

      this.toastrService.success('Logged out!');
    this._router.navigate(["/authentication/login"]);
  }
}
