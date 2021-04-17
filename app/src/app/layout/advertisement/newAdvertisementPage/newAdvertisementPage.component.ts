import { CookieService } from 'ngx-cookie-service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NewAdvertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
    selector: 'app-newAdvertisementPage',
    templateUrl: './newAdvertisementPage.component.html',
    styleUrls: ['./newAdvertisementPage.component.scss']
})
export class NewAdvertisementPageComponent implements OnInit {

    advertisementForm = new FormGroup({
        title: new FormControl('', [
            Validators.required,
            Validators.minLength(5)
        ]),
        description: new FormControl('', [
            Validators.required,
            Validators.minLength(5)
        ]),
        price: new FormControl('', [
            Validators.required
        ]),
        cover: new FormControl('', [
            Validators.required
        ]),
        categoryId: new FormControl('', [
            Validators.required
        ])
    });

    constructor(
        private advertisementService: AdvertisementService,
        private cookieService: CookieService,
        private router: Router) { }

    onSubmit() {
        console.log("Advertisement form info", this.advertisementForm.value);

        const advertisementToSend: NewAdvertisement = {
            title: this.advertisementForm.value.title,
            description: this.advertisementForm.value.description,
            price: this.advertisementForm.value.price,
            cover: this.advertisementForm.value.cover,
            categoryId: this.advertisementForm.value.categoryId
        };

        this.advertisementService.createAdvertisement(advertisementToSend)
            .pipe(untilDestroyed(this))
            .subscribe((r) => {
                this.router.navigateByUrl('/advertisements/' + this.cookieService.get('LatestRedirectId'));
            });
    }

    ngOnInit() {
    }
}
