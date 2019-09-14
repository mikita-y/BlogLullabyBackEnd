using BlogLullaby.DAL.DataStore.Entities;
using System.Linq;

namespace BlogLullaby.BLL.PostPreviewListService
{
    public static class PostListHelper
    {
        public static IQueryable<Post> Sorting(this IQueryable<Post> posts, PostListCriterion criterion)
        {
            switch (criterion.SortingBy)
            {
                case SortingBy.Newer:
                    return posts.OrderByDescending(x => x.Date);
                case SortingBy.Older:
                    return posts.OrderBy(x => x.Date);
                case SortingBy.Popular:
                    return posts.OrderByDescending(x => x.Visits);
            }
            return posts;
        }

        public static IQueryable<Post> Filtering(this IQueryable<Post> posts, PostListCriterion criterion)
        {
            if (criterion.FilterBy == null)
                return posts;
            switch (criterion.FilterBy)
            {
                case FilterBy.Title:
                    posts = posts.Where(x => x.Title.Contains(criterion.SearchText));
                    break;
                case FilterBy.Author:
                    posts = posts.Where(x => x.UserProfile.Username.Contains(criterion.SearchText));
                    break;
            }
            return posts;
        }
    }
}
