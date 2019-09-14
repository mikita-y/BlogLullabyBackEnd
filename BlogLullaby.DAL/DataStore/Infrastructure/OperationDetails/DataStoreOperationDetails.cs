using System.Collections.Generic;

namespace BlogLullaby.DAL.DataStore.Infrastructure.OperationDetails
{
    public class DataStoreOperationDetails
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<DataStoreError> DataStoreErrors { get; private set; }
        public DataStoreOperationDetails()
        {
            IsSuccess = true;
            DataStoreErrors = new List<DataStoreError>();
        }
        public void AddDataStoreError(DataStoreError error)
        {
            IsSuccess = false;
            ((List<DataStoreError>)DataStoreErrors).Add(error);
        }
    }
}
