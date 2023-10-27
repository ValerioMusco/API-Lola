using Gallery_Lola_DAL.Interfaces;
using Gallery_Lola_DAL.Models;
using System.Data;

namespace Gallery_Lola_DAL.Services {
    public class GalleryService : BaseRepository, IGalleryService {
        public GalleryService( IDbConnection connection ) : base( connection ) {
        }
        private Folders Mapper( IDataReader reader ) {
            return new Folders {

                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Group = (int)reader["Groupe"],
                Year = (int)reader["Year"]
            };
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

        public IEnumerable<Folders> Search( string querry ) {
            
            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SearchFolder";
                querry = "%" + querry + "%";
                GenerateParameter( command, "querry", querry );

                CheckOpenConnection( _connection );
                _connection.Open();

                IDataReader reader = command.ExecuteReader();
                while( reader.Read() )
                    yield return Mapper(reader);
            }
        }

        public IEnumerable<Folders> GetAll() {

            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "SELECT * FROM Folder WHERE Groupe = 0";

                CheckOpenConnection(_connection );
                _connection.Open();

                IDataReader reader = command.ExecuteReader();
                while( reader.Read() )
                    yield return Mapper(reader);
            }
        }
    }
}
