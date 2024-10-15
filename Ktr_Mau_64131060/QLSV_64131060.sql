create database QLSV_64131060
go
use QLSV_64131060
go

create table LOP
(
	MaLop nvarchar(10) PRIMARY KEY,
	TenLop nvarchar(50) NOT NULL
)
go
create table SINHVIEN
(
    MaSV nvarchar(10) PRIMARY KEY,
    HoSV nvarchar(50) NOT NULL,
    TenSV nvarchar(10) NOT NULL,
    GioiTinh bit DEFAULT(1),
    NgaySinh date,
    AnhSV nvarchar(50),
    DiaChi nvarchar(100) NOT NULL,
    MaLop nvarchar(10) NOT NULL,
    FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
go
insert into LOP values 
('L001','64.CNTT-1'),
('L002','64.CNTT-2'),
('L003','64.CNTT-3'),
('L004','64.CNTT-4')
go

insert into SINHVIEN values
('64131060',N'Phạm Tuấn',N'Kiệt',1,cast(N'2004-07-17' as date),N'phamtuankiet.png',N'Diên Khánh, Khánh Hòa','L004'),
('64131061',N'Nguyễn Minh',N'Tài',1,cast(N'2004-07-10' as date),N'nguyenminhtai.png',N'Ninh Hòa','L003'),
('64130152',N'Nguyễn Hồ Thanh',N'Bình',0,cast(N'2004-04-02' as date),N'nguyenhothanhbinh.png',N'Đông Sơn, Ninh Hòa','L004'),
('64130493',N'Cao Linh',N'Hà',0,cast(N'2004-12-17' as date),N'caolinhha.png',N'Ninh Hòa','L004'),
('64131973',N'Nguyễn Hiểu',N'Quyên',0,cast(N'2004-10-17' as date),N'nguyenhieuquyen.png',N'Diên Khánh, Khánh Hòa','L004'),
('64132409',N'Vĩnh',N'Thuận',1,cast(N'2004-08-31' as date),N'vinhthuan.png',N'Nha Trang','L004')
go
create proc SinhVien_TimKiem
    @MaSV nvarchar(10) = NULL,
    @HoTenSV nvarchar(50) = NULL
as
begin 
    set nocount on;

    select * from SINHVIEN where (1 = 1)
    and (@MaSV is null or MaSV like '%' + @MaSV + '%')
    and (@HoTenSV is null or (HoSV + ' ' + TenSV) like '%' + @HoTenSV + '%')
end 
go
