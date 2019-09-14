using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.DAL.DataStore.Infrastructure.OperationDetails
{
    public static class DataStoreErrorExtencionMetods
    {
        public static DataStoreError NotExist(this DataStoreError error)
        {
            error.Code = DataStoreErrorCode.NotExist;
            error.Description = "Object not existing";
            return error;
        }
    }
}
