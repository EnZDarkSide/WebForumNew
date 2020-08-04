export class TopicMessage {
  id: string;
  topicId: string;
  AuthorId: string;
  Message: string;
  DateTime: Date;
  isPinned?: boolean = false;
}
