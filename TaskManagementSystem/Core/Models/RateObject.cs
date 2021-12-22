namespace TaskManagementSystem.Core.Models
{
    /// <summary>
    /// Uses to rate collection of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RateObject<T>
    {
        public int Count { get; set; }
        public T Object { get; set; }
    }
}
