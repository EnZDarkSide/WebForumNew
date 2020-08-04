import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { searchObject } from '../models/discussion';

@Injectable({ providedIn: 'root' })
export class SearchService {

  constructor(public http: HttpClient) { }

  public search(searchString: string = ""): Promise<searchObject> {
    let query = {
      searchString,
    }
    return this.http.post<searchObject>(environment.apiUrl+"/search", query).toPromise();
  }
}
