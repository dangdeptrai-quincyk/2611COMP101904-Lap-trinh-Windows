using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QuanLyNhanVien
{
    class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public double LuongCoBan { get; set; }

        public NhanVien(string maNV, string hoTen, double luongCoBan)
        {
            MaNV = maNV;
            HoTen = hoTen;
            LuongCoBan = luongCoBan > 0 ? luongCoBan : 0;
        }

        public virtual double TinhLuong()
        {
            return LuongCoBan;
        }

        public virtual void HienThiThongTin()
        {
            Console.WriteLine($"Mã NV: {MaNV,-6} | Họ tên: {HoTen,-20} | Loại: Nhân viên      | Lương: {TinhLuong(),15:N0} VNĐ");
        }
    }

    class NhanVienVanPhong : NhanVien
    {
        public int SoNgayLamViec { get; set; }
        private const double DonGiaNgayCong = 200000;

        public NhanVienVanPhong(string maNV, string hoTen, double luongCoBan, int soNgayLamViec)
            : base(maNV, hoTen, luongCoBan)
        {
            SoNgayLamViec = (soNgayLamViec >= 0 && soNgayLamViec <= 31) ? soNgayLamViec : 0;
        }

        public override double TinhLuong()
        {
            return LuongCoBan + SoNgayLamViec * DonGiaNgayCong;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine($"Mã NV: {MaNV,-6} | Họ tên: {HoTen,-20} | Loại: Văn phòng    | Số ngày làm: {SoNgayLamViec,3} | Lương: {TinhLuong(),15:N0} VNĐ");
        }
    }

    class NhanVienKinhDoanh : NhanVien
    {
        public double DoanhSo { get; set; }
        private const double TiLeHoaHong = 0.05;

        public NhanVienKinhDoanh(string maNV, string hoTen, double luongCoBan, double doanhSo)
            : base(maNV, hoTen, luongCoBan)
        {
            DoanhSo = doanhSo >= 0 ? doanhSo : 0;
        }

        public override double TinhLuong()
        {
            return LuongCoBan + TiLeHoaHong * DoanhSo;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine($"Mã NV: {MaNV,-6} | Họ tên: {HoTen,-20} | Loại: Kinh doanh   | Doanh số: {DoanhSo,12:N0} | Lương: {TinhLuong(),15:N0} VNĐ");
        }
    }

    class NhanVienThoiVu : NhanVien
    {
        public double SoGioLam { get; set; }
        public double LuongTheoGio { get; set; }

        public NhanVienThoiVu(string maNV, string hoTen, double soGioLam, double luongTheoGio)
            : base(maNV, hoTen, 0)
        {
            SoGioLam = soGioLam >= 0 ? soGioLam : 0;
            LuongTheoGio = luongTheoGio >= 0 ? luongTheoGio : 0;
        }

        public override double TinhLuong()
        {
            return SoGioLam * LuongTheoGio;
        }

        public override void HienThiThongTin()
        {
            Console.WriteLine($"Mã NV: {MaNV,-6} | Họ tên: {HoTen,-20} | Loại: Thời vụ      | Số giờ làm: {SoGioLam,6:N1} | Lương: {TinhLuong(),15:N0} VNĐ");
        }
    }

    class Program
    {
        static List<NhanVien> danhSach = new List<NhanVien>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== NHẬP DANH SÁCH NHÂN VIÊN BAN ĐẦU (ít nhất 5 người) ===");
            NhapDanhSachBanDau(5);

            bool tiepTuc = true;
            while (tiepTuc)
            {
                HienThiMenu();
                string luaChon = Console.ReadLine();
                Console.WriteLine();

                switch (luaChon)
                {
                    case "1":
                        XuatDanhSach();
                        break;
                    case "2":
                        TimTheoMa();
                        break;
                    case "3":
                        TimLuongCaoNhat();
                        break;
                    case "4":
                        TinhTongLuong();
                        break;
                    case "5":
                        ThemNhanVien();
                        break;
                    case "0":
                        tiepTuc = false;
                        Console.WriteLine("Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void HienThiMenu()
        {
            Console.WriteLine("========== MENU ==========");
            Console.WriteLine("1. Xuất danh sách nhân viên");
            Console.WriteLine("2. Tìm nhân viên theo mã");
            Console.WriteLine("3. Tìm nhân viên có lương cao nhất");
            Console.WriteLine("4. Tính tổng lương công ty phải trả");
            Console.WriteLine("5. Thêm nhân viên mới");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn chức năng: ");
        }

        static void NhapDanhSachBanDau(int soLuong)
        {
            for (int i = 1; i <= soLuong; i++)
            {
                Console.WriteLine($"\n--- Nhập nhân viên thứ {i} ---");
                ThemNhanVien();
            }
        }

        static void ThemNhanVien()
        {
            Console.WriteLine("Chọn loại nhân viên:");
            Console.WriteLine("  1. Nhân viên văn phòng");
            Console.WriteLine("  2. Nhân viên kinh doanh");
            Console.WriteLine("  3. Nhân viên thời vụ (bonus)");
            Console.Write("Loại: ");
            string loai = Console.ReadLine();

            Console.Write("Mã nhân viên: ");
            string ma = Console.ReadLine();

            Console.Write("Họ tên: ");
            string hoTen = Console.ReadLine();

            switch (loai)
            {
                case "1":
                    {
                        double luongCoBan = NhapSo("Lương cơ bản (> 0): ");
                        int soNgay = (int)NhapSo("Số ngày làm việc (0-31): ");
                        danhSach.Add(new NhanVienVanPhong(ma, hoTen, luongCoBan, soNgay));
                        break;
                    }
                case "2":
                    {
                        double luongCoBan = NhapSo("Lương cơ bản (> 0): ");
                        double doanhSo = NhapSo("Doanh số (>= 0): ");
                        danhSach.Add(new NhanVienKinhDoanh(ma, hoTen, luongCoBan, doanhSo));
                        break;
                    }
                case "3":
                    {
                        double soGio = NhapSo("Số giờ làm: ");
                        double luongGio = NhapSo("Lương theo giờ: ");
                        danhSach.Add(new NhanVienThoiVu(ma, hoTen, soGio, luongGio));
                        break;
                    }
                default:
                    Console.WriteLine("Loại không hợp lệ, hủy thêm nhân viên này.");
                    break;
            }
        }

        static double NhapSo(string thongBao)
        {
            double ketQua;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out ketQua))
                    return ketQua;
                Console.WriteLine("Giá trị không hợp lệ, vui lòng nhập lại (dùng dấu '.' cho số thập phân).");
            }
        }

        static void XuatDanhSach()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống.");
                return;
            }
            Console.WriteLine("--- DANH SÁCH NHÂN VIÊN ---");
            foreach (var nv in danhSach)
            {
                nv.HienThiThongTin();
            }
        }

        static void TimTheoMa()
        {
            Console.Write("Nhập mã nhân viên cần tìm: ");
            string ma = Console.ReadLine();
            var nv = danhSach.FirstOrDefault(x => x.MaNV.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (nv == null)
            {
                Console.WriteLine("Không tìm thấy nhân viên có mã: " + ma);
            }
            else
            {
                Console.WriteLine("Tìm thấy:");
                nv.HienThiThongTin();
            }
        }

        static void TimLuongCaoNhat()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống.");
                return;
            }
            var nvCaoNhat = danhSach.OrderByDescending(x => x.TinhLuong()).First();
            Console.WriteLine("Nhân viên có lương cao nhất:");
            nvCaoNhat.HienThiThongTin();
        }

        static void TinhTongLuong()
        {
            double tong = danhSach.Sum(x => x.TinhLuong());
            Console.WriteLine($"Tổng lương công ty phải trả: {tong:N0} VNĐ");
        }
    }
}