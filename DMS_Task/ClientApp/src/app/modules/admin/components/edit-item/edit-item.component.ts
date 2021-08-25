import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { ItemService } from 'src/app/shared/services/item.service';
import { UnitOfMeasureServiceService } from 'src/app/shared/services/unit-of-measure-service.service';
import { UnitOfMeasure } from 'src/app/models/unit-of-measure';
import { Item } from 'src/app/models/item';


@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent implements OnInit {

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
    id: null,
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
    private _route: ActivatedRoute,
    private _itemServices: ItemService,
    private toastrService: ToastrService,
    private _unitService: UnitOfMeasureServiceService
  ) { }

  ngOnInit(): void {

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

    this._route.params.subscribe((p) => {
      this._itemServices.getItemById(p.id).then((a) => {
        
        this.item = a;
        console.log(this.item);
        
        this.imageUrl = a.imageUrl;
        this.imageUploaded = this.imageUrl != null;

        this.itemForm.get('name').setValue(this.item.name);
        this.itemForm.get('description').setValue(this.item.description);
        this.itemForm.get('quantity').setValue(this.item.quantity);
        this.itemForm.get('unitPrice').setValue(this.item.unitPrice);
        this.itemForm.get('discount').setValue(this.item.discount);
        this.itemForm.get('unitOfMeasureId').setValue(this.item.unitOfMeasureId);
        this.itemForm.get('tax').setValue(this.item.tax);
        this.itemForm.get('limitPerCustomer').setValue(this.item.limitPerCustomer);
      })});

    this._unitService.getAllUnits().subscribe(a => {
      this.units = a;
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
    

    // TODO: Show Toast => Added Successfully
    this._itemServices.editItem(this.item).subscribe(a => {
      this.toastrService.success('Item Upateded');
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
