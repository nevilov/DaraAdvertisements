import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-advertisement',
    templateUrl: './advertisement.component.html',
    styleUrls: ['./advertisement.component.scss'],
})
export class AdvertisementComponent implements OnInit {
    @Input() title: string = '';
    @Input() price: number = 0;
    @Input() ownerName: string = 'Доменный Юзер';
    @Input() ownerLogin: string = 'ДоменныйЮзер';
    @Input() ownerId: string = ""; // Решение на скорую руку TODO Исправить
    @Input() createdDate: Date = new Date();
    @Input() routLink: number = 0;
    @Input() thisUserAdvertisement: boolean = false;

    constructor() { }

    ngOnInit() { }
}
