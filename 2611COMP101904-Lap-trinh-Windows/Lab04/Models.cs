namespace Lab04;
public interface IEntity { string Id { get; } }
public class Product : IEntity
{
    private decimal _price; private int _quantity;
    public string MaSP { get; set; }
    public string TenSP { get; set; }
    public decimal Price
    {
        get => _price;
        set { if (value < 0) throw new ArgumentException("Đơn giá không được âm."); _price = value; }
    }
    public int Quantity
    {
        get => _quantity;
        set { if (value < 0) throw new ArgumentException("Số lượng không được âm."); _quantity = value; }
    }
    public string Id => MaSP;

    public Product(string maSP, string tenSP, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(maSP)) throw new ArgumentException("Mã sản phẩm không được rỗng.");
        if (string.IsNullOrWhiteSpace(tenSP)) throw new ArgumentException("Tên sản phẩm không được rỗng.");
        MaSP = maSP.Trim(); TenSP = tenSP.Trim(); Price = price; Quantity = quantity;
    }
    public override string ToString() => $"{MaSP,-8} {TenSP,-25} {Price,12:N0} {Quantity,6}";
}
public class DuplicateProductException : Exception
{
    public DuplicateProductException(string id) : base($"Mã sản phẩm '{id}' đã tồn tại.") { }
}
public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string id) : base($"Không tìm thấy sản phẩm có mã '{id}'.") { }
}
