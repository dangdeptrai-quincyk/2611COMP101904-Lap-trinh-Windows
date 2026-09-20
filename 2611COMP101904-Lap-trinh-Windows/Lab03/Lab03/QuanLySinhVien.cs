using Lab03;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03
{
    public class QuanLySinhVien
    {
        private List<SinhVien> danhSach = new List<SinhVien>();
        public bool Them(SinhVien sv)
        {
            if (danhSach.Any(x => x.MaSinhVien.Equals(sv.MaSinhVien, StringComparison.OrdinalIgnoreCase)))
                return false;

            danhSach.Add(sv);
            return true;
        }
        public bool Sua(string maSinhVien, double diemMoi)
        {
            SinhVien sv = TimTheoMa(maSinhVien);
            if (sv == null)
                return false;

            sv.DiemTrungBinh = diemMoi;
            return true;
        }
        public bool Xoa(string maSinhVien)
        {
            SinhVien sv = TimTheoMa(maSinhVien);
            if (sv == null)
                return false;

            danhSach.Remove(sv);
            return true;
        }
        public SinhVien TimTheoMa(string maSinhVien)
        {
            return danhSach.FirstOrDefault(x => x.MaSinhVien.Equals(maSinhVien, StringComparison.OrdinalIgnoreCase));
        }
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return danhSach.Where(x => x.HoTen.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
        public List<SinhVien> SapXepTheoDiem()
        {
            return danhSach.OrderByDescending(x => x.DiemTrungBinh).ToList();
        }
        public List<SinhVien> LocSinhVienDat()
        {
            return danhSach.Where(x => x.DiemTrungBinh >= 5).ToList();
        }
        public List<SinhVien> LayDanhSach()
        {
            return danhSach;
        }
    }
}