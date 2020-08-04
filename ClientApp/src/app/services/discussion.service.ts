import { Injectable } from '@angular/core';
import { Discussion } from '../models/discussion';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DiscussionService {

  constructor(private http: HttpClient) { }

  public getDiscussions(discussionId: string | number = 1): Promise<any> {
    return this.http.get<Discussion[]>(environment.apiUrl+"/getDiscussion/"+discussionId).toPromise();
  }
}
