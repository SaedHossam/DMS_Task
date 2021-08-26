import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { AuthenticationService } from "../shared/services/authentication.service";
import { Item } from 'src/app/models/item';
import { ItemService } from 'src/app/shared/services/item.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.css']
})
export class ItemDetailsComponent implements OnInit {

  item: Item = new Item();
  maxQty: number = 1;
  selectedQty: number = 1;
  isLoading: boolean = true;
  selectQty: number[] = [];
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
      this._itemService.getItemById(p.id).then(a => {
        this.item = a;
        this.maxQty = Math.min(this.item.avalibleQuantity, this.item.limitPerCustomer);
        for (let index = 1; index <= this.maxQty; index++) {
          this.selectQty.push(index);      
        }
      });
    });

    // this.selectQty.pop();
    
    // console.log(this.maxQty);
    // console.log(this.selectQty);

  }

  addToCart(id: number) {
    if (!this._authService.isUserAuthenticated()) {
      return this._router.navigate(["/authentication/login"]);
    }
    this._cartService.AddToCart(id, this.selectedQty).subscribe(a => {
      this.toastrService.success('Added Successfully!');
    });

  }

}
