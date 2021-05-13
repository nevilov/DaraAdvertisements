import { EventEmitter, Input, Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import {
  debounceTime,
  map,
  filter,
  distinctUntilChanged,
} from 'rxjs/operators';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  @Input() placeholder: string = '';
  @Output() setValue: EventEmitter<string> = new EventEmitter<string>();

  private searchString = new Subject<string>();

  search(inputValue: string): void {
    this.searchString.next(inputValue);
  }

  constructor() {}

  ngOnInit(): void {
    this.searchString
      .pipe(
        untilDestroyed(this),
        debounceTime(1000),
        map((value) => value.trim()),
        filter((value) => value.length >= 3 || value == ''),
        distinctUntilChanged()
      )
      .subscribe((searchValue: string) => this.setValue.emit(searchValue));
  }
}
