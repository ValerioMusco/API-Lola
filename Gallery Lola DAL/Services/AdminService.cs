using Gallery_Lola_DAL.Interfaces;
using System.Data;

namespace Gallery_Lola_DAL.Services {
    public class AdminService : BaseRepository, IAdminService {

        public AdminService( IDbConnection connection ) : base( connection ) {
        }
                                //Folder directory, ...
        public void AddFolder( string name, IEnumerable<string> pictures, IEnumerable<string> miniatures ) {

            int folderId;

            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "INSERT INTO Folder OUTPUT INSERTED.Id VALUES (@name, @group, @year)"; // Remplacer par objet plus tard
                GenerateParameter(command, "name", name ); // directory.name
                GenerateParameter( command, "group", 0 ); // directory.group
                GenerateParameter( command, "year", DateTime.Now.Year ); // directory.year

                CheckOpenConnection( _connection );
                _connection.Open();

                folderId = (int)command.ExecuteScalar();
                _connection.Close();
            }

            foreach( string p in pictures )
                AddFile( folderId, p, "Pictures");

            foreach( string m in miniatures )
                AddFile( folderId, m, "Miniatures");
        }
        public void DeleteFiles( int folderId, IEnumerable<string> pictures, IEnumerable<string> miniatures ) {

            foreach( string p in pictures )
                Delete( folderId, "Pictures", p );

            foreach( string m in miniatures )
                Delete( folderId, "Miniatures", m );
        }
        public void Delete( int folderId, string table, string? file = null ) {

            using( IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = $"DELETE FROM {table} WHERE IdFolder = @folderId";
                GenerateParameter( command, "folderId", folderId );

                if(file != null ) {

                    command.CommandText += " AND [Name] = @file";
                    GenerateParameter( command, "file", file );
                }

                CheckOpenConnection( _connection );
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public void DeleteFolder( int folderId ) {

            using( IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = $"DELETE FROM Folder WHERE Id = @folderId";
                GenerateParameter( command, "folderId", folderId );

                CheckOpenConnection( _connection );
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public void UpdateFolder( int folderId, string newName ) {

            using(IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = "UPDATE Folder SET [Name] = @name where Id = @folderId";
                GenerateParameter( command, "folderId", folderId );
                GenerateParameter(command, "name", newName );

                CheckOpenConnection(_connection );
                _connection.Open( );
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
        public string PathToDirectory( int folderId ) {
            
            using(IDbCommand command = _connection.CreateCommand()) {

                command.CommandText = "SELECT [Name] FROM Folder where Id = @folderId";
                GenerateParameter( command, "folderId", folderId );

                CheckOpenConnection(_connection );
                _connection.Open();

                return "E:\\FTP\\" + command.ExecuteScalar();
            }
        }

        private void AddFile(int folderId, string name, string table) {

            using( IDbCommand command = _connection.CreateCommand() ) {

                command.CommandText = $"INSERT INTO {table} VALUES (@file, @folderId)";
                GenerateParameter( command, "file", name );
                GenerateParameter( command, "folderId", folderId );

                CheckOpenConnection( _connection );
                _connection.Open();

                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
