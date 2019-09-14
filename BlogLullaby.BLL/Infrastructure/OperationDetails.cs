using System.Collections.Generic;

namespace BlogLullaby.BLL.Infrastructure
{
    public class OperationDetails
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Descriptions { get; set; }

        public OperationDetails(bool success, IEnumerable<string> descriptions = null)
        {
            IsSuccess = success;
            Descriptions = descriptions;
        }

        public OperationDetails()
        { }
    }
}
