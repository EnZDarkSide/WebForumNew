import { Component, OnInit } from '@angular/core';
import { Discussion, Topic } from 'src/app/models/discussion';
import { DiscussionService } from 'src/app/services/discussion.service';
import { ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-discussion',
  templateUrl: './discussion.component.html',
  styleUrls: ['./discussion.component.scss']
})
export class DiscussionComponent implements OnInit {

  discussions: Discussion[];
  discussionId: string;
  discussionTitle: string;
  topics: Topic[];

  constructor(private discussionService: DiscussionService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.discussionId = this.route.snapshot.paramMap.get("discussionId");
    this.discussionService.getDiscussions(this.discussionId).then(data => {
      this.discussions = data.discussions;

      this.discussions.sort(function(a,b) {
        return b.messagesCount - a.messagesCount
      });

      this.discussionTitle = data.title;
      this.topics = data.topics;

      this.topics.sort(function(a,b) {
        return b.messagesCount - a.messagesCount
      });
      console.log(this.discussions);
      console.log(data);
    });
  }

}
