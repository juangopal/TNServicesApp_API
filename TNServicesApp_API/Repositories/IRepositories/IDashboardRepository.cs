using TNServicesApp_API.Models;

namespace TNServicesApp_API.Repositories.IRepositories
{
    public interface IDashboardRepository
    {
        Task<DataAccessResponse> GetSslcDataAsync();
        Task<DataAccessResponse> GetHscDataAsync();
        Task<DataAccessResponse> SSLCInsertDataAsync(StudentsInsertRequest sslcData);
        Task<DataAccessResponse> HSCInsertDataAsync(HSC_InsertRequest hscData);
    }
}
