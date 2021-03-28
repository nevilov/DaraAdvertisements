import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import { Advertisement } from '../Dtos/advertisement';

@Injectable()
export class HttpService{

    constructor(private http: HttpClient){ }

    getAllAdvertisements() {

    }
}
