import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppComponent } from '../app.component';
import { ListOfCategories } from '../Dtos/category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  public getAllCategories(): Observable<ListOfCategories> {
    return this.http.get<ListOfCategories>(
      AppComponent.backendAddress + '/api/Category/get/top'
    );
  }

  public getCategoryChildrens(id: number): Observable<ListOfCategories> {
    return this.http.get<ListOfCategories>(
      AppComponent.backendAddress + '/api/Category/get/' + id
    );
  }
}
