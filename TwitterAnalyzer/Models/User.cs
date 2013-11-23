using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterAnalyzer.Models
{
    public class User
    {
        public double Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FavouritesCount { get; set; }
        public int FollowersCount { get; set; }
        public int FriendsCount { get; set; }
        public string Language { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public int StatusesCount { get; set; }
    }
}
