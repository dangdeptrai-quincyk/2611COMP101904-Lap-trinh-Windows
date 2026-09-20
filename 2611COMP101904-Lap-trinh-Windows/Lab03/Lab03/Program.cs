using System;
using System.Collections.Generic;

namespace Lab03
{
    class Program
    {
        static QuanLySinhVien quanLy = new QuanLySinhVien();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            int luaChon;

            do
            {
                HienThiMenu();
                string input = Console.ReadLine();

                if (!int.TryParse(input, out luaChon))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ, Vui lòng nhập lại");
                    continue;
                }

                switch (luaChon)
                {
                    case 1:
                        ThemSinhVien();
                        break;
                    case 2:
                        XuatDanhSach(quanLy.LayDanhSach());
                        break;
                    case 3:
                        TimTheoMa();
                        break;
                    case 4:
                        TimTheoTen();
                        break;
                    case 5:
                        SuaDiem();
                        break;
                    case 6:
                        XoaSinhVien();
                        break;
                    case 7:
                        Console.WriteLine("Danh sách sau khi sắp xếp theo điểm giảm dần:");
                        XuatDanhSach(quanLy.SapXepTheoDiem());
                        break;
                    case 8:
                        Console.WriteLine("Danh sách sinh viên đạt:");
                        XuatDanhSach(quanLy.LocSinhVienDat());
                        break;
                    case 0:
                        Console.WriteLine("Chương trình kết thúc");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại");
                        break;
                }

            } while (luaChon != 0);
        }

        static void HienThiMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== QUẢN LÝ SINH VIÊN =====");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Xuất danh sách");
            Console.WriteLine("3. Tìm sinh viên theo mã");
            Console.WriteLine("4. Tìm sinh viên theo tên");
            Console.WriteLine("5. Sửa điểm trung bình");
            Console.WriteLine("6. Xóa sinh viên");
            Console.WriteLine("7. Sắp xếp theo điểm giảm dần");
            Console.WriteLine("8. Lọc sinh viên đạt");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn chức năng: ");
        }
        static void ThemSinhVien()
        {
            Console.Write("Nhập mã sinh viên: ");
            string ma = Console.ReadLine().Trim();

            Console.Write("Nhập họ tên: ");
            string hoTen = Console.ReadLine().Trim();

            DateTime ngaySinh = NhapNgay("Nhập ngày sinh (dd/MM/yyyy): ");

            Console.Write("Nhập mã lớp: ");
            string maLop = Console.ReadLine().Trim();

            double diem = NhapDiem("Nhập điểm trung bình (0 - 10): ");

            try
            {
                SinhVien sv = new SinhVien(ma, hoTen, ngaySinh, maLop, diem);
                bool thanhCong = quanLy.Them(sv);

                if (thanhCong)
                    Console.WriteLine($"Thêm thành công. Xếp loại: {sv.XepLoai()}");
                else
                    Console.WriteLine($"Mã sinh viên '{ma}' đã tồn tại. Không thể thêm");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        static void XuatDanhSach(List<SinhVien> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng.");
                return;
            }

            foreach (SinhVien sv in danhSach)
            {
                Console.WriteLine(sv.LayThongTin());
            }
        }

        static void TimTheoMa()
        {
            Console.Write("Nhập mã sinh viên cần tìm: ");
            string ma = Console.ReadLine().Trim();

            SinhVien sv = quanLy.TimTheoMa(ma);
            if (sv != null)
                Console.WriteLine(sv.LayThongTin());
            else
                Console.WriteLine("Không tìm thấy sinh viên có mã này.");
        }

        static void TimTheoTen()
        {
            Console.Write("Nhập từ khóa họ tên cần tìm: ");
            string tuKhoa = Console.ReadLine().Trim();

            List<SinhVien> ketQua = quanLy.TimTheoTen(tuKhoa);
            if (ketQua.Count == 0)
                Console.WriteLine("Không tìm thấy sinh viên nào.");
            else
                XuatDanhSach(ketQua);
        }

        static void SuaDiem()
        {
            Console.Write("Nhập mã sinh viên cần sửa điểm: ");
            string ma = Console.ReadLine().Trim();

            SinhVien sv = quanLy.TimTheoMa(ma);
            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên có mã này.");
                return;
            }

            double diemMoi = NhapDiem("Nhập điểm trung bình mới (0 - 10): ");

            try
            {
                quanLy.Sua(ma, diemMoi);
                Console.WriteLine("Cập nhật điểm thành công");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }

        static void XoaSinhVien()
        {
            Console.Write("Nhập mã sinh viên cần xóa: ");
            string ma = Console.ReadLine().Trim();

            bool thanhCong = quanLy.Xoa(ma);
            if (thanhCong)
                Console.WriteLine("Xóa thành công");
            else
                Console.WriteLine("Không tìm thấy sinh viên có mã này");
        }
        static DateTime NhapNgay(string message)
        {
            DateTime ngay;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out ngay))
                    return ngay;
                Console.WriteLine("Định dạng ngày không hợp lệ, vui lòng nhập lại (ví dụ: 15/09/2005)");
            }
        }
        static double NhapDiem(string message)
        {
            double diem;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (double.TryParse(input, out diem) && diem >= 0 && diem <= 10)
                    return diem;
                Console.WriteLine("Điểm không hợp lệ, vui lòng nhập lại (số từ 0 đến 10)");
            }
        }
    }
}