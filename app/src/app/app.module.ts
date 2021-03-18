import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { MenuComponent } from './@shared/components/menu/menu.component';
import { SubmenuComponent } from './@shared/components/submenu/submenu.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    SubmenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
