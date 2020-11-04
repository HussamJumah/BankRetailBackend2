--create and use database
Create DATABASE BankRetailBackend;
go

USE BankRetailBackend;
go

--create tables
create table userstore(
	userID int primary key identity (1, 1),
	loginID varchar(50),
	pw varchar(500),
	roleID int foreign key references roles(ID),
	lastLogin datetime not null default Current_timestamp
);

create table roles(
	ID int primary key identity (1, 1),
	roleName varchar(50)
)

create table customerStatus(
	customerID int primary key identity (1000, 1),
	customerName varchar(50),
	SSN int,
	customerAge int,
	addressLine1 varchar(50),
	addressLine2 varchar(50),
	city varchar(50),
	state varchar(50),
	status varchar(10),
	message varchar(500),
	lastUpdated datetime not null default Current_timestamp
);

create index SSN_idx on customerStatus(SSN);

create table accountStatus(
	accountID int primary key identity (10000, 1),
	customerID int foreign key references customerStatus(customerID),
	accountType varchar(10),
	balance float,
	status varchar(25),
	message varchar(500),
	lastUpdated datetime not null default Current_timestamp
);

create table dbo.transactions(
	transactionID int primary key identity (1, 1),
	accountID int foreign key references accountStatus(accountID),
	transactionType varchar(20),
	transactionAmount float,
	balance float,
	transactionDate datetime not null default Current_timestamp
);

create index transactions_accountID_idx on transactions(accountID);

go