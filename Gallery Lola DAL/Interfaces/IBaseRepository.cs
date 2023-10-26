using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Interfaces {
    public interface IBaseRepository {

        IEnumerable<string> GetFolderContent( int folderId, bool miniatures = false);
    }
}
