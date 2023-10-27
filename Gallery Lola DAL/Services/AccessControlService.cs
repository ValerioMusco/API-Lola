using Gallery_Lola_DAL.Exceptions;
using Gallery_Lola_DAL.Interfaces;
using System.Data;
using System.Reflection;

namespace Gallery_Lola_DAL.Services {
    public class AccessControlService : BaseRepository, IAccessControlService {
        public AccessControlService( IDbConnection connection ) : base( connection ) {
        }

        public bool CheckAccess( int folderId, string token ) {

            switch( CheckGroup( folderId ) ) {

                case 0:
                    return true;
                case 1:
                    return HasAccess( 1, token , new { table = "UserAccess", firstField = "Groupe", secondField = "TokenUser"});
                case 2:
                    return HasAccess( 2, token, new { table = "UserAccess", firstField = "Groupe", secondField = "TokenUser" } );
                case 3:
                    return HasAccess( folderId, token, new { table = "UserUnlock", firstField = "IdFolder", secondField = "UserToken" } );
                default:
                    throw new CheckAccessException( "Erreur lors de la vérification." );
            }
        }

        private int CheckGroup( int folderId ) {

            using( IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "SELECT [Groupe] from Folder where Id = @folderId";
                GenerateParameter( command, "folderId", folderId );

                CheckOpenConnection( _connection );
                _connection.Open();

                return (int)command.ExecuteScalar();
            }
        }

        private bool HasAccess<T>( int groupOrId, string token, T values ) {

            Type type = typeof( T );
            PropertyInfo tableProperty = type.GetProperty( "table" );
            PropertyInfo firstFieldProperty = type.GetProperty( "firstField" );
            PropertyInfo secondFieldProperty = type.GetProperty( "secondField" );

            string tableName = (string)tableProperty.GetValue( values );
            string firstField = (string)firstFieldProperty.GetValue( values );
            string secondField = (string)secondFieldProperty.GetValue( values );

            using( IDbCommand command = _connection.CreateCommand()) {

                command.CommandText = $"SELECT COUNT(*) FROM {tableName} WHERE {firstField} = @groupOrId AND {secondField} = @token";
                GenerateParameter(command, "groupOrId", groupOrId );
                GenerateParameter(command, "token", token );

                CheckOpenConnection(_connection );
                _connection.Open();

                return (int)command.ExecuteScalar() == 1;
            }
        }
    }
}
