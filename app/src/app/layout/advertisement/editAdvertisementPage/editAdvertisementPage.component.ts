import { CookieService } from 'ngx-cookie-service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Advertisement, NewAdvertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ImageService } from 'src/app/services/image.service';
import { switchMap, tap } from 'rxjs/operators';
import { concat, Observable } from 'rxjs';

@UntilDestroy()
@Component({
  selector: 'app-editAdvertisementPage',
  templateUrl: './editAdvertisementPage.component.html',
  styleUrls: ['./editAdvertisementPage.component.scss'],
})
export class EditAdvertisementPageComponent implements OnInit {
  id: number = 0;
  advertisement: Advertisement;

  filesToUpload: File[] = [];
  imagePreviews: any[] = [];
  fileToUpload: File = {} as File;
  hasFilesChanged: boolean;

  isButtonEnabled: boolean;

  creationFormState: string;
  isCategoryVisible: boolean;
  newCategoryId: number;
  selectedCategory: string;
  categories: string[] = [
    'Транспорт',
    'Недвижимость',
    'Бытовая техника',
    'Животные',
  ];

  advertisementForm = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.minLength(5)]),
    description: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
    ]),
    price: new FormControl('', [Validators.required]),
  });

  constructor(
    private advertisementService: AdvertisementService,
    private route: ActivatedRoute,
    private router: Router,
    private imageService: ImageService
  ) {
    this.advertisement = {} as Advertisement;

    this.creationFormState = 'Изменить объявление';
    this.filesToUpload.push({} as File);
    this.hasFilesChanged = false;
    this.isButtonEnabled = true;

    this.newCategoryId = 0;
    this.selectedCategory = 'Выберите категорию';
    this.isCategoryVisible = false;
  }

  categorySelectorClicked() {
    this.isCategoryVisible = !this.isCategoryVisible;
  }

  onCategorySelected(newText: string, newCat: number) {
    this.newCategoryId = newCat + 1;
    this.selectedCategory = newText;
    this.isCategoryVisible = false;
  }

  newFile() {
    this.hasFilesChanged = true;
    if (this.filesToUpload.length < 5) {
      this.filesToUpload.push({} as File);
    }
  }

  removeFile(i: number) {
    this.hasFilesChanged = true;
    if (this.filesToUpload.length > 1) {
      if (i > -1) {
        this.filesToUpload.splice(i, 1);
        this.imagePreviews.splice(i, 1);
      }
    }
  }

  onFileChange(event: any, i: number) {
    this.hasFilesChanged = true;
    var reader = new FileReader();
    if (event.target.files.length > 0) {
      this.filesToUpload[i] = event.target.files[0];
      reader.readAsDataURL(this.filesToUpload[i]);
      reader.onload = (_event) => {
        this.imagePreviews[i] = reader.result;
      };
    }
  }

  onSubmit() {
    if (this.isButtonEnabled) {
      this.isButtonEnabled = false;

      this.creationFormState = 'Идёт обновление объявления... подождите..';
      const advertisementToSend: NewAdvertisement = {
        title: this.advertisementForm.value.title,
        description: this.advertisementForm.value.description,
        price: this.advertisementForm.value.price,
        cover: 'true',
        categoryId: this.newCategoryId,
      };

      this.advertisementService
        .updateAdvertisement(advertisementToSend, this.id)
        .pipe(untilDestroyed(this))
        .subscribe(
          (res) => {
            if (this.hasFilesChanged) {
              this.creationFormState =
                'Загрузка файлов к объявлению №' + this.id + '... подождите..';

              if (this.advertisement.images.length > 0) {
                let fileRemoverIndex = 0;
                const deleteObservables = this.advertisement.images.map(
                  (entry: { id: string }) => {
                    return this.deleteSingleFile(entry.id);
                  }
                );
                concat(...deleteObservables).subscribe(
                  (singleFile) => {
                    fileRemoverIndex += 1;
                    this.creationFormState =
                      'Удалено..' +
                      fileRemoverIndex +
                      '/' +
                      this.advertisement.images.length +
                      'файлов.. подождите';

                    if (fileRemoverIndex == this.advertisement.images.length) {
                      this.sendFiles();
                    }
                  },
                  (error) => {}
                );
              } else {
                this.sendFiles();
              }
            } else {
              this.creationFormState =
                'Информация загружена... переход на страницу объявления..';
              this.router.navigateByUrl(
                '/advertisements/' +
                  this.advertisement.category.id +
                  '/advertisement/' +
                  this.id
              );
            }
          },
          (error) => {
            this.isButtonEnabled = true;
            this.creationFormState = 'Произошла ошибка. Попробовать снова?';
          }
        );
    }
  }

  sendFiles() {
    let fileIteratorIndex = 1;
    const observables = this.filesToUpload.map((entry) => {
      return this.fetchSingleFile(this.id + '', entry);
    });
    concat(...observables).subscribe(
      (singleMedia) => {
        fileIteratorIndex += 1;
        this.creationFormState =
          'Загружено..' +
          fileIteratorIndex +
          '/' +
          this.filesToUpload.length +
          'файлов.. подождите';

        if (fileIteratorIndex > this.filesToUpload.length) {
          this.creationFormState =
            'Файлы загружены... переход на страницу объявления..';
          this.router.navigateByUrl(
            '/advertisements/' +
              this.advertisement.category.id +
              '/advertisement/' +
              this.id
          );
        }
      },
      (error) => {}
    );
  }

  fetchSingleFile(newId: string, entry: File): Observable<any> {
    return this.imageService
      .sendAdvertisementImage({
        id: Number(newId),
        image: entry,
      })
      .pipe(tap((mediaEntry) => console.log(mediaEntry)));
  }

  deleteSingleFile(fileId: string): Observable<any> {
    return this.imageService.deleteImageFromAdvertisement(this.id, fileId);
  }

  dataURItoBlob(dataURI: string) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/jpeg' });
    return blob;
  }

  ngOnInit() {
    this.route.paramMap
      .pipe(switchMap((params) => params.getAll('id')))
      .subscribe((data) => (this.id = +data));

    window.scroll(0, 0);

    this.advertisementService
      .getAdvertisementById(this.id)
      .subscribe((data: Advertisement) => {
        this.advertisement = data;

        this.advertisementForm.setValue({
          title: this.advertisement.title,
          description: this.advertisement.description,
          price: this.advertisement.price,
        });

        this.selectedCategory = this.categories[this.advertisement.category.id];
        this.newCategoryId = this.advertisement.category.id;

        for (let z = 0; z < this.advertisement.images.length - 1; z++) {
          this.filesToUpload.push({} as File);
        }

        for (let zi = 0; zi < this.filesToUpload.length; zi++) {
          this.imageService
            .getImageById(this.advertisement.images[zi].id)
            .pipe(untilDestroyed(this))
            .subscribe((data: any) => {
              this.imagePreviews[zi] =
                'data:image/jpeg;base64,' + data.imageBlob;
              let convertedImage: string = [this.imagePreviews[zi]] + '';
              let splitConvImage: string = convertedImage.split(',')[1];
              const imageBlob = this.dataURItoBlob(splitConvImage);
              const imageFile: File = new File(
                [imageBlob],
                'updatedFile_' + zi + '.jpg',
                {
                  type: 'image/jpeg',
                }
              );
              this.filesToUpload[zi] = imageFile;
            });
        }
      });
  }
}
