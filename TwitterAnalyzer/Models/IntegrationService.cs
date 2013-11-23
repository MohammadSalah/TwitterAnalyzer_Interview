using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TweetSharp;

namespace TwitterAnalyzer.Models
{
    public class IntegrationService :IIntegrationService
    {
        private static string _consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private static string _consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private static string _accessToken = ConfigurationManager.AppSettings["AccessToken"];
        private static string _AccessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];
        private static int _resultCount = int.Parse(ConfigurationManager.AppSettings["ResultCount"]);

        public IntegrationService()
        {

        }

        public IEnumerable<Tweet> SearchTwitter(string query)
        {
            TwitterClientInfo twitterClientInfo = new TwitterClientInfo();
            twitterClientInfo.ConsumerKey = _consumerKey;
            twitterClientInfo.ConsumerSecret = _consumerSecret;

            TwitterService twitterService = new TwitterService(twitterClientInfo);
            twitterService.AuthenticateWith(_accessToken, _AccessTokenSecret);
            var options = new SearchOptions { Q = query, Count = _resultCount };
            var SearchResult = twitterService.Search(options);

            var tweets = new List<Tweet>();

            foreach (var status in SearchResult.Statuses)
            {
                var tweet = new Tweet();
                tweet.SearchKey = query;
                tweet.CreatedDate = status.CreatedDate;
                tweet.IdStr = status.IdStr;
                tweet.RetweetCount = status.RetweetCount;
                tweet.Text = status.Text;
                tweet.User = new User()
                {
                    Id = status.User.Id,
                    CreatedDate = status.User.CreatedDate,
                    FavouritesCount = status.User.FavouritesCount,
                    FollowersCount = status.User.FollowersCount,
                    FriendsCount = status.User.FriendsCount,
                    Language = status.User.Language,
                    Location = status.User.Location,
                    Name = status.User.Name,
                    ScreenName = status.User.ScreenName,
                    StatusesCount = status.User.StatusesCount,
                };

                tweets.Add(tweet);
            }

            return tweets;
        }
    }
}