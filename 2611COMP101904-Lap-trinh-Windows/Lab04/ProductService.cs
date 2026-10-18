namespace Lab04;
public class ProductService
{
    private readonly Repository<Product> _repo = new();
    public event Action<Product> ProductAdded;
    public event Action<Product> ProductRemoved;

    public void AddProduct(Product p)
    {
        if (_repo.FindById(p.Id) != null) throw new DuplicateProductException(p.Id);
        _repo.Add(p);
        ProductAdded?.Invoke(p);
    }
    public void RemoveProduct(string id)
    {
        var p = _repo.FindById(id) ?? throw new ProductNotFoundException(id);
        _repo.Remove(id);
        ProductRemoved?.Invoke(p);
    }
    public Product FindById(string id) => _repo.FindById(id);
    public IEnumerable<Product> GetAll() => _repo.GetAll();
    public IEnumerable<Product> Search(string keyword) =>
        Filter(p => p.TenSP.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    public IEnumerable<Product> Filter(Func<Product, bool> predicate) => _repo.Find(predicate);
    public decimal TotalValue() => _repo.GetAll().Sum(p => p.Price * p.Quantity);
}
