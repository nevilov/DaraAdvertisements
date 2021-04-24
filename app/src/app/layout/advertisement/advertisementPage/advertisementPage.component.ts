import { switchMap, catchError } from 'rxjs/operators';
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

    constructor(
        private route: ActivatedRoute,
        private advertisementService: AdvertisementService) { }

    ngOnInit() {
        this.loadAdvertisements();
    }

    loadAdvertisements() {
        this.advertisementService.getAllAdvertisements().subscribe((data) => {
            this.advertisements = data.items;
            for (let i = 0; i < this.advertisements.length; i++) {
                if (this.advertisements[i].images[0] === undefined) {
                    this.advertisements[i].images[0] = { id: "default" };
                }
            }
            console.log(this.advertisements);
        });
    }

    loadAdvertisementsByCategory(categoryId: number) {

        this.advertisementService.getAdvertisementsByCategoryId(categoryId)
            .subscribe(data => {
                this.advertisements = data.items;
                console.log(this.advertisements)
                for (let i = 0; i < this.advertisements.length; i++) {
                    if (this.advertisements[i].images[0] === undefined) {
                        this.advertisements[i].images[0] = { id: "default" };
                    }
                }
            });
    }
}
