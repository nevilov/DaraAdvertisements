import { ListOfItems } from './../Dtos/advertisement';
import { AppComponent } from './../app.component';
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Advertisement } from '../Dtos/advertisement';

@Injectable()
export class AdvertisementService{
    // public adv$: BehaviorSubject<Advertisement[]>

    constructor(private http: HttpClient){
      // this.adv$ = new BehaviorSubject(null);
     }

     getAllAdvertisements(): Observable<ListOfItems<Advertisement>> {
      return this.http.get<ListOfItems<Advertisement>>(AppComponent.backendAddress + '/api/Advertisement?limit=10&offset=0');
    }

    getAdvertisementById(id:number): Observable<Advertisement> {
      return this.http.get<Advertisement>(AppComponent.backendAddress + '/api/Advertisement/'+ id);
    }
}
