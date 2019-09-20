using BlogLullaby.DAL.DataStore.Entities;
using System;
using System.Linq;

namespace BlogLullaby.BLL.UserListService
{
    public static class UserListHelper
    {
        public static IQueryable<UserProfile> Sorting(this IQueryable<UserProfile> posts, UserListCriterion criterion)
        {
            return posts.OrderByDescending(x => x.Posts.Sum(y => y.Visits));
        }

        public static IQueryable<UserProfile> Filtering(this IQueryable<UserProfile> posts, UserListCriterion criterion)
        {
            if (criterion == null)
                return posts;
            if(!String.IsNullOrEmpty(criterion.Username))
                posts = posts.Where(x => x.Username.Contains(criterion.Username));
            if (!String.IsNullOrEmpty(criterion.Fullname))
                posts = posts.Where(x => $"{x.FirstName} {x.LastName}".Contains(criterion.Fullname));
            if (!String.IsNullOrEmpty(criterion.City))
                posts = posts.Where(x => x.City.Contains(criterion.City));
            if(criterion.Online)
                posts = posts.Where(x => (x.LastVisit - DateTime.Now).Duration().Minutes < 5);
            return posts;
        }
    }
}
