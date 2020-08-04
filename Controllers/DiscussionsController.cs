using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebForumNew.Classes;
using WebForumNew.Data;
using WebForumNew.Models.Requests;

namespace WebForumNew.Controllers
{
    [Route("api/discussions")]
    [Produces("application/json")]
    [ApiController]
    public class DiscussionsController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [HttpGet("/getDiscussions")]
        public JsonResult GetDiscussions()
        {
            var discussions = db.Discussions
            .Include(disc => disc.Discussions)
            .Include(disc => disc.Topics)
            .Select(disc => new
            {
                disc.Id,
                disc.Title,
                disc.Description,

                discussions = disc.Discussions.Select(subdisc => new
                {
                    discussion = subdisc,
                    messagesCount = subdisc.Topics.Select(topic => topic.TopicMessages.Count()).Sum(),
                    topicsCount = subdisc.Topics.Count(),
                }),
                topics = disc.Topics.Select(topic => new
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    Description = topic.Description,
                    Author = topic.Author,
                    MessageCount = topic.TopicMessages.Count()
                })
            })
            .ToList();

            return Json(discussions);
        }

        [HttpGet("/getDiscussion/{id}")]
        public JsonResult GetDiscussion(int id)
        {
            var discussion = db.Discussions
            .Include(disc => disc.Discussions)
            .Include(disc => disc.Topics)
            .Select(disc => new
            {
                disc.Id,
                disc.Title,

                discussions = disc.Discussions.Select(subdisc => new
                {
                    subdisc.Id,
                    subdisc.Title,
                    subdisc.ImgSource,
                    subdisc.Description,

                    messagesCount = subdisc.Topics.Select(topic => topic.TopicMessages.Count()).Sum(),
                    topicsCount = subdisc.Topics.Count(),
                }),
                topics = disc.Topics.Select(topic => new
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    Description = topic.Description,
                    Author = new { topic.Author.ID, topic.Author.Username},
                    MessagesCount = topic.TopicMessages.Count()
                })
            })
            .FirstOrDefault(disc => disc.Id == id);

            return Json(discussion);
        }

        [HttpPost("/find/")]
        public List<Discussion> Find([FromBody]string searchString)
        {
            using (ApplicationContext db = new ApplicationContext())
                return db.Discussions.Where(disc => disc.Description.Contains(searchString)).ToList();
        }

        [HttpPost("/createDiscussion")]
        public JsonResult CreateDiscussion([FromBody] DiscussionRequestModel request)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var NewDiscussion = request as Discussion;

                db.Discussions.Add(NewDiscussion);
                db.SaveChanges();
                return new JsonResult(new { ID = NewDiscussion.Id });
            }
        }
    }
}
