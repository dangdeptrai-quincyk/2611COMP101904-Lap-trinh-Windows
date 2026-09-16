using System;

namespace Lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] mang = null;
            bool daNhapMang = false;
            int luaChon;

            do
            {
                Console.WriteLine();
                Console.WriteLine("===== MENU =====");
                Console.WriteLine("1. Nhap mang");
                Console.WriteLine("2. Xuat mang");
                Console.WriteLine("3. Tinh tong");
                Console.WriteLine("4. Tim max/min");
                Console.WriteLine("5. Dem chan/le");
                Console.WriteLine("6. Sap xep tang dan");
                Console.WriteLine("7. Tim kiem");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out luaChon))
                {
                    Console.WriteLine("Lua chon khong hop le. Vui long nhap lai.");
                    continue;
                }

                switch (luaChon)
                {
                    case 1:
                        mang = NhapMang();
                        daNhapMang = true;
                        break;
                    case 2:
                        if (KiemTraDaNhapMang(daNhapMang))
                            XuatMang(mang);
                        break;
                    case 3:
                        if (KiemTraDaNhapMang(daNhapMang))
                            Console.WriteLine($"Tong cac phan tu: {TinhTong(mang)}");
                        break;
                    case 4:
                        if (KiemTraDaNhapMang(daNhapMang))
                        {
                            Console.WriteLine($"Gia tri lon nhat: {TimMax(mang)}");
                            Console.WriteLine($"Gia tri nho nhat: {TimMin(mang)}");
                        }
                        break;
                    case 5:
                        if (KiemTraDaNhapMang(daNhapMang))
                        {
                            Console.WriteLine($"So luong phan tu chan: {DemChan(mang)}");
                            Console.WriteLine($"So luong phan tu le: {DemLe(mang)}");
                        }
                        break;
                    case 6:
                        if (KiemTraDaNhapMang(daNhapMang))
                        {
                            SapXepTangDan(mang);
                            Console.WriteLine("Da sap xep tang dan:");
                            XuatMang(mang);
                        }
                        break;
                    case 7:
                        if (KiemTraDaNhapMang(daNhapMang))
                        {
                            int x = NhapSoNguyen("Nhap gia tri can tim x: ");
                            int viTri = TimKiem(mang, x);
                            if (viTri != -1)
                                Console.WriteLine($"Co tim thay, vi tri dau tien la {viTri} (tinh tu 0).");
                            else
                                Console.WriteLine("Khong tim thay.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Chuong trinh ket thuc.");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                        break;
                }

            } while (luaChon != 0);
        }
        static bool KiemTraDaNhapMang(bool daNhap)
        {
            if (!daNhap)
            {
                Console.WriteLine("Ban chua nhap mang. Vui long chon '1. Nhap mang' truoc.");
                return false;
            }
            return true;
        }
        static int NhapSoNguyen(string message)
        {
            int soNguyen;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out soNguyen))
                    return soNguyen;
                Console.WriteLine("Du lieu khong hop le, vui long nhap lai (so nguyen).");
            }
        }
        static int NhapSoNguyenDuong(string message)
        {
            int soNguyen;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out soNguyen) && soNguyen > 0)
                    return soNguyen;
                Console.WriteLine("Du lieu khong hop le, vui long nhap lai (so nguyen duong).");
            }
        }
        static int[] NhapMang()
        {
            int n = NhapSoNguyenDuong("Nhap so luong phan tu cua mang (n > 0): ");
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = NhapSoNguyen($"Nhap phan tu thu {i + 1}: ");
            }
            Console.WriteLine("Nhap mang thanh cong.");
            return a;
        }
        static void XuatMang(int[] a)
        {
            Console.Write("Mang: ");
            foreach (int x in a)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();
        }
        static int TinhTong(int[] a)
        {
            int tong = 0;
            foreach (int x in a)
            {
                tong += x;
            }
            return tong;
        }
        static int TimMax(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max;
        }
        static int TimMin(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] < min)
                    min = a[i];
            }
            return min;
        }
        static int DemChan(int[] a)
        {
            int dem = 0;
            foreach (int x in a)
            {
                if (x % 2 == 0)
                    dem++;
            }
            return dem;
        }
        static int DemLe(int[] a)
        {
            int dem = 0;
            foreach (int x in a)
            {
                if (x % 2 != 0)
                    dem++;
            }
            return dem;
        }
        static void SapXepTangDan(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                int viTriNhoNhat = i;
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < a[viTriNhoNhat])
                        viTriNhoNhat = j;
                }
                if (viTriNhoNhat != i)
                {
                    int tam = a[i];
                    a[i] = a[viTriNhoNhat];
                    a[viTriNhoNhat] = tam;
                }
            }
        }
        static int TimKiem(int[] a, int x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == x)
                    return i;
            }
            return -1;
        }
    }
}