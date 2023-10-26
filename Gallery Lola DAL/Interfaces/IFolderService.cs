using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Interfaces {
    public interface IFolderService : IBaseRepository {

        void Remove( int folderId, string token, string table );
        
    }
}
