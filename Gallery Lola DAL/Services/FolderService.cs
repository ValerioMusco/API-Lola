using Gallery_Lola_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Services {
    public class FolderService : BaseRepository, IFolderService {

        public FolderService( IDbConnection connection ) : base( connection ) { }

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
    }
}
