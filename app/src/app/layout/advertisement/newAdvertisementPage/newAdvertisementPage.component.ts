import { CookieService } from 'ngx-cookie-service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NewAdvertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ImageService } from 'src/app/services/image.service';
import { tap } from 'rxjs/operators';
import { concat, Observable } from 'rxjs';

@UntilDestroy()
@Component({
    selector: 'app-newAdvertisementPage',
    templateUrl: './newAdvertisementPage.component.html',
    styleUrls: ['./newAdvertisementPage.component.scss']
})
export class NewAdvertisementPageComponent implements OnInit {

    filesToUpload: File[] = [];
    imagePreviews: any[] = [];
    fileToUpload: File = {} as File;
    creationFormState: string;
    newId: string;

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
        categoryId: new FormControl('', [
            Validators.required
        ])
    });

    constructor(
        private advertisementService: AdvertisementService,
        private cookieService: CookieService,
        private router: Router,
        private imageService: ImageService
    ) {
        this.creationFormState = 'Создать объявление';
        this.filesToUpload.push({} as File);
        this.newId = '0';
        console.log(this.filesToUpload);
    }

    newFile() {
        this.filesToUpload.push({} as File);
        console.log(this.filesToUpload);
    }

    removeFile(i: number) {
        if (this.filesToUpload.length > 1) {
            if (i > -1) {
                this.filesToUpload.splice(i, 1);
                this.imagePreviews.splice(i, 1);
            }
        }
        console.log(this.filesToUpload);
    }

    onFileChange(event: any, i: number) {
        var reader = new FileReader();
        if (event.target.files.length > 0) {
            this.filesToUpload[i] = event.target.files[0];
            reader.readAsDataURL(this.filesToUpload[i]);
            reader.onload = (_event) => {
                this.imagePreviews[i] = reader.result;
            }
        }
        console.log(i);
    }

    onSubmit() {
        console.log("Advertisement form info", this.advertisementForm.value);
        this.creationFormState = 'Создание объявления... подождите..';
        const advertisementToSend: NewAdvertisement = {
            title: this.advertisementForm.value.title,
            description: this.advertisementForm.value.description,
            price: this.advertisementForm.value.price,
            cover: "true",
            categoryId: this.advertisementForm.value.categoryId
        };

        this.advertisementService.createAdvertisement(advertisementToSend)
            .pipe(untilDestroyed(this))
            .subscribe((r) => {
                this.newId = this.cookieService.get('LatestRedirectId');
                this.creationFormState = 'Загрузка файлов к новому объявлению №' + this.newId + '... подождите..';
                let fileIteratorIndex = 1;
                const observables = this.filesToUpload.map(entry => {
                    return this.fetchSingleFile(this.newId, entry);
                });
                concat(...observables).subscribe(singleMedia => {
                    fileIteratorIndex += 1;
                    this.creationFormState = 'Загружено..' + fileIteratorIndex + '/' + this.filesToUpload.length + 'файлов.. подождите';
                    console.log("concatted" + singleMedia);
                    if (fileIteratorIndex > this.filesToUpload.length) {
                        this.creationFormState = 'Файлы загружены... переход на страницу объявления..';
                        this.router.navigateByUrl('/advertisements/' + this.newId);
                    }
                },
                    error => {
                    });
            });
    }

    fetchSingleFile(newId: string, entry: File): Observable<any> {
        return this.imageService.sendAdvertisementImage({
            id: Number(newId),
            image: entry
        }).pipe(
            tap(mediaEntry => console.log(mediaEntry))
        );
    }

    ngOnInit() {
    }
}
