import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewAbusePageComponent } from './newAbusePage/newAbusePage.component';
import { AbusePageComponent } from './abusePage/abusePage.component';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, SharedModule],
  declarations: [NewAbusePageComponent, AbusePageComponent],
  exports: [NewAbusePageComponent, AbusePageComponent],
})
export class AbuseModule {}
