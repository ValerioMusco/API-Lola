using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Exceptions {
    public class FailedToAddFolderException : Exception {

        public FailedToAddFolderException()
        {
            
        }

        public FailedToAddFolderException(string message) : base (message)
        {
            
        }
    }
}
