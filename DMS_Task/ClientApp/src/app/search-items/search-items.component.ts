import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { AuthenticationService } from "../shared/services/authentication.service";
import { Item } from 'src/app/models/item';
import { ItemService } from 'src/app/shared/services/item.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-search-items',
  templateUrl: './search-items.component.html',
  styleUrls: ['./search-items.component.css']
})
export class SearchItemsComponent implements OnInit {

  page = 1;
  pageSize = 5;
  collectionSize = 0;
  allItems: Item[] = [];
  items: Item[];
  term: string;
  constructor(
    private _authService: AuthenticationService,
    private _router: Router,
    private _route: ActivatedRoute,
    private _itemService: ItemService,
    private _cartService: CartService,
    private toastrService: ToastrService,
  ) { }


  ngOnInit(): void {

    if (this._authService.isUserAdmin()) {
      this._router.navigate(["/admin"]);
    }

    this._route.params.subscribe((p) => {
      this._itemService.matchAllItems(p.term).subscribe(a => {
        this.allItems = a;
      this.collectionSize = this.allItems.length;

      this.refreshItems();
      });
    })
  }


  refreshItems() {
    this.items = this.allItems
      .map((item, i) => ({ number: i + 1, ...item }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  addToCart(id: number) {
    if (!this._authService.isUserAuthenticated()) {
      return this._router.navigate(["/authentication/login"]);
    }
    this._cartService.AddToCart(id, 1).subscribe(a => {
      this.toastrService.success('Added Successfully!');
    });

  }
  
  itemDetails(id: number) {
    this._router.navigate(['/item/' + id]);
    // this._itemService.getItemById(id).then(a => {
    // });
  }

}
