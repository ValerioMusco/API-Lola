using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Interfaces {
    public interface IGalleryService : IBaseRepository {

        IEnumerable<int> GetYears();
        IEnumerable<string> Search(string querry);
        bool AddToFavorite( int folderId, string userToken );
    }
}
