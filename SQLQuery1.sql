create database mydb
use mydb

create table items
(
itemcode varchar(100) primary key,
itemname varchar(100),
picture varchar(200)
)

Create table student
(
id int primary key,
name varchar(100),
fee money,
picture nvarchar(500)
)

create table Fee
(
voucherno nvarchar(100) primary key,
inputDate datetime,
studentId int foreign key references student(id),
headname nvarchar(100),
amount money
)

create table examresult
(
examsl int,
studentid int foreign key references student(id),
examname varchar(100),
institution varchar(200),
board varchar(200),
result varchar(100),
primary key(examsl,studentid)
)


select * from items
select * from student
select * from Fee
select * from examresult