CREATE PROCEDURE [dbo].[GetUserAccessible]
	@token nvarchar(max)
AS
	select f.Id, f.Name, f.Groupe, f.[Year]
	from Folder f
	full join UserAccess ua on ua.Groupe = f.Groupe
	full join UserUnlock uu on uu.IdFolder = f.Id
	where uu.UserToken like @token or ua.TokenUser like @token
RETURN 0
