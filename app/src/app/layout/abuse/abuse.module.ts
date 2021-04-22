import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewAbusePageComponent } from './newAbusePage/newAbusePage.component';
import { AbusePageComponent } from './abusePage/abusePage.component';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule
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
