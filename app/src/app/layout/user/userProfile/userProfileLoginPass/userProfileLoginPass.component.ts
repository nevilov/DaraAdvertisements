import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-userProfileLoginPass',
  templateUrl: './userProfileLoginPass.component.html',
  styleUrls: ['./userProfileLoginPass.component.scss'],
})
export class UserProfileLoginPassComponent implements OnInit {
  isEmailChanges = false;
  isPasswordChanges = false;
  constructor() {}

  ngOnInit() {}
}
