create database DevelopersBuddy
go

use DevelopersBuddy
go

create table Categories(
CategoryId int primary key identity(1,1),
CategoryName nvarchar(max))
go

create table Users(
UserId int primary key identity(1,1),
Email nvarchar(max),
PasswordHash nvarchar(max),
Name nvarchar(max),
Mobile nvarchar(max),
IsAdmin bit default(0))
go

create table Questions(
QuestionId int primary key identity(1,1),
QuestionName nvarchar(max),
QuestionDateAndTime datetime,
UserId int references Users(UserId) on delete cascade,
CategoryId int references Categories(CategoryId) on delete cascade,
VotesCount int,
AnswersCount int,
ViewsCount int)
go

create table Answers(
AnswerId int primary key identity(1,1),
AnswerText nvarchar(max),
AnswerDateAndTime datetime,
UserId int references Users(UserId),
QuestionId int references Questions(QuestionId) on delete cascade,
VotesCount int)
go

create table Votes(
VoteId int primary key identity(1,1),
UserId int references Users(UserId),
AnswerId int references Answers(AnswerId) on delete cascade,
VoteValue int)
go

use DevelopersBuddy
go

insert into Users values('admin@email.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','Admin','987654321',1)
go
insert into Users values('user@email.com','e606e38b0d8c19b24cf0ee3808183162ea7cd63ff7912dbb22b5e803286b4446','User1','987654320',0)
go

insert into Categories values('HTML')
go
insert into Categories values('CSS')
go
insert into Categories values('JavaScript')
go

insert into Questions values('How to display icon in browser using Html','2020-8-02 10:02 am',2,1,0,0,0)
go
insert into Questions values('How to set background image in CSS','2021-6-15 12:21 am',2,1,0,0,0)
go