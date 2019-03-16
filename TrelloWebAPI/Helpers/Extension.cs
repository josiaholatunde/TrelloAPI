using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TrelloWebAPI.Helpers
{
     public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalPages, int totalItems)
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            var paginationHeader = new PaginationHeaders(currentPage, itemsPerPage, totalPages, totalItems);
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}