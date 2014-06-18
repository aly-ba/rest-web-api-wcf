using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stats.Models
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}