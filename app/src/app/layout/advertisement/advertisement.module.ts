import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewAdvertisementPageComponent } from './newAdvertisementPage/newAdvertisementPage.component';
import { EditAdvertisementPageComponent } from './editAdvertisementPage/editAdvertisementPage.component';
import { AdvertisementDetailPageComponent } from './advertisementDetailPage/advertisementDetailPage.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AdvertisementPageWithSubCategoriesComponent } from './advertisementPageWithSubCategories/advertisementPageWithSubCategories.component';
import { HttpClientModule } from '@angular/common/http';
import { AngularYandexMapsModule, YA_CONFIG } from 'angular8-yandex-maps';
import { NgxDadataModule } from '@kolkov/ngx-dadata';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        SharedModule,
        ReactiveFormsModule,
        HttpClientModule,
        AngularYandexMapsModule,
        HttpClientModule,
        NgxDadataModule
    ],
    declarations: [
        NewAdvertisementPageComponent,
        EditAdvertisementPageComponent,
        AdvertisementDetailPageComponent,
        AdvertisementPageWithSubCategoriesComponent,
    ],
    exports: [
        NewAdvertisementPageComponent,
        EditAdvertisementPageComponent,
        AdvertisementDetailPageComponent,
        AdvertisementPageWithSubCategoriesComponent,
    ],
    providers: [
        {
            provide: YA_CONFIG,
            useValue: {
                apikey: '',
                lang: 'en_US',
            },
        },
    ],
})
export class AdvertisementModule { }
