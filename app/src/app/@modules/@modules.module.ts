import { sharedModule } from 'src/app/@shared/@shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from './homePage/homePage.component';
import { TuiBreadcrumbs } from '../@shared/components/TuiBreadcrumbs/TuiBreadcrumbs';

@NgModule({
  declarations: [
    HomePageComponent
  ],
  imports: [
    CommonModule,
    sharedModule
  ]
})
export class modulesModule { }