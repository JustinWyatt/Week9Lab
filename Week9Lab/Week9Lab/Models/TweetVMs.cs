using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week9Lab.Models
{
    public class TweetVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
    }
}