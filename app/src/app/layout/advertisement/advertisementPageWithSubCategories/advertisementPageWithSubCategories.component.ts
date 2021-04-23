import { AdvertisementService } from 'src/app/services/advertisements.service';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from 'src/app/Dtos/advertisement';

@Component({
    selector: 'app-advertisementPageWithSubCategories',
    templateUrl: './advertisementPageWithSubCategories.component.html',
    styleUrls: ['./advertisementPageWithSubCategories.component.scss'],
    providers: [AdvertisementService]
})
export class AdvertisementPageWithSubCategoriesComponent implements OnInit {
    advertisements: Advertisement[] = [];

    constructor(private advertisementService: AdvertisementService) { }

    ngOnInit() {
        this.loaAdvertisements();    // загрузка данных при старте компонента
    }

    // получаем данные через сервис
    loaAdvertisements() {
        this.advertisementService.getAllAdvertisements()
            .subscribe(data => {
                this.advertisements = data.items;
                console.log(this.advertisements)
            });
    }
}
