
import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "../shared/services/authentication.service";
import { Router } from "@angular/router";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  constructor(private _authService: AuthenticationService, private _router: Router) { }

  ngOnInit(): void {
    // Get All Items
    if (this._authService.isUserAdmin()) {
      this._router.navigate(["/admin"]);
    }
  }

  // Add to Cart Method
  // if user role = customer =>{
  // add(itemId, qty = 1) then show success message
  // and in add function we check if item exists in cart
  // then update qty
  // else add to cart
  // }
  // else if user role = admin => navigate to /admin/home
  // else navigate to /login

  // Search Method
  // get input data
  // call items/search/?term=data

  // get Item Details(itemId: Number)
  // handled via routerlink /item/:id

  ///////////////////get item by id////////////////////////////
  // onInt => getItem(id)
  // add to cart => add(itemId, qty)

}
