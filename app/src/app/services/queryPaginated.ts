import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListOfItems } from '../Dtos/advertisement';

export function queryPaginated<T>(
  http: HttpClient,
  baseUrl: string,
  queryParams?: any,
  headers?: any
): Observable<ListOfItems<T>> {
  let params = new HttpParams();
  let url = baseUrl;

  Object.keys(queryParams)
    .sort()
    .forEach((key) => {
      const value = queryParams[key];
      if (value !== null && value !== '') {
        params = params.set(key, value.toString());
      }
    });

  return http.get<ListOfItems<T>>(url, {
    params,
    headers,
  });
}
