using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebForumNew.Classes;
using WebForumNew.Data;
using WebForumNew.Models.Requests;

namespace WebForumNew.Controllers
{
    [Route("api/topics")]
    [Produces("application/json")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        [HttpGet("/GetTopics")]
        public IEnumerable<Topic> GetTopics()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Topics;
            }
        }

        [HttpGet("/getTopic/{title}")]
        public Topic GetTopic(string title)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Topics.FirstOrDefault(topic => topic.Title == title);
            }
        }

        [HttpPost("/createTopic")]
        public JsonResult CreateTopic([FromBody]TopicRequestModel request)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var NewTopic = request as Topic;

                db.Topics.Add(NewTopic);
                db.SaveChanges();

                return new JsonResult(new { ID = NewTopic.Id });
            }
        }
    }
}
