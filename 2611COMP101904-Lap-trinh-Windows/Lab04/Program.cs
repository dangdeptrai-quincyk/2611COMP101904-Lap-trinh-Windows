using System.Text;
using Lab04;
Console.OutputEncoding = Encoding.UTF8;
var service = new ProductService();
service.ProductAdded += p => Console.WriteLine($"[EVENT] Đã thêm sản phẩm: {p.TenSP}");
service.ProductRemoved += p => Console.WriteLine($"[EVENT] Đã xóa sản phẩm: {p.TenSP}");
while (true)
{
    ShowMenu();
    string choice = Console.ReadLine()?.Trim();
    try
    {
        switch (choice)
        {
            case "1": AddProduct(); break;
            case "2": PrintList(service.GetAll()); break;
            case "3": FindByCode(); break;
            case "4": SearchByName(); break;
            case "5": FilterByPrice(); break;
            case "6": RemoveProduct(); break;
            case "7": Console.WriteLine($"Tổng giá trị kho: {service.TotalValue():N0}"); break;
            case "0": return;
            default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
        }
    }
    catch (DuplicateProductException ex) { Console.WriteLine("Lỗi trùng mã: " + ex.Message); }
    catch (ProductNotFoundException ex) { Console.WriteLine("Lỗi không tồn tại: " + ex.Message); }
    catch (ArgumentException ex) { Console.WriteLine("Dữ liệu không hợp lệ: " + ex.Message); }
    catch (Exception ex) { Console.WriteLine("Lỗi không mong muốn: " + ex.Message); }
}
void ShowMenu()
{
    Console.WriteLine("\n===== PRODUCT MANAGER =====");
    Console.WriteLine("1. Them san pham\n2. Xuat danh sach\n3. Tim theo ma\n4. Tim theo ten");
    Console.WriteLine("5. Loc theo khoang gia\n6. Xoa san pham\n7. Tinh tong gia tri kho\n0. Thoat");
    Console.Write("Chon: ");
}
string ReadString(string label)
{
    Console.Write(label); return Console.ReadLine() ?? "";
}
decimal ReadDecimal(string label)
{
    while (true)
    {
        Console.Write(label);
        if (decimal.TryParse(Console.ReadLine(), out var v)) return v;
        Console.WriteLine("Vui lòng nhập số hợp lệ.");
    }
}
int ReadInt(string label)
{
    while (true)
    {
        Console.Write(label);
        if (int.TryParse(Console.ReadLine(), out var v)) return v;
        Console.WriteLine("Vui lòng nhập số nguyên hợp lệ.");
    }
}
void AddProduct()
{
    string ma = ReadString("Mã SP: "), ten = ReadString("Tên SP: ");
    decimal gia = ReadDecimal("Đơn giá: ");
    int sl = ReadInt("Số lượng: ");
    service.AddProduct(new Product(ma, ten, gia, sl));
}
void PrintList(IEnumerable<Product> list)
{
    var items = list.ToList();
    if (items.Count == 0) { Console.WriteLine("Danh sách rỗng / không có kết quả."); return; }
    Console.WriteLine($"{"Mã",-8} {"Tên",-25} {"Đơn giá",12} {"SL",6}");
    items.ForEach(Console.WriteLine);
}
void FindByCode()
{
    var p = service.FindById(ReadString("Nhập mã: "));
    if (p == null) Console.WriteLine("Không tìm thấy."); else Console.WriteLine(p);
}
void SearchByName() => PrintList(service.Search(ReadString("Từ khóa: ")));
void FilterByPrice()
{
    decimal min = ReadDecimal("Giá nhỏ nhất: "), max = ReadDecimal("Giá lớn nhất: ");
    if (min > max) (min, max) = (max, min);
    Func<Product, bool> inRange = p => p.Price >= min && p.Price <= max;
    PrintList(service.Filter(inRange));
}
void RemoveProduct() => service.RemoveProduct(ReadString("Mã cần xóa: "));