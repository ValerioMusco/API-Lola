using Gallery_Lola_DAL.Models;

namespace Gallery_Lola_DAL.Interfaces {
    public interface IGalleryService : IBaseRepository {

        IEnumerable<int> GetYears();
        IEnumerable<Folders> Search(string querry);
        IEnumerable<Folders> GetAll();
        bool AddToFavorite( int folderId, string userToken );
    }
}
