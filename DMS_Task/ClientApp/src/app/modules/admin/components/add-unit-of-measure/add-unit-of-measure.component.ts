import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UnitOfMeasureServiceService } from 'src/app/shared/services/unit-of-measure-service.service';
import { UnitOfMeasure } from 'src/app/models/unit-of-measure';

@Component({
  selector: 'app-add-unit-of-measure',
  templateUrl: './add-unit-of-measure.component.html',
  styleUrls: ['./add-unit-of-measure.component.css']
})
export class AddUnitOfMeasureComponent implements OnInit {

  public unitOfMeasureform: FormGroup;
  unitOfMeasure : UnitOfMeasure = {name:'', description:''};
  
  constructor(
    private _router: Router,
    private _unitOfMeasureService: UnitOfMeasureServiceService,
  ) { }

  ngOnInit(): void {
    this.unitOfMeasureform = new FormGroup({
      'name': new FormControl(null, [Validators.required]),
      'description': new FormControl(null, [Validators.required]),
    });
  }

  public addUnit(unitOfMeasureform){
    
    this.unitOfMeasure.name = unitOfMeasureform.value.name;
    this.unitOfMeasure.description = unitOfMeasureform.value.description;
    // TODO: Show Toast => Added Successfully
    this._unitOfMeasureService.addUnit(this.unitOfMeasure).subscribe(a => {
      this._router.navigate(['/admin/unit-of-measure'])
    })

  }

}
