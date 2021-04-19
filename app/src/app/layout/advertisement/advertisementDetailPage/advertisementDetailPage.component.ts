import { CookieService } from 'ngx-cookie-service';
import { UserService } from './../../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';

@Component({
    selector: 'app-advertisementDetailPage',
    templateUrl: './advertisementDetailPage.component.html',
    styleUrls: ['./advertisementDetailPage.component.scss'],
    providers: [
        AdvertisementService
    ]
})
export class AdvertisementDetailPageComponent implements OnInit {

    id: number = 0;
    userId: number = 0;
    advertisement: Advertisement | null = null;
    sameAdvertisements: Advertisement[] | null = null;
    userAdvertisements: Advertisement[] | null = null;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private advertisementService: AdvertisementService,
        private userService: UserService) {
    }

    ngOnInit() {

        this.router.events.subscribe((event) => {
            console.log('route changed');
            // this.ngOnInit();
        });

        this.route.paramMap.pipe(
            switchMap(params => params.getAll('id'))
        )
            .subscribe(data => this.id = +data);

        this.advertisementService.getAdvertisementById(this.id)
            .subscribe((data: Advertisement) => {
                this.advertisement = data;

                this.userService.getUserAdvertisementsWithLimit(this.advertisement.owner.id, 4, 0).subscribe((data) => {
                    this.userAdvertisements = data.items;
                    console.log(data);
                });
            });

        // this.advertisementService.getAllAdvertisements()
        //     .subscribe((data: Advertisement) => {
        //         this.advertisement = data;
        //         console.log(data)
        //     });
    }
}
