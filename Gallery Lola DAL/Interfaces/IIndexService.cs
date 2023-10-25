using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Interfaces {
    public interface IIndexService {

        public IEnumerable<string> GetRandomPictures();


        /// <summary>
        /// Retourne un entier pour le cas à gérer.
        /// 0 => Recherche
        /// 1 => Unlock group
        /// 2 => Unlock folder
        /// </summary>
        /// <param name="userInput">Entrée de l'utilisateur</param>
        /// <returns></returns>
        public int CheckQuerryOrUnlock(string userInput);
        void AddToGroup( string userInput, string token );
        void AddFolder( string userInput, string token );
    }
}
