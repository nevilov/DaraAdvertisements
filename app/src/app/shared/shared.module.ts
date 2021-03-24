import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './components/footer/footer.component';
import { MenuComponent } from './components/menu/menu.component';
import { SubmenuComponent } from './components/submenu/submenu.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    FooterComponent,
    MenuComponent,
    SubmenuComponent
  ],
  exports: [
    FooterComponent,
    MenuComponent,
    SubmenuComponent
  ]
})
export class SharedModule { }
