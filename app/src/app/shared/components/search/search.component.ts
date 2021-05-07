import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import {
  debounceTime,
  map,
  filter,
  distinctUntilChanged,
} from 'rxjs/operators';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  searchString = new Subject<string>();

  search(inputValue: string): void {
    this.searchString.next(inputValue);
  }

  constructor() {}

  ngOnInit(): void {
    this.searchString.pipe(
      debounceTime(1000),
      map((value) => value.trim()),
      filter((value) => value.length >= 3 || value == ''),
      distinctUntilChanged()
    );
  }
}
