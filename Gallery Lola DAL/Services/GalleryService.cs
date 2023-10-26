using Gallery_Lola_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Services {
    public class GalleryService : BaseRepository, IGalleryService {
        public GalleryService( IDbConnection connection ) : base( connection ) {
        }

        public bool AddToFavorite( int folderId, string userToken ) {
            
            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "INSERT INTO UserFav VALUES(@userToken, @folderId)";
                GenerateParameter( command, "userToken", userToken );
                GenerateParameter( command, "folderId", folderId );

                CheckOpenConnection(_connection );
                _connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        public IEnumerable<int> GetYears() {
            
            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "SELECT [Year] FROM Folder group by [Year]";

                CheckOpenConnection( _connection );
                _connection.Open();

                IDataReader reader = command.ExecuteReader();
                while( reader.Read() )
                    yield return (int)reader["Year"];
                
            }
        }

        public IEnumerable<string> Search( string querry ) {
            
            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SearchFolder";
                querry = "%" + querry + "%";
                GenerateParameter( command, "querry", querry );

                CheckOpenConnection( _connection );
                _connection.Open();

                IDataReader reader = command.ExecuteReader();
                while( reader.Read() )
                    yield return (string)reader["Name"];
            }
        }
    }
}
