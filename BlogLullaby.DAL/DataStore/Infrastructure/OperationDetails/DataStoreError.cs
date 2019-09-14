
namespace BlogLullaby.DAL.DataStore.Infrastructure.OperationDetails
{
    public enum DataStoreErrorCode { NotExist }
    
    public class DataStoreError
    {
        public DataStoreErrorCode Code { get; set; }
        public string Description { get; set; }
    }
}
