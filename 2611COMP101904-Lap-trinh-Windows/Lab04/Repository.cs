namespace Lab04;
public class Repository<T> where T : IEntity
{
    private readonly List<T> _items = new();
    public void Add(T item) => _items.Add(item);
    public bool Remove(string id)
    {
        var item = FindById(id);
        return item != null && _items.Remove(item);
    }
    public T FindById(string id) =>
        _items.FirstOrDefault(x => string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase));
    public IEnumerable<T> Find(Func<T, bool> predicate) => _items.Where(predicate).ToList();
    public IEnumerable<T> GetAll() => _items.ToList();
}