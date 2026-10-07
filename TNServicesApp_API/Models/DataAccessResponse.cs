using System.Text.Json;

namespace TNServicesApp_API.Models
{
    public class DataAccessResponse
    {
        public JsonElement? OutJSON { get; set; }
        public string? OutStatus { get; set; }
    }
}
