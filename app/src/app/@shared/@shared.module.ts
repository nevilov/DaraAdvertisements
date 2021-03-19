import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiBreadcrumbs } from './components/TuiBreadcrumbs/TuiBreadcrumbs';
import { TuiBreadcrumbsModule } from '@taiga-ui/kit';

@NgModule({
  declarations: [
    TuiBreadcrumbs
  ],
  imports: [
    CommonModule,
    TuiBreadcrumbsModule
  ],
  exports: [
    TuiBreadcrumbs,
  ]
})
export class sharedModule { }
