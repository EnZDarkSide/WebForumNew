import { Component, OnInit } from '@angular/core';
import { MessagesService } from 'src/app/services/messages.service';
import { TopicMessage, TopicMeta } from 'src/app/models/discussion';
import { ActivatedRoute } from '@angular/router';

import { User } from 'src/app/models/user';

import { EditorChangeContent, EditorChangeSelection } from 'ngx-quill';
import Quill from 'quill';

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.scss']
})
export class TopicComponent implements OnInit {

  blurred = false
  focused = false

  currentUser = JSON.parse(localStorage.getItem("currentUser")) as User;

  messages: TopicMessage[];
  topicMeta: TopicMeta;
  topicId: number;

  editorData = '<p>Hello, world!</p>';

  constructor(private messagesService: MessagesService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    if(!this.currentUser)
      this.currentUser = new User();

    this.topicId = Number(this.route.snapshot.paramMap.get("topicId"));
    this.messagesService.getMessages(this.topicId).then(data =>
      {
        this.messages = (data.topicMessages as TopicMessage[]).sort((a,b) => a.dateTime > b.dateTime ? 1 : 0);

        this.messages.forEach(element => {
          element.dateTime = new Date(element.dateTime);
          console.log(element.dateTime.getUTCDate());
        });

        this.topicMeta = data.topicMeta;
        console.log(data);
      }
    );
  }

  isAuthorized() {
    return this.currentUser.token != null;
  }

  deleteMessage(messageId: string) {
    this.messagesService.deleteMessage(messageId).finally(() => location.reload());
  }

  public onSubmit(){
    this.messagesService.sendMessage(this.topicId, this.editorData).finally(() => location.reload());
  }


  changedEditor(event: EditorChangeContent) {
    this.editorData = event.html;
  }
}
