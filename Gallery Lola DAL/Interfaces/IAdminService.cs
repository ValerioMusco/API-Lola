namespace Gallery_Lola_DAL.Interfaces {
    public interface IAdminService : IBaseRepository {

        void DeleteFiles( int folderId, IEnumerable<string> pictures, IEnumerable<string> miniatures );
        void UpdateFolder( int folderId, string newName );
        void AddFolder( string name, IEnumerable<string> pictures, IEnumerable<string> miniatures );
        string PathToDirectory( int folderId );
        void Delete( int folderId, string table, string? file = null );
        void DeleteFolder( int folderId );
    }
}
