import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-userProfilePersonal',
  templateUrl: './userProfilePersonal.component.html',
  styleUrls: ['./userProfilePersonal.component.scss'],
})
export class UserProfilePersonalComponent implements OnInit {
  constructor() {}

  ngOnInit() {}

  avatarChange() {
    const fileInput = document.getElementById('avatar');
    fileInput?.addEventListener('change', function () {
      const selectedFile = fileInput;
      alert();
    });
  }
}
