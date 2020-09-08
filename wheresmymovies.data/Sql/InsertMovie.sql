DECLARE @Id int,
		@Title NVARCHAR(256),
		@ThumbImgUrl NVARCHAR(512),
		@Description NVARCHAR(MAX),
		@Runtime bigint,
		@FullImgUrl NVARCHAR(512)

INSERT INTO Movie (Title, ThumbImgUrl, [Description], Runtime, FullImgUrl) 
VALUES (@Title, @ThumgImgUrl, @Description, @Runtime, @FullImgUrl)

SELECT @Id = last_insert_rowid() --SCOPE_IDENTITY()
