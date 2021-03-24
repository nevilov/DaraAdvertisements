import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdvertisementPageComponent } from './layout/advertisement/advertisementPage/advertisementPage.component';

const routes: Routes = [
  { path: '', component: AdvertisementPageComponent },
  // { path: 'registration', component: RegistrationPageComponent },
  // { path: 'autorization', component: RegistrationPageComponent },
  // { path: 'help', component: RegistrationPageComponent },
  // { path: 'contact', component: RegistrationPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
