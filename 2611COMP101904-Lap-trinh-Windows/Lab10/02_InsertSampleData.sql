USE EduManagerDB;
GO
INSERT INTO KHOA VALUES ('CNTT', N'Công nghệ Thông tin'), ('TOAN', N'Toán - Tin');
INSERT INTO LOP VALUES ('DH22TIN01', N'Đại học Tin 22-01', 'CNTT'),
                       ('DH22TIN02', N'Đại học Tin 22-02', 'CNTT'),
                       ('DH22TOAN01', N'Đại học Toán 22-01', 'TOAN');
INSERT INTO SINHVIEN VALUES
 ('SV001', N'Nguyễn Văn An',  '2004-03-15', N'Nam', 'DH22TIN01'),
 ('SV002', N'Trần Thị Bình',  '2004-07-21', N'Nữ',  'DH22TIN01'),
 ('SV003', N'Lê Hoàng Cường', '2003-11-02', N'Nam', 'DH22TIN02'),
 ('SV004', N'Phạm Thu Dung',  '2004-01-30', N'Nữ',  'DH22TIN02'),
 ('SV005', N'Võ Minh Em',     '2004-09-09', N'Nam', 'DH22TOAN01'),
 ('SV006', N'Đặng Ngọc Hà',   '2004-05-12', N'Nữ',  'DH22TIN01');
INSERT INTO MONHOC VALUES ('MH01', N'Lập trình trên Windows', 3), ('MH02', N'Cơ sở dữ liệu', 3),
                          ('MH03', N'Cấu trúc dữ liệu', 4),     ('MH04', N'Toán rời rạc', 2);
INSERT INTO KETQUA VALUES
 ('SV001','MH01',8.5), ('SV001','MH02',7.0), ('SV002','MH01',9.0), ('SV002','MH03',6.5),
 ('SV003','MH02',5.5), ('SV004','MH01',7.5), ('SV005','MH04',8.0), ('SV003','MH03',6.0);
INSERT INTO TAIKHOAN VALUES ('admin','123',N'Admin'), ('user','123',N'User');
GO