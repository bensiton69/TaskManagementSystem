namespace TaskManagementSystem.Core.Models
{
    public class RateObject<T>
    {
        public int Count { get; set; }
        public T Object { get; set; }
    }
}
