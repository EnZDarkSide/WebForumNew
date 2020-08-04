using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebForumNew.Classes
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [ForeignKey("SectionId")]
        public List<Discussion> Discussions { get; set; }

    }

    public class DiscussionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Discussion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgSource { get; set; }
        public int? DiscussionId { get; set; }
        public int? SectionId { get; set; }

        [ForeignKey("DiscussionId")]
        public List<Discussion> Discussions { get; set; }

        [ForeignKey("DiscussionId")]
        public List<Topic> Topics { get; set; }


        public int MessagesCount;
        public int TopicsCount;
    }

    [Table("topics")]
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("Id")]
        public User Author { get; set; }

        [ForeignKey("TopicId")]
        public List<TopicMessage> TopicMessages {get;set;}
    }

    public class TopicMessage
    {
        [Key]
        public int ID { get; set; }
        public int TopicId { get; set; }
        public User Author { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public int? ReplyTo{ get; set; }
    }

    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Username {get; set; }
        public string Email {get; set; }
        public string Role { get; set; } = "User";
    }

    public class LoginPasswordRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }

    public class Password
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [Column("Password")]
        public string Pwd { get; set; }
    }
}
