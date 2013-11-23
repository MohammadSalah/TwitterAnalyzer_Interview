using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterAnalyzer.Models;

namespace TwitterAnalyzer.Models
{
    public interface IIntegrationService
    {
        IEnumerable<Tweet> SearchTwitter(string query);
    }
}