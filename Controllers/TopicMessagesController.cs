using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebForumNew.Classes;
using WebForumNew.Data;

namespace WebForumNew.Controllers
{
    [Route("api/topicmessages")]
    [Produces("application/json")]
    [ApiController]
    public class TopicMessagesController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [Authorize(Roles = "Admin")]
        [HttpPost("/getTopicMessages")]
        public IEnumerable<TopicMessage> GetSections()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.TopicMessages;
            }
        }

        [HttpGet("/getTopicMessages/{topicID}")]
        public JsonResult GetTopicMessages(int topicId)
        {
            var topicMessages = db.TopicMessages.Where(topicMsg => topicMsg.TopicId == topicId)
                .Select(topic => new {
                    topic.ID,
                    topic.TopicId,
                    topic.Author,
                    topic.Message,
                    topic.DateTime,
                    topic.ReplyTo,
                }).ToList();

            var topicMeta = db.Topics.Select(topic => new 
                {
                    topic.Description,
                    topic.Title,
                    topic.Id,
                }).FirstOrDefault(topic => topic.Id == topicId);

            return Json(new {
                topicMeta,
                topicMessages 
            });
        }

        [Authorize]
        [HttpPost("/createTopicMessage")]
        public void CreateTopicMessage([FromBody]TopicMessage request)
        {
            request.Author = db.Users.FirstOrDefault(user => user.Username == User.Identity.Name);
            var NewTopicMessage = request;

            db.TopicMessages.Add(NewTopicMessage);
            db.SaveChanges();

            Response.StatusCode = 200;
        }

        [Authorize]
        [HttpGet("/deleteTopicMessage/{id}")]
        public void DeleteTopicMessage(int id)
        {
            var topicMessage = db.TopicMessages.FirstOrDefault(topic => topic.ID == id);
            var userRole = db.Users.FirstOrDefault(user => user.Username == User.Identity.Name).Role;

            db.Entry(topicMessage).Reference(tMessage => tMessage.Author).Load();

            if (User.Identity.Name != topicMessage.Author.Username || userRole != "Admin")
                Response.StatusCode = 401;

            db.TopicMessages.Remove(topicMessage);
            db.SaveChanges();

            Response.StatusCode = 200;
        }
    }
}
