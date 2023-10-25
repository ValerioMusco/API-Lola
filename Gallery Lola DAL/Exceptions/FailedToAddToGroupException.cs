using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Exceptions {
    public class FailedToAddToGroupException : Exception {

        public FailedToAddToGroupException()
        {
            
        }

        public FailedToAddToGroupException(string message) : base(message)
        {
            
        }
    }
}
