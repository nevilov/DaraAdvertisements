import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {switchMap, tap} from 'rxjs/operators';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import {ChatService} from '../../../services/chat.service';
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

    constructor(private route: ActivatedRoute,
                private router: Router,
                private advertisementService: AdvertisementService,
                private chatService: ChatService)
    {
    }

    ngOnInit() {
        this.route.paramMap.pipe(
            switchMap(params => params.getAll('id'))
        )
        .subscribe(data => this.id = +data);
        this.advertisementService.getAdvertisementById(this.id)
            .subscribe((data: Advertisement) => {
              this.advertisement = data;
              console.log(data);
            });
    }

    onCreateChat(){
      this.chatService.createChat(this.id)
        .subscribe((r) => {
          this.router.navigateByUrl('/chats');
        });
    }

}
