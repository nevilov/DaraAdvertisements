import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewAdvertisementPageComponent } from './newAdvertisementPage/newAdvertisementPage.component';
import { EditAdvertisementPageComponent } from './editAdvertisementPage/editAdvertisementPage.component';
import { AdvertisementPageComponent } from './advertisementPage/advertisementPage.component';
import { AdvertisementDetailPageComponent } from './advertisementDetailPage/advertisementDetailPage.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  declarations: [
    NewAdvertisementPageComponent,
    EditAdvertisementPageComponent,
    AdvertisementPageComponent,
    AdvertisementDetailPageComponent
  ],
  exports: [
    NewAdvertisementPageComponent,
    EditAdvertisementPageComponent,
    AdvertisementPageComponent,
    AdvertisementDetailPageComponent
  ],

})
export class AdvertisementModule { }
