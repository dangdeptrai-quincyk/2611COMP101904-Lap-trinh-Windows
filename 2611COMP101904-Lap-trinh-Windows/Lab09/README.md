# Lab 09 - Thẻ sinh viên (WinForms)
Chạy: `dotnet run` (Windows, .NET 8). Phím tắt: F1 chọn ảnh, F4 xem trước in, F5 in, F6 làm mới.

## 8 test case tự kiểm thử
| # | Thao tác | Kết quả mong đợi |
|---|----------|------------------|
| 1 | Bấm Xem trước in khi để trống mã SV | MessageBox "Mã sinh viên không được rỗng" |
| 2 | Nhập mã SV, để trống họ tên, bấm In | MessageBox "Họ tên không được rỗng", không in |
| 3 | Nhập mã + tên, chưa chọn ảnh, Xem trước in | Báo "Vui lòng chọn ảnh đại diện" |
| 4 | Chọn file .txt trong OpenFileDialog | Dialog chỉ hiện JPG/JPEG/PNG/BMP |
| 5 | Chọn ảnh dọc (chân dung) | Ảnh hiển thị đúng tỉ lệ, không méo ở PictureBox và Panel |
| 6 | Gõ họ tên / đổi lớp, khoa | Panel preview cập nhật ngay |
| 7 | Nhập đủ dữ liệu, Xem trước in | PrintPreviewDialog hiện thẻ có khung, tiêu đề, ảnh, thông tin |
| 8 | Chọn ảnh khác rồi xóa file ảnh cũ trên đĩa | Xóa được file (chương trình không khóa file) |
| 9 | Bấm Làm mới | Dữ liệu và ảnh được xóa, form về trạng thái ban đầu |