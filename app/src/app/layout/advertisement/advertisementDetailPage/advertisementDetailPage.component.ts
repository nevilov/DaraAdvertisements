
import { CookieService } from 'ngx-cookie-service';
import { UserService } from './../../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { switchMap } from 'rxjs/operators';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { ImageService } from 'src/app/services/image.service';
import { ChatService } from '../../../services/chat.service';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';

@UntilDestroy()
@Component({
    selector: 'app-advertisementDetailPage',
    templateUrl: './advertisementDetailPage.component.html',
    styleUrls: ['./advertisementDetailPage.component.scss'],
    providers: [
        AdvertisementService
    ]
})
export class AdvertisementDetailPageComponent implements OnInit {
    public id = 0;
    public advertisement: Advertisement | null = null;
    images: any[];
    imageValues: string[];
    userId: number = 0;
    sameAdvertisements: Advertisement[] | null = null;
    userAdvertisements: Advertisement[] | null = null;

    constructor(
        private route: ActivatedRoute,
        private advertisementService: AdvertisementService,
        private imageService: ImageService,
        private chatService: ChatService,
        private router: Router,
        private userService: UserService,
        private ngDynamicBreadcrumbService: NgDynamicBreadcrumbService
    ) {
        this.advertisement = {} as Advertisement;
        this.images = [];
        this.imageValues = [];
    }

    changeImage(i: number) {
        this.imageValues[0] = this.imageValues[i];
    }

    ngOnInit() {
        this.route.paramMap.pipe(
            switchMap(params => params.getAll('id'))
        )
            .subscribe(data => this.id = +data);

        this.advertisementService.getAdvertisementById(this.id)
            .subscribe((data: Advertisement) => {
                this.advertisement = data;
                console.log(this.advertisement);

                const breadcrumb = { categoryId: this.advertisement.category.id, category: this.advertisement.category.name, title: this.advertisement.title };
                this.ngDynamicBreadcrumbService.updateBreadcrumbLabels(breadcrumb);

                this.images = data.images;

                this.advertisementService.getSameAdvertisementsWithLimit(this.advertisement.category.id, 4).subscribe((data) => {
                    this.sameAdvertisements = data.items;
                });

                this.userService.getUserAdvertisementsWithLimit(this.advertisement.owner.id, 4, 0).subscribe((data) => {
                    this.userAdvertisements = data.items;
                    console.log(data);
                });

                for (let i = 0; i < this.images.length; i++) {
                    this.imageService.getImageById(this.images[i].id)
                        .pipe(untilDestroyed(this))
                        .subscribe((data: any) => {
                            this.imageValues[i + 1] = 'data:image/jpeg;base64,' + data.imageBlob;
                            if (i === 0) {
                                this.imageValues[0] = this.imageValues[1]
                            }
                        });
                }
            });
    }


    onCreateChat() {
        this.chatService.createChat(this.id)
            .subscribe((r) => {
                this.router.navigateByUrl('/chats');
            });
    }

}
