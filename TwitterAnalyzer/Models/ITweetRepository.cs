using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterAnalyzer.Models;

namespace TwitterAnalyzer.Models
{
    public interface ITweetRepository
    {
        IQueryable<Tweet> GetAllTweets(string query = null);

        Tweet GetTweet(string Id);

        void InsertTweet(Tweet tweet);

        void InsertTweetBatch(IEnumerable<Tweet> tweets);
    }
}