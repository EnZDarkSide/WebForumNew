import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Section } from '../models/discussion';

@Injectable({
  providedIn: 'root'
})
export class SectionsService {

  constructor(private http: HttpClient) { }

  public getSections() : Promise<Section[]>
  {
    return this.http.get<Section[]>(environment.apiUrl+"/getSections").toPromise();
  }
}
