import { UserProfilePersonalComponent } from './layout/user/userProfile/userProfilePersonal/userProfilePersonal.component';
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
import { PublicProfileComponent } from './layout/user/publicProfile/publicProfile.component';
import { ForgotPasswordPageComponent } from './layout/auth/forgotPasswordPage/forgotPasswordPage.component';
import { ResetPasswordPageComponent } from "./layout/auth/resetPasswordPage/resetPasswordPage.component";
import { UserChatsComponent } from "./layout/user/userChats/userChats.component";
import { UserProfileLoginPassComponent } from './layout/user/userProfile/userProfileLoginPass/userProfileLoginPass.component';
import { UserProfileAdvertisementsComponent } from './layout/user/userProfile/userProfileAdvertisements/userProfileAdvertisements.component';
import { UserProfileSettingsComponent } from './layout/user/userProfile/userProfileSettings/userProfileSettings.component';
import { UserProfileComponent } from './layout/user/userProfile/userProfile.component';
import { AdvertisementPageWithSubCategoriesComponent } from './layout/advertisement/advertisementPageWithSubCategories/advertisementPageWithSubCategories.component';
import { LoginGuard } from './Guards/login.guard';
import { UserProfileBlockComponent } from "./layout/user/userProfile/userProfileBlock/userProfileBlock.component"; import { UserProfileBulkLoadingComponent } from "./layout/user/userProfile/userProfileBulkLoading/userProfileBulkLoading.component";

const routes: Routes = [
    // { path: '**', component: PageNotFoundComponent }
    { path: '', redirectTo: 'advertisements', pathMatch: 'full' },
    { path: 'registration', component: RegistrationPageComponent },
    { path: 'autorization', component: LoginPageComponent },
    {
        path: 'help', component: HelpComponent, data: {
            breadcrumb: [
                { label: 'Главная', url: '' },
                { label: 'Помощь', url: '/help' },
            ]
        },
    },
    {
        path: 'contact', component: ContactsComponent, data: {
            breadcrumb: [
                { label: 'Главная', url: '' },
                { label: 'Контакты', url: '/contact' },
            ]
        },
    },
    {
        path: 'abuse', component: AbusePageComponent, data: {
            breadcrumb: [
                { label: 'Главная', url: '' },
                { label: 'Жалобы', url: 'abuse' },
            ]
        },
    },
    {
        path: 'newAbuse', component: NewAbusePageComponent, data: {
            breadcrumb: [
                { label: 'Главная', url: '' },
                { label: 'Жалобы', url: 'abuse' },
                { label: 'Новая жалоба', url: '/newAbuse' },
            ]
        },
    },
    {
        path: 'advertisements', component: AdvertisementPageComponent, data: {
            breadcrumb: [
                { label: 'Объявления', url: '/advertisements' }
            ]
        }
    },
    {
        path: 'advertisements/:categoryId', component: AdvertisementPageWithSubCategoriesComponent, data: {
            breadcrumb: [
                { label: 'Объявления', url: '/advertisements' },
                { label: '{{categoryName}}', url: 'advertisements/:id' },
            ]
        },
    },
    {
        path: 'advertisements/:categoryId/advertisement/:id', component: AdvertisementDetailPageComponent, data: {
            breadcrumb: [
                { label: 'Объявления', url: '/advertisements' },
                { label: '{{category}}', url: '/advertisements/:categoryId' },
                { label: '{{title}}', url: 'advertisements/:categoryId/advertisement/:id' },
            ]
        },
    },
    {
        path: 'newAdvertisement', component: NewAdvertisementPageComponent, data: {
            breadcrumb: [
                { label: 'Объявления', url: '/advertisements' },
                { label: 'Новое объявление', url: '/newAdvertisement' },
            ]
        },
    },
    {
        path: 'editAdvertisement/:id', component: EditAdvertisementPageComponent, data: {
            breadcrumb: [
                { label: 'Объявления', url: '/advertisements' },
                { label: 'Редактирование', url: 'editAdvertisement/:id' },
            ]
        },
    },
    { path: 'forgotPassword', component: ForgotPasswordPageComponent },
    { path: 'resetPassword', component: ResetPasswordPageComponent },
    { path: 'chats', component: UserChatsComponent },
    { path: 'profile/:Username', component: PublicProfileComponent, canActivate: [LoginGuard] },
    {
        path: 'cabinet',
        canActivate: [LoginGuard],
        component: UserProfileComponent,
        children: [
            { path: 'personal', component: UserProfilePersonalComponent, },
            { path: 'changeinfo', component: UserProfileLoginPassComponent, },
            { path: 'advertisements', component: UserProfileAdvertisementsComponent, },
            { path: 'block', component: UserProfileBlockComponent, },
            { path: 'favorites', component: UserProfileSettingsComponent, },
            { path: 'import', component: UserProfileBulkLoadingComponent, },
        ],
    },
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule],
    providers: [LoginGuard],
})
export class AppRoutingModule { }
