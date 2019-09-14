using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.DAL.IdentityManager.Infrastucture
{
    public class IdentityDetails
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<Error> ErrorList { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
