using System;

namespace SED.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string UserImageUrl { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int SportEventId { get; set; }

        public virtual SportEvent SportEvent { get; set; }
    }
}
