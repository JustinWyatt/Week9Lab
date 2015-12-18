using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week9Lab.Models
{
    public class Follower  
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}   