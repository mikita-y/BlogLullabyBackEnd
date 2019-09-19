namespace BlogLullaby.BLL.UserListService
{
    public enum FilterBy { Username, FullName, City, Online };

    public class UserListCriterion
    {
        public FilterBy? FilterBy { get; set; }
        public string SearchText { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public UserListCriterion()
        {
            PageNumber = 0;
            PageSize = 10;
        }
    }
}
