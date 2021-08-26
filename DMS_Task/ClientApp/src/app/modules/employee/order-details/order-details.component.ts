import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { AuthenticationService } from "../../../shared/services/authentication.service";
import { Order } from 'src/app/models/order';
import { OrderService } from 'src/app/shared/services/order.service';
import { ToastrService } from 'ngx-toastr';
import { jsPDF } from 'jspdf';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  order: Order;
  @ViewChild('o', {static:false}) el!:ElementRef;
  constructor(
    private _authService: AuthenticationService,
    private _router: Router,
    private _route: ActivatedRoute,
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
    this._route.params.subscribe(a => {
      this.loadOrder(a.id);
    })

  }

  loadOrder(id:number) {
    this._orderService.getOrderById(id).then(a => {
      this.order = a;
    });
  }

  printOrder(){
    var pdf = new jsPDF('p', 'pt', 'a2');
    pdf.html(this.el.nativeElement, {
      callback: (pdf) => {
        pdf.save(this.order.id + ".pdf")
      }
    })
  }

}
