import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { ToastrService } from 'ngx-toastr';
import { Cart } from '../models/cart';
import { AuthenticationService } from "../shared/services/authentication.service";
import { ItemService } from 'src/app/shared/services/item.service';
import { CartService } from 'src/app/shared/services/cart.service';
import { OrderService } from 'src/app/shared/services/order.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cart: Cart = new Cart();
  totalPrice: number = 0;
  totalDiscount: number = 0;
  totalTax: number = 0;
  finalPrice: number = 0;
  constructor(
    private _authService: AuthenticationService,
    private _router: Router,
    private _orderService: OrderService,
    private _cartService: CartService,
    private toastrService: ToastrService,
  ) { }

  ngOnInit(): void {
    if (!this._authService.isUserAuthenticated()) {
      this._router.navigate(["/authentication/login"]);
    }
    if (this._authService.isUserAdmin()) {
      this._router.navigate(["/admin/"]);
    }

    this._cartService.getCartItems().subscribe(a => {
      this.loadCartData(a);
    });
  }

  loadCartData(a) {
    this.cart = a;

    this.totalPrice = 0;
    this.totalDiscount = 0;
    this.totalTax = 0;
    this.finalPrice = 0;

    for (let index = 0; index < this.cart.shoppingCartItems.length; index++) {
      this.totalPrice +=
        (this.cart.shoppingCartItems[index].unitPrice * this.cart.shoppingCartItems[index].orderedQuantity);
      this.totalDiscount += this.cart.shoppingCartItems[index].discount;
      this.totalTax += this.cart.shoppingCartItems[index].tax;
      this.finalPrice +=
        (this.cart.shoppingCartItems[index].unitPrice * this.cart.shoppingCartItems[index].orderedQuantity) -
        this.cart.shoppingCartItems[index].discount +
        this.cart.shoppingCartItems[index].tax;
    }
  }

  deleteCartItem(itemId: number) {
    this.toastrService.error("Deleted!");
    this._cartService.DeleteFromCart(itemId).subscribe(a => {
      this.loadCartData(a);
    })
  }

  checkout() { 
    this._orderService.addOrder().subscribe(a => {
      this.toastrService.success("Order Confirmed!\nOrder Id: #" + a);
      this._router.navigate(['employee/orders']);
    });
  }

}
