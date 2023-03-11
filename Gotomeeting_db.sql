Create Database Gotomeeting_db;
Use Gotomeeting_db;
Create table GTM_meeting_room_details
(
user_id int PRIMARY KEY identity(1 ,1),
first_name varchar(50) ,
last_name varchar(50) ,
email varchar(50),
room_id int ,
capacity bigint ,
start_at DATETIME not null,
  end_at DATETIME not null,
);

Create table GTM_Register_Details
(
Room_id int PRIMARY  KEY identity(101,1),
Full_name varchar(100),
Organization_name varchar(50),
category_name varchar(100),
Phone_number bigint,
Meeting_type varchar (250),
Exist_space int,
Available_space int,
);


Create table GTM_Login_Details
(
User_id int PRIMARY KEY identity(1,1),
email varchar(50),
IsActive varchar(100),
login_password varchar(100),
confirm_password varchar(100),
);

Create table GTM_Booking_details
(
booking_id bigint PRIMARY KEY identity(1111,1),
First_name varchar(50),
last_name varchar(50),
start_at DATETIME,
end_at DATETIME,
room_id int,
confirmation_status varchar(250),
requested_by varchar(50),
approved_by varchar(50),
);

Create table GTM_Admin_Details
(
room_admin_id int NOT NULL PRIMARY KEY,
approved_by varchar(50),
approved_at DATE,
IsActive varchar(100),
Verification_status varchar(100));

INSERT into GTM_meeting_room_details values('John','son','johnson@gmail.com',4321,500,'2022-10-22 10:10:00','2022-11-10 10:10:00')
INSERT into GTM_meeting_room_details values('Alien','Daniel','aliendan56@gmail.com',5678,500,'2022-02-10 11:10:00','2022-03-10 09:00:00')
INSERT into GTM_meeting_room_details values('Jig','jag','jigjag12@gmail.com',1001,500,'2021-09-10 09:00:00','2021-09-10 10:30:00')
INSERT into GTM_meeting_room_details values('Jack','frost','jackfrost@gmail.com',1002,500,'2021-10-10 11:10:00','2021-11-10 12:00:00')
INSERT into GTM_meeting_room_details values('James','sao','jamessao@gmail.com',1003,500,'2022-10-07 10:15:10','2021-10-07 01:00:00')
INSERT into GTM_meeting_room_details values('Danie','liam','danieliam@gmail.com',1005,500,'2023-01-02 08:00:00','2023-01-02 12:00:00');
Select * from GTM_meeting_room_details;

INSERT into GTM_Register_Details values('Johnson','VSB Engineering college','Student',9876543210,'Webinar',500,400)
INSERT into GTM_Register_Details values('AlienDaniel','KPR Engineering college','Student',6532109874,'Workshop_Online',500,450)
INSERT into GTM_Register_Details values('Jigjag','SSS Engineering college','Professor',9754621045,'Seminar',500,401)
INSERT into GTM_Register_Details values('Jackfrost','Hexaware Technologies','Presenter',6541237890,'Chiefguest',500,300)
INSERT INTO GTM_Register_Details values('Jamessao','Anna University','Student',6542789100,'Webinar',500,350)
INSERT INTO GTM_Register_Details values('Danieliam','Bharathiyar University','Student',7894561230,'Workshop_online',500,200);
select * from GTM_Register_Details;

INSERT INTO GTM_Login_Details values('johnson@gmail.com',1,'john@1155','john@1155')
INSERT INTO GTM_Login_Details values('aliendan56@gmail.com',1,'alien@528','alien@528')
INSERT INTO GTM_Login_Details values('jigjag12@gmail.com',1,'jigjag*231','jigjag*231')
INSERT INTO GTM_Login_Details values('jackfrost@gmail.com',0,'frost@828','frost@828')
INSERT INTO GTM_Login_Details values('danieliam@gmail.com',1,'danie#105','danie#105');
select * from GTM_Login_Details;

INSERT INTO GTM_Booking_details values('John','son','2022-10-22 10:10:00','2022-11-10 10:10:00',101,'REGISTERED','Johnson','Sushant')
INSERT INTO GTM_Booking_details values('Danie','liam','2023-01-02 08:00:00','2023-01-02 12:00:00',106,'REGISTERED','Danieliam','Kapoor')
INSERT INTO GTM_Booking_details values('Alien','Daniel','2022-02-10 11:10:00','2022-03-10 09:00:00',102,'REGISTERED','Aliendaniel','Padmesh')
INSERT INTO GTM_Booking_details values('Jig','jag','2021-09-10 09:00:00','2021-09-10 10:30:00',103,'REGISTERED','Jigjag','Saranya')
INSERT INTO GTM_Booking_details values('Jack','frost','2021-10-10 11:10:00','2021-11-10 12:00:00',104,'REGISTERED','Jackfrost','Divya');
select distinct * from GTM_Booking_details;

INSERT INTO GTM_Admin_Details values(9221,'Sushant','2022-10-21',1,'VERIFIED')
INSERT INTO GTM_Admin_Details values(9222,'Kapoor','2023-01-01',1,'VERIFIED')
INSERT INTO GTM_Admin_Details values(9223,'Padmesh','2022-03-09',1,'VERIFIED')

INSERT INTO GTM_Admin_Details values(9224,'Saranya','2021-09-08',1,'VERIFIED')
INSERT INTO GTM_Admin_Details values(9225,'Divya','2021-11-09',1,'VERIFIED')
INSERT INTO GTM_Admin_Details values(9226,'Vidisha','2022-10-21',0,'NOT VERIFIED');
select distinct * from GTM_Admin_Details