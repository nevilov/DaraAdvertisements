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
import { ThrowStmt } from '@angular/compiler';

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
  hasFiles: boolean;

  isButtonEnabled: boolean;

  creationFormState: string;
  newId: string;
  isCategoryVisible: boolean;
  newCategoryId: number;
  selectedCategory: string;
  categories: string[] = ["Транспорт", "Недвижимость", "Бытовая техника", "Животные"];

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
      this.hasFiles = false;
      this.isButtonEnabled = true;

      this.newCategoryId = 0;
      this.selectedCategory = "Выберите категорию"
      this.isCategoryVisible = false;
      console.log(this.filesToUpload);
    }
  
    categorySelectorClicked() {
      this.isCategoryVisible = !this.isCategoryVisible;
    }

    onCategorySelected(newText:string, newCat: number) {
      console.log(newCat);
      this.newCategoryId = newCat + 1;
      this.selectedCategory = newText;
      this.isCategoryVisible = false;
    }

    newFile() {
        if (this.filesToUpload.length < 5) {
          this.filesToUpload.push({} as File);
        }
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
      this.hasFiles = true;
      var reader = new FileReader();
      if (event.target.files.length > 0) {
        this.filesToUpload[i] = event.target.files[0];
        reader.readAsDataURL(this.filesToUpload[i]);
        reader.onload = (_event) => {
          this.imagePreviews[i] = reader.result;
        }
        console.log(i);
      }
    }

    onSubmit() {
      if (this.isButtonEnabled) {
      this.isButtonEnabled = false;

      console.log("Advertisement form info", this.advertisementForm.value);
      this.creationFormState = 'Создание объявления... подождите..';
      const advertisementToSend: NewAdvertisement = {
        title: this.advertisementForm.value.title,
        description: this.advertisementForm.value.description,
        price: this.advertisementForm.value.price,
        cover: "true",
        categoryId: this.newCategoryId
      };
      
      this.advertisementService.createAdvertisement(advertisementToSend)
      .pipe(untilDestroyed(this))
      .subscribe(
        res => {
          console.log("Received" + res);
           this.newId = this.cookieService.get('LatestRedirectId');
           if (this.hasFiles) {
           this.creationFormState = 'Загрузка файлов к новому объявлению №' + this.newId + '... подождите..';
            let fileIteratorIndex = 1;
           const observables = this.filesToUpload.map(entry => {
             return this.fetchSingleFile(this.newId, entry);
            });
            concat(...observables).subscribe(singleMedia => {
              fileIteratorIndex += 1;
              this.creationFormState = 'Загружено..'+ fileIteratorIndex + '/' + this.filesToUpload.length + 'файлов.. подождите';
              console.log("concatted" + singleMedia);
              if (fileIteratorIndex > this.filesToUpload.length) {
                  this.creationFormState = 'Файлы загружены... переход на страницу объявления..';
                  this.router.navigateByUrl('/advertisement/' + this.newId);
              }
              },
              error => {
              }); 
            } else {
              this.creationFormState = 'Информация загружена... переход на страницу объявления..';
              this.router.navigateByUrl('/advertisement/' + this.newId);
            }


        },
        error => {
          this.isButtonEnabled = true;
          this.creationFormState = "Произошла ошибка. Попробовать снова?"
          console.log(error)
      });
    }
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
