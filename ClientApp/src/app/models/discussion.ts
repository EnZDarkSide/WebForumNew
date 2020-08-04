import {User} from './user';

export class Discussion {
  id: number;
  title: string;
  description: string;
  ImgSource: string;
  messagesCount: number;
  topicsCount: number;
  discussions: Discussion[];
}

export class searchObject {
  discussions: Discussion[] = [];
  topics: Topic[] = [];
  topicMessages: TopicMessage[] = [];
}

export class Topic {
  id: number;
  title: string;
  author: User;
  description: string;
  discussionId: string;
  messagesCount: number;

}

export class TopicMessage {
  id: number;
  topicId: number;
  author: User;
  message: string;
  dateTime: Date;
  replyTo: string;
}

export class TopicMeta {
  id: number;
  title: string;
  description: string;
}

export class Section {
  id: number;
  title: string;
  discussions: Discussion[];
}
