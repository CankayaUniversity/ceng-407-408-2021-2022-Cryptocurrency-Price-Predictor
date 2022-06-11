namespace Shared.Entities.Common
{
    public class SearchEntity
    {
        public SortingPaging? SortingPaging { get; set; }
        public string? SearchText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
