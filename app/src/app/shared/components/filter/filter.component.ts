import { formatDate } from '@angular/common';
import { EventEmitter } from '@angular/core';
import { Component, OnInit, Output } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';

import { AdvertisementService } from 'src/app/services/advertisements.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {
  isVisible: boolean = false;
  currentDate: string = formatDate(new Date(), 'yyyy-MM-dd', 'en');

  @Output() filterParams = new EventEmitter<object>();

  // rangeValidator: ValidatorFn = (
  //   control: AbstractControl
  // ): ValidationErrors | null => {
  //   const min = control.get('min')?.value;
  //   const max = control.get('max')?.value;

  //   return max >= min ? { isValid: true } : null;
  // };

  filtersForm = new FormGroup({
    minPrice: new FormControl(),
    maxPrice: new FormControl(),
    minDate: new FormControl(),
    maxDate: new FormControl(),
  });

  constructor() {}

  ngOnInit(): void {}

  onSubmit() {
    this.filterParams.emit(this.filtersForm.value);
  }

  toggleFiltersMenu() {
    this.isVisible = !this.isVisible;
  }
}
