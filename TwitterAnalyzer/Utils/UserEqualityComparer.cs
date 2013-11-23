using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterAnalyzer.Models;

namespace TwitterAnalyzer.Utils
{
    public class UserEqualityComparer : IEqualityComparer<User>
    {

        public bool Equals(User user1, User user2)
        {
            if (user1.Id == user2.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(User user)
        {
            double hCode = user.Id;
            return hCode.GetHashCode();
        }

    }
}
