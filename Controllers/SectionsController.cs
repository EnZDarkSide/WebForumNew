using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebForumNew.Classes;
using WebForumNew.Data;
using WebForumNew.Models.Requests;

namespace WebForumNew.Controllers
{
    [ApiController]
    [Route("api/sections")]
    public class SectionsController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [HttpGet("/getSections")]
        public JsonResult GetSectionsInfo()
        {
            var sections = db.Sections
                .Select(sec => new
                {
                    id = sec.Id,
                    title = sec.Title,
                    discussions = sec.Discussions.Select(disc => new
                    {
                        disc.Id,
                        disc.Title,
                        disc.ImgSource,
                        disc.Description,
                        messagesCount = disc.Topics.Select(topic => topic.TopicMessages.Count()).Sum(),
                        topicsCount = disc.Topics.Count()
                    })
                })
                .ToList();

            return Json(sections);
        }


        [HttpGet("/getSection/{id}")]
        public Section GetSection(int id)
        {
            return db.Sections.FirstOrDefault(section => section.Id == id);
        }

        // Возвращает ID новой секции
        [HttpPost("/createSection")]
        public int CreateSection([FromBody]SectionRequestModel request)
        {
            var NewSection = new Section
            {
                Id = request.Id,
                Title = request.Title
            };

            db.Sections.Add(NewSection);
            db.SaveChanges();

            return NewSection.Id;
        }
    }
}
