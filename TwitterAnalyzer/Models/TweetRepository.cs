using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using TwitterAnalyzer.Models;


namespace TwitterAnalyzer.Models
{
    public class TweetRepository :ITweetRepository
    {

        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Tweet> _tweets;

        public TweetRepository()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["MongoDBConnectionString"]);

            _server = client.GetServer();
            _database = _server.GetDatabase(ConfigurationManager.AppSettings["TwitterMonitorDatabaseName"].ToString());
            _tweets = _database.GetCollection<Tweet>(ConfigurationManager.AppSettings["TweetCollectionName"].ToString());
        }

        public IQueryable<Tweet> GetAllTweets(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return _tweets.FindAll().AsQueryable();
            }
            else
            {
                var query = Query<Tweet>.EQ(e => e.SearchKey, key);
                return _tweets.Find(query).AsQueryable();
            }
        }

        public Tweet GetTweet(string id)
        {
            var query = Query<Tweet>.EQ(e => e.IdStr, id);
            return _tweets.Find(query).FirstOrDefault();
        }

        public void InsertTweet(Tweet tweet)
        {
            _tweets.Insert(tweet);
        }

        public void InsertTweetBatch(IEnumerable<Tweet> tweets)
        {
            if (tweets.Count() > 0)
            {
                _tweets.InsertBatch<Tweet>(tweets);
            }
        }
    }
}