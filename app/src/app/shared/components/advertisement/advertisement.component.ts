import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-advertisement',
  templateUrl: './advertisement.component.html',
  styleUrls: ['./advertisement.component.scss']
})
export class AdvertisementComponent implements OnInit {

  @Input() title: string = "";
  @Input() price: number = 0;
  @Input() ownerName: string = "";

  constructor() { }

  ngOnInit() {
  }

}
