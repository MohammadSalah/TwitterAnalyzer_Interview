using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TweetSharp;
using TwitterAnalyzer.Models;
using System.Web;
using TwitterAnalyzer.Models;
using TwitterAnalyzer.Utils;

namespace TwitterAnalyzer.Controllers
{
    public class TweetAnalayzerController : ApiController
    {
        ITweetRepository tweetRepository = new TweetRepository();
        IIntegrationService integrationService = new IntegrationService();


        public TweetAnalayzerController(ITweetRepository tweetRepository, IIntegrationService integrationService)
        {
            if (tweetRepository == null)
            {
                throw new ArgumentNullException("Tweet Repository");
            }
            if (integrationService == null)
            {
                throw new ArgumentNullException("Integration Service");
            }

            this.integrationService = integrationService;
        }


        // GET api/<controller>/5
        public Tweet Get(string id)
        {
            return tweetRepository.GetTweet(id);
        }

        public IEnumerable<Tweet> GetTwitterSearch(string query)
        {
            var encodedQuery = HttpContext.Current.Server.UrlEncode(query);
            var tweets = integrationService.SearchTwitter(encodedQuery);
            tweetRepository.InsertTweetBatch(tweets);
            return tweets;
        }
        
        // GET api/<controller>
        public IEnumerable<Tweet> GetAll()
        {
            return tweetRepository.GetAllTweets();
        }

        public IQueryable<Tweet> GetAll(string query)
        {
            return tweetRepository.GetAllTweets(query);
        }
        
        //get top 5 retweeted tweets
        public IQueryable<Tweet> GetTopRetweets()
        {
            return tweetRepository.GetAllTweets().OrderByDescending(x => x.RetweetCount).Take(5);
        }

        //get top 5 retweeted tweets for a searched key
        public IQueryable<Tweet> GetTopRetweets(string query)
        {
            return tweetRepository.GetAllTweets(query).OrderByDescending(x => x.RetweetCount).Take(5);
        }

        //get the tweets that were tweeted by a user with hieght count of friends
        public IEnumerable<User> GetTopUserFriends()
        {
            return tweetRepository.GetAllTweets().Select(x => x.User).ToList().Distinct(new UserEqualityComparer()).OrderByDescending(x => x.FriendsCount).Take(5);
        }

        public IEnumerable<User> GetTopFavorite(string query)
        {
            return tweetRepository.GetAllTweets(query).Select(x => x.User).ToList().Distinct(new UserEqualityComparer()).OrderByDescending(x => x.FriendsCount).Take(5);
        }

        // get top 5 tweeters (most tweets)
        public IEnumerable<User> GetTopAuther()
        {
            return tweetRepository.GetAllTweets().Select(x => x.User).ToList().Distinct(new UserEqualityComparer()).OrderByDescending(x => x.StatusesCount).Take(5);
        }

        public IEnumerable<User> GetTopAuther(string query)
        {
            return tweetRepository.GetAllTweets(query).Select(x => x.User).ToList().Distinct(new UserEqualityComparer()).OrderByDescending(x => x.StatusesCount).Take(5);
        }

    }
}