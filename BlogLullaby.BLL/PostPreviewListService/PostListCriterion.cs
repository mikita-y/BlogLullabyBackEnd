
namespace BlogLullaby.BLL.PostPreviewListService
{
    public enum SortingBy { Popular, Newer, Older};
    public enum FilterBy { Title, Author };

    public class PostListCriterion
    {
        public SortingBy SortingBy { get; set; }
        public FilterBy? FilterBy { get; set; }
        public string SearchText { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool ExactMatch { get; set; }

        public PostListCriterion()
        {
            PageNumber = 0;
            PageSize = 10;
        }
    }
}
