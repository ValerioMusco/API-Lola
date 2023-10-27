using Gallery_Lola_DAL.Exceptions;
using Gallery_Lola_DAL.Interfaces;
using System.Data;

namespace Gallery_Lola_DAL.Services {
    public class IndexService : BaseRepository, IIndexService {

        public IndexService( IDbConnection connection ) : base(connection) {}

        public int CheckQuerryOrUnlock( string userInput ) {

            using( IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "SELECT * FROM FolderPassword WHERE Password = @password";
                GenerateParameter( command, "password", userInput );

                CheckOpenConnection( _connection );
                _connection.Open();

                IDataReader reader = command.ExecuteReader();
                if( reader.Read() )
                    return 2;
            }

            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "SELECT * FROM GroupPassword WHERE Password = @password";
                GenerateParameter( command, "password", userInput );

                CheckOpenConnection( _connection );
                _connection.Open() ;

                IDataReader reader = command.ExecuteReader();
                if( reader.Read() )
                    return 1;
            }

            return 0;
        }

        public IEnumerable<string> GetRandomPictures() {

            using(IDbCommand command = _connection.CreateCommand()) {

                List<string> tempPictures = new();
                Random rand = new Random();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllMiniaturesByGroup";
                GenerateParameter( command, "group", 0 );

                CheckOpenConnection(_connection );
                _connection.Open();
                IDataReader reader = command.ExecuteReader();
                while( reader.Read() )
                    tempPictures.Add( (string)reader["Path"] );

                _connection.Close();

                return tempPictures.OrderBy(item => rand.Next()).Take(9).ToList();
            }
        }

        public void AddToGroup(string userInput, string token) {

            using(IDbCommand command = _connection.CreateCommand()) {

                command.CommandText = "INSERT INTO UserAccess VALUES (@token, @group)";
                GenerateParameter(command, "group", GetGroupId(userInput));
                GenerateParameter(command, "token", token);

                try {

                    CheckOpenConnection( _connection );
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch(Exception ex) {

                    _connection.Close();
                    throw new FailedToAddToGroupException( ex.Message );
                }
            }
        }

        public void AddFolder( string userInput, string token ) {

            using( IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "INSERT INTO UserUnlock VALUES (@token, @folderId)";
                GenerateParameter( command, "folderId", GetFolderIdWithPassword( userInput ) );
                GenerateParameter( command, "token", token );

                try {

                    CheckOpenConnection( _connection );
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch( Exception ex ) {

                    _connection.Close();
                    throw new FailedToAddFolderException( ex.Message );
                }
            }
        }

        public int GetGroupId(string userInput ) {

            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "SELECT [Group] FROM GroupPassword where Password = @password";
                GenerateParameter( command, "password", userInput );

                CheckOpenConnection(_connection );
                _connection.Open() ;

                return (int)command.ExecuteScalar();
            }
        }

        public int GetFolderIdWithPassword( string userInput ) {

            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = $"SELECT IdFolder FROM FolderPassword WHERE " +
                                      $"Password = @password";
                GenerateParameter(command, "password", userInput );

                CheckOpenConnection(_connection );
                _connection.Open() ;

                return (int)command.ExecuteScalar();

            }
        }
    }
}
