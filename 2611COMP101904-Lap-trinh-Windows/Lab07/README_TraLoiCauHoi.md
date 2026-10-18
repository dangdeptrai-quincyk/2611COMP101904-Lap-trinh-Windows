# Trả lời câu hỏi nộp kèm - Buổi 7

1. **SDI và MDI khác nhau thế nào?**
   SDI (Single Document Interface): mỗi cửa sổ là một form độc lập, mỗi lần chỉ làm việc trên một tài liệu/cửa sổ chính (ví dụ Notepad). MDI (Multiple Document Interface): một form cha (`IsMdiContainer = true`) chứa nhiều form con bên trong, dùng chung menu/status bar (ví dụ Excel đời cũ).

2. **Vì sao Form đăng nhập nên dùng ShowDialog()?**
   `ShowDialog()` mở form dạng modal: chặn code phía sau và không cho thao tác form khác cho đến khi đóng. Nhờ đó chương trình chỉ mở FrmMain khi `DialogResult.OK` (đăng nhập thành công), và có thể lấy kết quả/thông tin user ngay sau khi form đóng.

3. **Dữ liệu user truyền từ FrmLogin sang FrmMain bằng cách nào?**
   FrmLogin có property `LoggedInUser` (set khi đăng nhập đúng). Trong `Program.Main`, sau khi `ShowDialog()` trả về OK, lấy `login.LoggedInUser` và truyền vào constructor `new FrmMain(user)`; FrmMain hiển thị tên + vai trò trên StatusStrip.

4. **Bấm menu nhiều lần, làm sao không mở trùng Form?**
   Duyệt `MdiChildren.OfType<T>().FirstOrDefault()`; nếu đã có thì `Activate()` (và restore nếu đang minimize), nếu chưa có mới tạo form mới với `MdiParent = this`. (Xem hàm `OpenChild<T>()` trong FrmMain.)

5. **Khi nào dùng constructor, khi nào dùng property để truyền dữ liệu giữa các Form?**
   Constructor: dữ liệu *bắt buộc* phải có để form hoạt động, truyền một lần lúc khởi tạo (vd `FrmMain(User)`). Property: dữ liệu *tùy chọn* hoặc cần lấy kết quả ngược lại / thay đổi sau khi tạo form (vd `LoginForm.LoggedInUser`, hoặc set `frm.Product = p` trước khi `Show()`).
