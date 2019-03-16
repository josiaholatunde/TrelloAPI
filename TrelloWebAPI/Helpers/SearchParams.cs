using TrelloWebAPI.Models;

namespace TrelloWebAPI.Data
{
    public class SearchParams
    {
        private readonly int MaxPageSize = 50;
        private int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public BookingSubjectType BookingType { get; set; }
        public string NameOrLocation { get; set; }
        
    }
}