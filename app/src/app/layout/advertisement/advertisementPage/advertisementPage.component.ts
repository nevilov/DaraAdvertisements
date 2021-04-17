import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';

@Component({
    selector: 'app-advertisementPage',
    templateUrl: './advertisementPage.component.html',
    styleUrls: ['./../../../../assets/scss/pages/__home.scss'],
    providers: [
        AdvertisementService
    ]
})
export class AdvertisementPageComponent implements OnInit {
    advertisements: Advertisement[] = [];
    categoryId: number = -1;

    constructor(private route: ActivatedRoute, private advertisementService: AdvertisementService) { }

    ngOnInit() {
        this.route.paramMap.pipe(
            switchMap(params => params.getAll('id'))
        )
            .subscribe(data => this.categoryId = +data);

        if (this.categoryId == -1) {
            this.loaAdvertisements();
        } else {
            this.loaAdvertisementsByCategory(this.categoryId);
        }
    }

    loaAdvertisements() {
        this.advertisementService.getAllAdvertisements()
            .subscribe(data => {
                this.advertisements = data.items;
                console.log(this.advertisements)
            });
    }

    loaAdvertisementsByCategory(categoryId: number) {
        this.advertisementService.getAdvertisementByCategoryId(categoryId)
            .subscribe(data => {
                this.advertisements = data.items;
                console.log(this.advertisements)
            });
    }
}
