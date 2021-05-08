import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SortItem } from 'src/app/Dtos/sorting';

@Component({
  selector: 'app-sort',
  templateUrl: './sort.component.html',
  styleUrls: ['./sort.component.scss'],
})
export class SortComponent implements OnInit {
  @Input() sotringElements: SortItem[] = [];
  @Output() setValue: EventEmitter<string> = new EventEmitter<string>();

  onSort(inputValue: string) {
    this.setValue.emit(inputValue);
  }

  constructor() {}

  ngOnInit(): void {}
}
