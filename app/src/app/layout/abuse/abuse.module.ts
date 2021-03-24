import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewAbusePageComponent } from './newAbusePage/newAbusePage.component';
import { AbusePageComponent } from './abusePage/abusePage.component';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    NewAbusePageComponent,
    AbusePageComponent
  ],
  exports: [
    NewAbusePageComponent,
    AbusePageComponent
  ]
})
export class AbuseModule { }
