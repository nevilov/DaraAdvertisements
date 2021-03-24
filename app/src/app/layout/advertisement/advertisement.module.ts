import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewAdvertisementPageComponent } from './newAdvertisementPage/newAdvertisementPage.component';
import { EditAdvertisementPageComponent } from './editAdvertisementPage/editAdvertisementPage.component';
import { AdvertisementPageComponent } from './advertisementPage/advertisementPage.component';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    NewAdvertisementPageComponent,
    EditAdvertisementPageComponent,
    AdvertisementPageComponent
  ],
  exports: [
    NewAdvertisementPageComponent,
    EditAdvertisementPageComponent,
    AdvertisementPageComponent
  ]
})
export class AdvertisementModule { }
