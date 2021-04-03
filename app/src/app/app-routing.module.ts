import { EditAdvertisementPageComponent } from './layout/advertisement/editAdvertisementPage/editAdvertisementPage.component';
import { NewAdvertisementPageComponent } from './layout/advertisement/newAdvertisementPage/newAdvertisementPage.component';
import { AdvertisementDetailPageComponent } from './layout/advertisement/advertisementDetailPage/advertisementDetailPage.component';
import { NewAbusePageComponent } from './layout/abuse/newAbusePage/newAbusePage.component';
import { AbusePageComponent } from './layout/abuse/abusePage/abusePage.component';
import { ContactsComponent } from './layout/contacts/contacts.component';
import { HelpComponent } from './layout/help/help.component';
import { LoginPageComponent } from './layout/auth/loginPage/loginPage.component';
import { RegistrationPageComponent } from './layout/auth/registrationPage/registrationPage.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdvertisementPageComponent } from './layout/advertisement/advertisementPage/advertisementPage.component';

const routes: Routes = [
  { path: '', component: AdvertisementPageComponent },
  { path: 'registration', component: RegistrationPageComponent },
  { path: 'autorization', component: LoginPageComponent },
  { path: 'help', component: HelpComponent },
  { path: 'contact', component: ContactsComponent },
  { path: 'abuse', component: AbusePageComponent },
  { path: 'newAbuse', component: NewAbusePageComponent },
  { path: 'advertisements', component: AdvertisementPageComponent },
  { path: 'advertisements/:id', component: AdvertisementDetailPageComponent },
  { path: 'newAdvertisement', component: NewAdvertisementPageComponent },
  { path: 'editAdvertisement/:id', component: EditAdvertisementPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
