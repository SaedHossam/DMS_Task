import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Item } from 'src/app/models/item';
import { ItemService } from 'src/app/shared/services/item.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {

  page = 1;
  pageSize = 5;
  collectionSize = 0;
  allItems: Item[] = [];
  items: Item[];
  requestDetails: Item;
  requestId: number;

  constructor(private _router: Router, private _companyRequestsService: ItemService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    this._companyRequestsService.getAllItems().subscribe(a => {
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

  open(content) {
    this._companyRequestsService.getItemById(this.requestId).then(r => {
      
      this.requestDetails = r;
    });

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {

    }, (reason) => {

    });
  }


  editItem(id:number){
    this._router.navigate(['/admin/edit-item/' + id]);
  }

  deleteItem(id:number){
    this._companyRequestsService.deleteItem(id).subscribe(a => {
      this.loadItems();
    });
  }

}
