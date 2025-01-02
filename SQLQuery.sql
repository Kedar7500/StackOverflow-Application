create database StackOverflow
go

use StackOverflow
go

create table Categories(
CategoryID int primary key identity (1,1),
CategoryName varchar(max)
)
go

create table Users(
UserID int primary key identity (1,1),
Email varchar (max),
Password varchar(max),
Name varchar(15),
Mobile varchar(10),
IsAdmin bit default(0)
)
go

create table Answers(
AnswerID int primary key identity(1,1),
AnswerText nvarchar(max),
AnswerDateAndTime datetime,
UserID int references Users(UserID),
QuestionID int references Questions(QuestionID) on delete cascade,
VotesCount int)
go

create table Votes(
VoteID int primary key identity(1,1),
UserID int references Users(UserID),
AnswerID int references Answers(AnswerID) on delete cascade,
VoteValue int)
go

insert into Users values('admin@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
'Admin', '0000000000', 1)
go

