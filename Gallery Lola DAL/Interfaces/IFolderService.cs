using Gallery_Lola_DAL.Models;

namespace Gallery_Lola_DAL.Interfaces {
    public interface IFolderService : IBaseRepository {

        void Remove( int folderId, string token, string table );
        IEnumerable<Folders> GetAll( string token );
        
    }
}
