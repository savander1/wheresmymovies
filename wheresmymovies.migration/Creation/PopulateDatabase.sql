DECLARE @Id int;

INSERT INTO Movie (Title, ThumbImgUrl, [Description], Runtime, FullImgUrl) 
VALUES ('Loan Aranger', 'https://via.placeholder.com/100x150', 'The greatest movie of all time', 143, 'https://via.placeholder.com/200x300')

SELECT @Id = last_insert_rowid() --SCOPE_IDENTITY()

INSERT INTO MovieYear (Id, [Year]) VALUES (@Id, 1973)
INSERT INTO MovieGenre (Id, Genre) VALUES (@Id, 'Comedy')
INSERT INTO MovieGenre (Id, Genre) VALUES (@Id, 'Drama')
INSERT INTO MovieGenre (Id, Genre) VALUES (@Id, 'Western')
INSERT INTO MovieDirector (Id, Director) VALUES (@Id, 'Gene Wildest')
INSERT INTO MovieWriter (Id, Writer) VALUES (@Id, 'Charlie CoughMan')
INSERT INTO MovieActor (Id, Actor) VALUES (@Id, 'Will Smoth')
INSERT INTO MovieActor (Id, Actor) VALUES (@Id, 'Giani Deep')
INSERT INTO MovieActor (Id, Actor) VALUES (@Id, 'Angelina Jolly')
INSERT INTO MovieFormatLocation (Id, [Format], [Location]) VALUES (@Id, 'DVD', 'B68')
INSERT INTO MovieFormatLocation (Id, [Format], [Location]) VALUES (@Id, 'Digital', 'AppleTV')
INSERT INTO MovieFormatLocation (Id, [Format], [Location]) VALUES (@Id, 'Digital', 'XBox')