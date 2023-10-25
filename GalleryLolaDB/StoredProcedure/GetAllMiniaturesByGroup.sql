CREATE PROCEDURE [dbo].[GetAllMiniaturesByGroup]
	@group int
AS
	SELECT CONCAT(f.Name, '/', m.Name) as [Path]
	FROM Miniatures m	
	JOIN Folder f on m.IdFolder = f.Id
	WHERE f.Groupe = @group
RETURN 0
