using TNServicesApp_API.DataAccess;
using TNServicesApp_API.Models;
using TNServicesApp_API.Repositories.IRepositories;

namespace TNServicesApp_API.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IDataAccess _dataAccess;

        public DashboardRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<DataAccessResponse> GetSslcDataAsync()
        {
            var request = new QueryInputRequest
            {
                QueryJSON = null,
                QueryType = "SSLC_Summary"
            };
            return await _dataAccess.ExecuteStoredProcedureAsync("TNS_001_SP_SSLCSummary", request);
        }

        public async Task<DataAccessResponse> GetHscDataAsync()
        {
            var request = new QueryInputRequest
            {
                QueryJSON = null,
                QueryType = "HSC_Summary"
            };
            return await _dataAccess.ExecuteStoredProcedureAsync("TNS_002_SP_HSCSummary", request);
        }

        public async Task<DataAccessResponse> SSLCInsertDataAsync(StudentsInsertRequest sslcData)
        {
            var request = new QueryInputRequest
            {
                QueryJSON = sslcData,
                QueryType = "insert"
            };
            return await _dataAccess.ExecuteStoredProcedureAsync("TNS_003_SP_SSLC_Register", request);
        }

        public async Task<DataAccessResponse> HSCInsertDataAsync(HSC_InsertRequest hscData)
        {
            var request = new QueryInputRequest
            {
                QueryJSON = hscData,
                QueryType = "insert"
            };
            return await _dataAccess.ExecuteStoredProcedureAsync("TNS_004_SP_HSLC_Register", request);
        }
    }
}
