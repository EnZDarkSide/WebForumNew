using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebForumNew.Classes;

namespace WebForumNew.Models.Requests
{
    public class SectionRequestModel : Section
    {
    }

    public class DiscussionRequestModel : Discussion
    {
    }

    public class TopicRequestModel : Topic
    {
    }

    public class SearchRequest
    {
        public string searchString { get; set; }
    }
}
