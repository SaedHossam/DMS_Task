import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UnitOfMeasureServiceService } from 'src/app/shared/services/unit-of-measure-service.service';
import { UnitOfMeasure } from 'src/app/models/unit-of-measure';
@Component({
  selector: 'app-edit-unit-of-measure',
  templateUrl: './edit-unit-of-measure.component.html',
  styleUrls: ['./edit-unit-of-measure.component.css']
})
export class EditUnitOfMeasureComponent implements OnInit {

  public unitOfMeasureform: FormGroup;
  unitOfMeasure : UnitOfMeasure = {name:'', description:''};
  
  constructor(
    private _router: Router,
    private _unitOfMeasureService: UnitOfMeasureServiceService,
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.unitOfMeasureform = new FormGroup({
      'name': new FormControl(null, [Validators.required]),
      'description': new FormControl(null, [Validators.required]),
    });


    this._route.params.subscribe((p) => {
      this._unitOfMeasureService.getUnitById(p.id).then((a) => {
        this.unitOfMeasure = a;
        this.unitOfMeasureform.get('name').setValue(this.unitOfMeasure.name);
        this.unitOfMeasureform.get('description').setValue(this.unitOfMeasure.description);
      })});

  }



  public editUnit(unitOfMeasureform){
    
    this.unitOfMeasure.name = unitOfMeasureform.value.name;
    this.unitOfMeasure.description = unitOfMeasureform.value.description;
    // TODO: Show Toast => Added Successfully
    this._unitOfMeasureService.editUnit(this.unitOfMeasure).subscribe(a => {
      this._router.navigate(['/admin/unit-of-measure'])
    });
  }
}
