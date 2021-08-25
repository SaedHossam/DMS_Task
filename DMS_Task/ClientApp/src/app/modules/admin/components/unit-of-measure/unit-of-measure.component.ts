import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UnitOfMeasure } from 'src/app/models/unit-of-measure';
import { UnitOfMeasureServiceService } from 'src/app/shared/services/unit-of-measure-service.service';

@Component({
  selector: 'app-unit-of-measure',
  templateUrl: './unit-of-measure.component.html',
  styleUrls: ['./unit-of-measure.component.css']
})

export class UnitOfMeasureComponent implements OnInit {

  page = 1;
  pageSize = 5;
  collectionSize = 0;
  allRequests: UnitOfMeasure[] = [];
  requests: UnitOfMeasure[];
  requestDetails: UnitOfMeasure;
  requestId: number;
  
  constructor(private _router: Router, private _companyRequestsService: UnitOfMeasureServiceService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadComapiesRequestes();
  }


  loadComapiesRequestes() {
    this._companyRequestsService.getAllUnits().subscribe(a => {
      this.allRequests = a;
      this.collectionSize = this.allRequests.length;

      this.refreshCompanies();
    });
  }

  refreshCompanies() {
    this.requests = this.allRequests
      .map((company, i) => ({ number: i + 1, ...company }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  open(content) {
    this._companyRequestsService.getUnitById(this.requestId).then(r => {
      this.requestDetails = r;
    });

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {

    }, (reason) => {

    });
  }


  editUnit(id:number){
    this._router.navigate(['/admin/edit-unit-of-measure/' + id]);
  }

  deleteUnit(id:number){
    this._companyRequestsService.deleteUnit(id).subscribe(a => {
      this.loadComapiesRequestes();
    });
  }

}
