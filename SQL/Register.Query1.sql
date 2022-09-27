use Handmade_Product

--------------------------------------------COUSTOMER REGISTRATION

create table Registration(custID int identity(1,1) primary key,f_name varchar(100) not null,l_name varchar(100),email varchar(100) not null unique,gender varchar(20),phone bigint,c_password varchar(20))
select*from Registration
insert into Registration values('Saranya','Shalu','sara@gmail.com','female',9946902664,'Saranya123')
insert into Registration values('Sara','Shalu','sara123@gmail.com','female',9944568753,'Saranya123456')
alter table Registration add  Con_password varchar(20)


-------------------------------------------------------ADMIN
create table Admin(AdminID int identity primary key,AdminName varchar(100) not null unique,A_Password varchar(100)not null)
insert into Admin values('User1','User123')
select*from Admin

----------------------------------------------PRODUCT
create table Product(P_ID int identity primary key,P_name varchar(100) not null,P_image nvarchar(max)not null,P_desc varchar(500)not null,P_price int,P_quantity int)
select*from Product
alter table Product add add_pid int foreign key references Category(Cat_ID)

alter table Product add addadmin_pid int foreign key references Admin(AdminID)
delete from Product where P_ID=4




----------------------------------------------CATEGORY
create table Category(Cat_ID int identity primary key,Cat_name varchar(100)not null,Cat_image nvarchar(max),add_id int foreign key references Admin(AdminID))
select*from Category
alter table Category add Cat_status int 
delete from Category where Cat_ID=10
-----------------------------Order

create table Order_Details(
orderID int primary key identity,
o_P_fk int foreign key references Product(P_ID),
o_Userfk int foreign key references Registration(custID),
o_invoicefk int foreign key references O_Invoice(I_Id),
o_date datetime,
qty int,
bill float,
unitprice int



)
select*from Order_Details





create table O_Invoice(
I_Id int primary key identity,
I_userfk int foreign key references Product(P_ID),
I_date datetime


)
select*from O_Invoice