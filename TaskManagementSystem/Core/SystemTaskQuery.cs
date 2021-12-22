namespace TaskManagementSystem.Core
{
    public class SystemTaskQuery : IQueryObject
    {
        public int? UrgentLevel { get; set; }
        public int? Status { get; set; }
        public string UserName { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}