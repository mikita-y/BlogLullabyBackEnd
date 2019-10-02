namespace BlogLullaby.BLL.UserListService
{
    public class UserListCriterion
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string City { get; set; }
        public bool Online { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public UserListCriterion()
        {
            PageNumber = 0;
            PageSize = 10;
        }
    }
}
