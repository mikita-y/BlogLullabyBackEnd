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
            if (criterion.FilterBy == null)
                return posts;
            switch (criterion.FilterBy)
            {
                case FilterBy.City:
                    return posts.Where(x => x.City.Contains(criterion.SearchText));
                case FilterBy.Online:
                    return posts.Where(x => (x.LastVisit - DateTime.Now).Duration().Minutes < 5);
                case FilterBy.Username:
                    return posts.Where(x => x.Username.Contains(criterion.SearchText));
                case FilterBy.FullName:
                    return posts.Where(x => $"{x.FirstName} {x.LastName}".Contains(criterion.SearchText));
                default:
                    return posts;
            }
        }
    }
}
