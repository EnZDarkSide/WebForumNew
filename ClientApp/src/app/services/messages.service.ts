import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { TopicMessage } from '../models/discussion';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  constructor(private http: HttpClient) { }

  public getMessages(topicId: string | number): Promise<any> {
    return this.http.get<any[]>(environment.apiUrl+"/getTopicMessages/"+topicId).toPromise();
  }

  public sendMessage(topicId: number, message: string, replyTo?: string): Promise<any> {

    let currentUser = JSON.parse(localStorage.getItem("currentUser")) as User;

    if(!currentUser.username){
      throw new Error("Пользователь не авторизован");
    }

    let newMessage = new TopicMessage();
    newMessage.dateTime = this.createDateAsUTC(new Date());
    newMessage.message = message;
    newMessage.topicId = topicId;
    newMessage.replyTo = replyTo;

    return this.http.post(environment.apiUrl+"/createTopicMessage", newMessage).toPromise()
  }

  public deleteMessage(messageId: string){
    return this.http.get(environment.apiUrl+"/deleteTopicMessage/"+messageId).toPromise();

  }

  createDateAsUTC(date) {
    return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds()));
  }

  convertDateToUTC(date) {
      return new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
  }


}
