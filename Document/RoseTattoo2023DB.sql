USE master
GO

CREATE DATABASE RoseTattooShop2023DB
GO

USE RoseTattooShop2023DB
GO

CREATE TABLE MemberAccount(
  MemberID int primary key,
  MemberPassword nvarchar(40) not null,
  MemberFullName nvarchar(60) not null,
  MemberEmail nvarchar(60) unique, 
  MemberRole int
)
GO

INSERT INTO MemberAccount VALUES(4423 ,N'@1', N'Administrator', 'admin@RoseTattooShop.info', 1);
INSERT INTO MemberAccount VALUES(4424 ,N'@2', N'Member', 'member01@RoseTattooShop.info', 4);
INSERT INTO MemberAccount VALUES(4425 ,N'@4', N'Staff', 'staff01@RoseTattooShop.info', 2);
INSERT INTO MemberAccount VALUES(4426 ,N'@3', N'Manager', 'manager@RoseTattooShop.info', 3);
GO


CREATE TABLE RoseTattooType(
  TypeId nvarchar(20) primary key,
  RoseTattooName nvarchar(80) not null,
  RoseTattooDescription nvarchar(250), 
  Origin nvarchar(60)
)
GO
INSERT INTO RoseTattooType VALUES(N'RT0003',N'Traditional Rose',N'This is the classic rose tattoo design usually seen with bold lines and bright red color. It symbolizes love, beauty, and passion.', N'Austraulia')
GO
INSERT INTO RoseTattooType VALUES(N'RT0004',N'Black rose',N'It is a popular choice for those who want to create a unique look with their rose tattoo. Black roses are often associated with death, loss, and mourning.', N'Singapore')
GO
INSERT INTO RoseTattooType VALUES(N'RT0005',N'Watercolor rose',N'This rose tattoo design features vibrant hues and a unique style resembling watercolor paintings. It is perfect for those who want a colorful and artistic piece.', N'Thailand')
GO
INSERT INTO RoseTattooType VALUES(N'RT0006',N'Tribal rose',N'It is a more masculine and bold version of the rose tattoo design featuring sharp lines, geometric patterns, and darker hues.', N'Japan')
GO
INSERT INTO RoseTattooType VALUES(N'RT0007',N'Realistic rose',N'This tattoo design is perfect for those looking for a highly detailed and lifelike tattoo of a rose. It often features intricate shading and coloring work to make it look authentic.', N'US')
GO

CREATE TABLE TattooSticker(
 TattooStickerId int primary key,
 TattooStickerName nvarchar(100) not null,
 ImportDate datetime,
 TattooStickerDescription nvarchar(240),
 Quantity int,
 Price float,
 TypeId nvarchar(20) references RoseTattooType(TypeId) on delete cascade on update cascade
)
GO


INSERT INTO TattooSticker VALUES(5511,N'Realistic Rose Tattoo',CAST(N'2023-06-10' AS DateTime), N'Realistic is a popular style that is sophisticate and incredibly eye-catching. The realistic rose tattoo is a perfect way to emphasize the real beauty of roses.', 20, 10,'RT0003')
GO

INSERT INTO TattooSticker VALUES(5521,N'The single-line Rose Tattoo',CAST(N'2023-05-25' AS DateTime), N'The single-line tattoo is just perfect for anyone who is into minimal style. It’s a simple yet mesmerizing abstract of a rose flower.', 10, 40,'RT0003')
GO

INSERT INTO TattooSticker VALUES(5531,N'Blackwork Rose Tattoo',CAST(N'2023-05-12' AS DateTime), N'Blackwork and dot work tattoo might give you the impression of masculine and big tattoo design. However, with some skill, a rose tattoo can surely be done in this charming style.', 30, 18,'RT0003')
GO

INSERT INTO TattooSticker VALUES(5541,N'Traditional Rose Tattoo',CAST(N'2023-06-20' AS DateTime), N'Traditional style tattoo is where it all started. The traditional style rose tattoos were often inked by sailors to reminds them of their women. During that time, rose tattoos didn’t stand alone; ', 40, 30,'RT0003')
GO

INSERT INTO TattooSticker VALUES(5551,N'Mini Rose Tattoo',CAST(N'2023-06-10' AS DateTime), N'A small, elegant yet impressive mini tattoo, can you name anything better than that? We guess not. These little funky rose tattoo can be placed anywhere, on the wrist, chest, collarbone, or even on the ankle. ', 25, 45,'RT0003')
GO

INSERT INTO TattooSticker VALUES(5561,N'Red Rose Tattoo',CAST(N'2023-05-29' AS DateTime), N'Keep it classic with a red rose tattoo. This style is timeless and looks fantastic in a lot of different styles. Whether you opt for an Asian design or something realistic, you can’t go wrong with this piece. ', 15, 34,'RT0003')
GO

INSERT INTO TattooSticker VALUES(5571,N'Skull and Rose Tattoo',CAST(N'2023-06-19' AS DateTime), N'Channel your inner Shakespeare with a skull and rose tattoo. Whether you’re a fan of Hamlet or admire the contrast between subjects, this design is filled with symbolism. ', 10, 23,'RT0007')
GO