import { SubmenuComponent } from '../submenu/submenu.component';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { DataSharingService } from './../../../services/datasharing.service';
import { SignService } from './../../../services/sign.service';
import { ToastrService } from 'ngx-toastr';
import { ImageService } from 'src/app/services/image.service';
import { untilDestroyed } from '@ngneat/until-destroy';

@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./../../../../assets/scss/layout/__menu.scss'],
})
export class MenuComponent implements OnInit {
    currentUserName: string = '';
    currentUserRole: string = '';
    currentUserAvatar: string = '';
    isAutorized: boolean = false;

    @ViewChild('submenu', { read: ElementRef }) SubmenuComponent!: ElementRef;
    @ViewChild('usermenu', { read: ElementRef }) UserMenu!: ElementRef;
    @ViewChild('btnShowsSubmenu', { read: ElementRef }) span!: ElementRef;
    isSubmenuShown: boolean = false;
    isUsermenuShown: boolean = false;

    public showSubmenu() {
        if (this.isSubmenuShown) {
            this.SubmenuComponent.nativeElement.style.display = 'none';
            this.span.nativeElement.style.transform = 'rotate(180deg)';
            this.isSubmenuShown = false;
        } else {
            this.SubmenuComponent.nativeElement.style.display = 'block';
            this.span.nativeElement.style.transform = 'rotate(360deg)';
            this.isSubmenuShown = true;
        }
    }

    public showUsermenu() {
        if (this.isUsermenuShown) {
            this.UserMenu.nativeElement.style.display = 'none';
            this.isUsermenuShown = false;
        } else {
            this.UserMenu.nativeElement.style.display = 'block';
            this.isUsermenuShown = true;
        }
    }

    public userLogout() {
        this.signService.logout();
        this.isAutorized = false;
        this.showUsermenu();
        // alert(this.isUsermenuShown);
    }

    public checkAuthorized() {
        if (this.cookieService.get('AuthToken') != '') {
            this.isAutorized = true;
            this.currentUserName = this.cookieService.get('AuthUsername');
            this.currentUserRole = this.cookieService.get('UserRole');
        } else {
            this.isAutorized = false;
        }
    }

    constructor(
        
        private imageService: ImageService,
        private cookieService: CookieService,
        private dataSharingService: DataSharingService,
        private signService: SignService,
        public toastr: ToastrService
    ) {
        this.dataSharingService.isUserLoggedIn.subscribe(() => {
            this.checkAuthorized();
        });
    }

    ngOnInit() {
        this.checkAuthorized();

        this.currentUserAvatar = this.cookieService.get('UserAvatar');

        if (this.currentUserAvatar == "null") {
            this.currentUserAvatar = "default";
        }

        this.imageService.getImageById(this.currentUserAvatar)
        .subscribe((data: any) => {
            if (data.imageBlob) {
                this.currentUserAvatar = 'data:image/jpeg;base64,' + data.imageBlob;
            }
        });
    }
}
