import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { AppComponent } from "../app.component";
import { catchError } from "rxjs/operators";
import { AdvertisementFile } from "../Dtos/advertisementFile";


@Injectable({
    providedIn: 'root',
})

export class ImageService {
    constructor(
        private http: HttpClient,
        private cookieService: CookieService) {
    }

    public sendAdvertisementImage(inputRequest: AdvertisementFile) {
        var formData = new FormData();
        formData.append("image", inputRequest.image, inputRequest.image.name)

        return this.http
          .patch(AppComponent.backendAddress + '/api/Advertisement/' + inputRequest.id + '/images', formData, {
            headers: new HttpHeaders({
              Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
            }),
          });
    }

    public sendUserAvatarImage(image: File) {
      var formData = new FormData();
      formData.append("image", image, image.name)
      return this.http
        .patch(AppComponent.backendAddress + '/api/User/images', formData, {
          headers: new HttpHeaders({
            Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
          }),
        });
    }

    public deleteImageFromAdvertisement(advId: number, imageId: string) {
      return this.http.delete<any>(AppComponent.backendAddress + '/api/Advertisement/' + advId + '/images/' + imageId, {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      });
    }

    public getImageById(imageId: string) {
          return this.http.get<any>(AppComponent.backendAddress + '/api/images/' + imageId);
    }

    public checkError(error: any) {
        alert('Произошла ошибка: ' + error.error.error);
        console.log(error);
        return error;
    }
}
