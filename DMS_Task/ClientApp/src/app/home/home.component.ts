import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "../shared/services/authentication.service";
import { Item } from 'src/app/models/item';
import { ItemService } from 'src/app/shared/services/item.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  page = 1;
  pageSize = 5;
  collectionSize = 0;
  allItems: Item[] = [];
  items: Item[];

  constructor(private _authService: AuthenticationService, private _router: Router, private _itemService: ItemService) { }

  ngOnInit(): void {
    // Get All Items
    if (this._authService.isUserAdmin()) {
      this._router.navigate(["/admin"]);
    }

    this.loadItems();
  }


  loadItems() {
    this._itemService.getAvalibleItems().subscribe(a => {
      this.allItems = a;
      this.collectionSize = this.allItems.length;

      this.refreshItems();
    });
  }

  refreshItems() {
    this.items = this.allItems
      .map((company, i) => ({ number: i + 1, ...company }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  addToCart(id:number){
    if (!this._authService.isUserAuthenticated()) {
      return this._router.navigate(["/authentication/login"]);
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
