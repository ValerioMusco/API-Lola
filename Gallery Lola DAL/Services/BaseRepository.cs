using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_Lola_DAL.Services {
    public class BaseRepository {

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
    }
}
