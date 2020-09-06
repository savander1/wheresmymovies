SELECT Id, Title, ThumbImgUrl, Description, Runtime, FullImgUrl FROM Movie Where Id = @Id
SELECT Id, Year FROM MovieYear WHERE Id = @Id
SELECT Id, Genre FROM MovieGenre WHERE Id = @Id
SELECT Id, Directory FROM MovieDirector WHERE Id = @Id
SELECT Id, Writer FROM MovieWriter WHERE Id = @Id
SELECT Id, Actor FROM MovieActor WHERE Id = @Id
SELECT Id, Format, Location FROM MovieFormatLocation WHERE Id = @Id