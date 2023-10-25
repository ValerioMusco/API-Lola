CREATE PROCEDURE [dbo].[GetAllPictureByGroup]
	@group int
AS
	SELECT CONCAT(f.Name, '/', p.Name) as [Path]
	FROM Pictures p
	JOIN Folder f on p.IdFolder = f.Id
	WHERE f.Groupe = @group	
RETURN 0
