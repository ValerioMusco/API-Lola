using Gallery_Lola_DAL.Interfaces;
using Gallery_Lola_DAL.Models;
using System.Data;

namespace Gallery_Lola_DAL.Services {
    public class FolderService : BaseRepository, IFolderService {

        public FolderService( IDbConnection connection ) : base( connection ) { }

        public IEnumerable<Folders> GetAll( string token ) {
            
            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetUserAccessible";
                GenerateParameter(command, "token", token );

                CheckOpenConnection( _connection );
                _connection.Open();

                IDataReader reader = command.ExecuteReader();
                while( reader.Read() )
                    yield return Mapper( reader );
            }
        }

        public void Remove( int folderId, string token, string table) {
            
            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = $"DELETE FROM {table} WHERE UserToken = @token AND IdFolder = @folderId";
                GenerateParameter( command, "token", token );
                GenerateParameter( command, "folderId", folderId );

                CheckOpenConnection( _connection );
                _connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private Folders Mapper( IDataReader reader ) {
            return new Folders {

                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Group = (int)reader["Groupe"],
                Year = (int)reader["Year"]
            };
        }
    }
}
