namespace Shared.Entities.Common
{
    public class Sort
    {
        public enum SortOrder
        {
            Ascending,
            Descending
        }

        public string ColumnName { get; set; }

        public SortOrder ColumnOrder { get; set; }
    }
}
