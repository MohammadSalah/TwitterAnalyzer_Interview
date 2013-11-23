using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TwitterAnalyzer.Models
{
    public class Tweet
    {
        [BsonId]
        ObjectId Id { get; set; }       
        public DateTime CreatedDate { get; set; }       
        public string IdStr { get; set; }
        public int RetweetCount { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public string SearchKey { get; set; }
    }
}