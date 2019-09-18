using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.BLL.UserListService
{
    public class UserListCriterion
    {
        public string Username { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public UserListCriterion()
        {
            PageNumber = 0;
            PageSize = 10;
        }
    }
}
