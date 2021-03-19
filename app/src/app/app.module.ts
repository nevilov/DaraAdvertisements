import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { coreModule } from './@core/@core.module';
import { modulesModule } from './@modules/@modules.module';
import { sharedModule } from 'src/app/@shared/@shared.module';
import { TuiActionModule } from '@taiga-ui/kit';

import { AppComponent } from './app.component';
import { MenuComponent } from './@shared/components/menu/menu.component';
import { SubmenuComponent } from './@shared/components/submenu/submenu.component';
import { FooterComponent } from './@shared/components/footer/footer.component';

import { ReactiveFormsModule } from '@angular/forms';

import {
  TuiNotificationsModule,
  TuiDialogModule,
  TuiRootModule,
  iconsPathFactory,
  TUI_ICONS_PATH
} from '@taiga-ui/core';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    SubmenuComponent,
    FooterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    sharedModule,
    modulesModule,
    coreModule,
    ReactiveFormsModule,
    TuiRootModule,
    TuiNotificationsModule,
    TuiDialogModule,
    TuiActionModule
  ],
  providers: [
    {
      provide: TUI_ICONS_PATH,
      useValue: iconsPathFactory('assets/taiga-ui/icons/'),
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
