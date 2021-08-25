import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ItemService } from 'src/app/shared/services/item.service';
import { UnitOfMeasureServiceService } from 'src/app/shared/services/unit-of-measure-service.service';
import { UnitOfMeasure } from 'src/app/models/unit-of-measure';
import { Item } from 'src/app/models/item';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {

  imageUpload = new FormGroup({
    imageFile: new FormControl('')
  });
  units: UnitOfMeasure[];
  profilePhotoSizeError: boolean = false;
  profilePhotoTypeError: boolean = false;
  imageUrl: string = null;
  imageUploaded: boolean = false;
  progress: number =100;
  public itemForm: FormGroup;
  item: Item = {
    name: '',
    description: '',
    unitOfMeasureId: 0,
    unitOfMeasure: '',
    imageUrl: '',
    quantity: 0,
    avalibleQuantity: 0,
    unitPrice: 0,
    discount: 0,
    tax: 0,
    limitPerCustomer: 0
  };

  constructor(
    private _router: Router,
    private _itemServices: ItemService,
    private toastrService: ToastrService,
    private _unitService: UnitOfMeasureServiceService
  ) { }

  ngOnInit(): void {
    this._unitService.getAllUnits().subscribe(a => {
      this.units = a;
    });

    this.itemForm = new FormGroup({
      'name': new FormControl(null, [Validators.required]),
      'description': new FormControl(null, [Validators.required]),
      'quantity': new FormControl(null, [Validators.required, Validators.min(0)]),
      'unitPrice': new FormControl(null, [Validators.required, Validators.min(0)]),
      'discount': new FormControl(null, [Validators.required, Validators.min(0), Validators.max(100)]),
      'unitOfMeasureId': new FormControl(null, [Validators.required]),
      'tax': new FormControl(null, [Validators.required, Validators.min(0), Validators.max(100)]),
      'limitPerCustomer': new FormControl(null, [Validators.required, , Validators.min(0)]),
    });

  }

  public addItem(itemForm){
    
    this.item.name = itemForm.value.name;
    this.item.description = itemForm.value.description;
    this.item.quantity = itemForm.value.quantity;
    this.item.unitPrice = itemForm.value.unitPrice;
    this.item.discount = itemForm.value.discount;
    this.item.unitOfMeasureId = itemForm.value.unitOfMeasureId;
    this.item.tax = itemForm.value.tax;
    this.item.limitPerCustomer = itemForm.value.limitPerCustomer;
    //this.item.imageUrl = itemForm.value.imageUrl;
    

    // TODO: Show Toast => Added Successfully
    this._itemServices.addItem(this.item).subscribe(a => {
      this._router.navigate(['/admin/items'])
    })

  }



  onUpload(event) {
    this.toastrService.success('File Uploaded');

    
    this.item.imageUrl = event['originalEvent'].body.dbPath;
    this.imageUrl = this.item.imageUrl;
    this.imageUploaded = true;
    console.log(this.imageUrl);

    //this.loadEmployeeData();
  }

  onProgress(event) {
    this.progress = event.progress;
  }
}
