using System;

namespace Lab03
{
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }

        private double diemTrungBinh;
        public double DiemTrungBinh
        {
            get { return diemTrungBinh; }
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException(nameof(value), "Điểm trung bình phải nằm trong khoảng từ 0 đến 10");
                diemTrungBinh = value;
            }
        }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }
        public string XepLoai()
        {
            if (diemTrungBinh >= 8)
                return "Giỏi";
            else if (diemTrungBinh >= 6.5)
                return "Khá";
            else if (diemTrungBinh >= 5)
                return "Trung bình";
            else
                return "Yếu";
        }
        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien} | Họ tên: {HoTen} | Ngày sinh: {NgaySinh:dd/MM/yyyy} | " +
                   $"Lớp: {MaLop} | Điểm TB: {diemTrungBinh:0.00} | Xếp loại: {XepLoai()}";
        }
    }
}