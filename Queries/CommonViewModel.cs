namespace Queries;

public class Paginated<T>
{
    public int Total { get; set; }
    public ICollection<T>? Items { get; set; }
}
