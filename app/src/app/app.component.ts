import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent {
    title = 'DaraAds';
    public static backendAddress: string = 'http://185.60.134.206:80';
}
