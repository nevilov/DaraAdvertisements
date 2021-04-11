import { Component, OnInit } from '@angular/core';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';

@Component({
	selector: 'app-advertisementPage',
	templateUrl: './advertisementPage.component.html',
	styleUrls: ['./../../../../assets/scss/pages/__home.scss'],
	providers: [AdvertisementService],
})
export class AdvertisementPageComponent implements OnInit {
	advertisements: Advertisement[] = [];

	constructor(private advertisementService: AdvertisementService) { }

	ngOnInit() {
		this.loadAdvertisements();
	}

	loadAdvertisements() {
		this.advertisementService.getAllAdvertisements().subscribe((data) => {
			this.advertisements = data.items;
			console.log(this.advertisements);
		});
	}
}
