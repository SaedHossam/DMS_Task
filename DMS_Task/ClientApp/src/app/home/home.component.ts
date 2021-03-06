import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "../shared/services/authentication.service";
import { Item } from 'src/app/models/item';
import { ItemService } from 'src/app/shared/services/item.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { ToastrService } from 'ngx-toastr';


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

  constructor(
    private _authService: AuthenticationService, 
    private _router: Router, 
    private _itemService: ItemService,
    private _cartService: CartService,
    private toastrService: ToastrService,
    ) { }

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
      .map((item, i) => ({ number: i + 1, ...item }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  addToCart(id:number){
    if (!this._authService.isUserAuthenticated()) {
      return this._router.navigate(["/authentication/login"]);
    }
    this._cartService.AddToCart(id, 1).subscribe(a => {
      this.toastrService.success('Added Successfully!');
    });
    
  }
  searchItem(term:string){
    this._router.navigate(['/search-items/' + term]);
    // this._itemService.matchAllItems(term).subscribe(a => {
    // })
  }
  itemDetails(id:number){
    this._router.navigate(['/item/' + id]);
  }

}
