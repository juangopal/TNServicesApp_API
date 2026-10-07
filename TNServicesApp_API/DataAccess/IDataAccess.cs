using TNServicesApp_API.Models;

namespace TNServicesApp_API.DataAccess
{
    public interface IDataAccess
    {
        Task<DataAccessResponse> ExecuteStoredProcedureAsync(string storedProcedureName, QueryInputRequest parameters, CancellationToken cancellationToken=default);
    }
}
