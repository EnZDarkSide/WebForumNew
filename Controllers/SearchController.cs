using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebForumNew.Data;
using WebForumNew.Models.Requests;

namespace WebForumNew.Controllers
{
    [Route("/search")]
    [Produces("application/json")]
    public class SearchController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [HttpPost]
        public JsonResult Search([FromBody]SearchRequest request)
        {
            var Discussions = db.Discussions.Select(disc => new {
                disc.Id,
                disc.Title,
                disc.ImgSource,
                disc.Description,
                messagesCount = disc.Topics.Select(topic => topic.TopicMessages.Count()).Sum(),
                topicsCount = disc.Topics.Count()
            }).Where(disc => disc.Title.Contains(request.searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            var Topics = db.Topics.Select(topic => new {
                topic.Id,
                topic.Title,
                topic.Description,
                Author = new { topic.Author.ID, topic.Author.Username },
                MessagesCount = topic.TopicMessages.Count()
            })
            .Where(topic => topic.Title.Contains(request.searchString, StringComparison.OrdinalIgnoreCase)).ToList();


            var result = new { Discussions, Topics };

            return Json(result);
        }
    }
}