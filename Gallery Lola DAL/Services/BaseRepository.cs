using Gallery_Lola_DAL.Interfaces;
using System.Data;

namespace Gallery_Lola_DAL.Services {
    public class BaseRepository : IBaseRepository {

        protected readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection) {
            _connection = connection;
        }

        protected void GenerateParameter( IDbCommand dbCommand, string parameterName, object? value ) {

            IDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value ?? DBNull.Value;
            dbCommand.Parameters.Add( parameter );
        }
        protected static void CheckOpenConnection( IDbConnection connection ) {

            if( connection != null && connection.State == ConnectionState.Open )
                connection.Close();
        }

        public IEnumerable<string> GetFolderContent( int folderId, bool miniatures = false) {


            using( IDbCommand command = _connection.CreateCommand() ) {

                command.CommandType = CommandType.StoredProcedure;
                if( miniatures )
                    command.CommandText = "GetAllMiniaturesById";
                else
                    command.CommandText = "GetAllPictureById";

                GenerateParameter( command, "id", folderId );

                CheckOpenConnection( _connection );
                _connection.Open();

                IDataReader reader = command.ExecuteReader();
                while( reader.Read() )
                    yield return (string)reader["Path"];
            }
        }
    }
}
