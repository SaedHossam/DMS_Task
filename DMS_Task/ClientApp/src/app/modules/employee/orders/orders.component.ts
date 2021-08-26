import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { AuthenticationService } from "../../../shared/services/authentication.service";
import { Order } from 'src/app/models/order';
import { OrderService } from 'src/app/shared/services/order.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  page = 1;
  pageSize = 5;
  collectionSize = 0;
  allOrders: Order[] = [];
  orders: Order[];
  fullName:string;
  email:string;
  constructor(
    private _authService: AuthenticationService,
    private _router: Router,
    private _orderService: OrderService,
    private toastrService: ToastrService,
  ) { }

  ngOnInit(): void {
    if (this._authService.isUserAdmin()) {
      this._router.navigate(["/admin"]);
    }
    if (!this._authService.isUserAuthenticated()) {
      this._router.navigate(["/authentication/login"]);
    }
    this.loadOrders();
  }


  loadOrders() {
    this._orderService.getMyOrders().subscribe(a => {
      this.allOrders = a;
      this.collectionSize = this.allOrders.length;
      this.fullName = this.allOrders[0].fullName;
      this.email = this.allOrders[0].email;
      this.refreshOrders();
    });
  }

  refreshOrders() {
    this.orders = this.allOrders
      .map((order, i) => ({ number: i + 1, ...order }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

}
