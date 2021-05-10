import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { User } from 'src/app/Dtos/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-publicProfile',
  templateUrl: './publicProfile.component.html',
  styleUrls: ['./publicProfile.component.scss'],
})
export class PublicProfileComponent implements OnInit {
  advertisements: Advertisement[] = [];
  user: User | null = null;
  userName: string = '';

  total: number = 0;

  queryParams = {
    Id: '',
    limit: 10,
    offset: 0,
  };

  constructor(
    private userService: UserService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.userName = params['Username'];
    });
    this.loadUserInfo();
  }

  loadAdvertisements(id: string) {
    this.userService.getUserAdvertisements(id).subscribe((data) => {
      this.advertisements = data.items;
    });
  }

  loadUserInfo() {
    this.userService.getUser(this.userName).subscribe((data) => {
      this.user = data;
      this.loadAdvertisements(data.id);
    });
  }
}
