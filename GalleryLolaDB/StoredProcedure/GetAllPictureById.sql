CREATE PROCEDURE [dbo].[GetAllPictureById]
	@id int
AS
	SELECT CONCAT(f.Name, '/', p.Name) as [Path]
	FROM Pictures p
	JOIN Folder f on p.IdFolder = f.Id
	WHERE f.Id = @id
RETURN 0
