namespace Utilitary.Infrastructure.Persistence
{
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Domine.Common;
    public class ProceduresDataEstudiantes : IProceduresDataEstudiantes
    {
        private readonly IDbConnection _dbConnection;
        private IConfiguration Configuration { get; }

        public ProceduresDataEstudiantes(IConfiguration configuration)
        {
            Configuration = configuration;
            _dbConnection = new SqlConnection(configuration["ConnectionStrings:EstudiantesData"]);
        }

        public async Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, IEnumerable<Parameters> Parameter, CancellationToken cancellationToken)
        {
            var valueParameter = new DynamicParameters();

            foreach (Parameters item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            var result = await _dbConnection.QueryFirstOrDefaultAsync<T>(
                new CommandDefinition(spName
                    , parameters: valueParameter
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, CancellationToken cancellationToken)
        {

            var result = await _dbConnection.QueryFirstOrDefaultAsync<T>(
                new CommandDefinition(spName
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, IEnumerable<Parameters> Parameter, CancellationToken cancellationToken)
        {

            var valueParameter = new DynamicParameters();

            foreach (Parameters item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }


            var result = await _dbConnection.QueryAsync<T>(
                new CommandDefinition(spName
                    , parameters: valueParameter
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, CancellationToken cancellationToken)
        {

            var result = await _dbConnection.QueryAsync<T>(
                new CommandDefinition(spName
                    , cancellationToken: cancellationToken
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public async Task<int> ExecuteSpSingleAsync(string spName, IEnumerable<Parameters> Parameter, CancellationToken cancellationToken)
        {
            var valueParameter = new DynamicParameters();

            foreach (Parameters item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            return await _dbConnection.ExecuteAsync(new CommandDefinition(spName, parameters: valueParameter, cancellationToken: cancellationToken, commandType: CommandType.StoredProcedure));
        }

        public async Task<int> ExecuteSpSingleWithOutPutAsync(string spName, IEnumerable<Parameters> Parameter, string output, CancellationToken cancellationToken)
        {
            var valueParameter = new DynamicParameters();

            foreach (Parameters item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            await _dbConnection.ExecuteAsync(new CommandDefinition(spName, parameters: valueParameter, cancellationToken: cancellationToken, commandType: CommandType.StoredProcedure));

            return valueParameter.Get<int>(output);
        }

        public async Task<int> ExecuteSpSingleAsync(string spName, CancellationToken cancellationToken)
        {
            return await _dbConnection.ExecuteAsync(new CommandDefinition(spName, cancellationToken: cancellationToken, commandType: CommandType.StoredProcedure));
        }

        public IEnumerable<T> ExecuteSp<T>(string spName, IEnumerable<Parameters> Parameter)
        {

            var valueParameter = new DynamicParameters();

            foreach (Parameters item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            var result = _dbConnection.Query<T>(
                new CommandDefinition(spName
                    , parameters: valueParameter
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public IEnumerable<T> ExecuteSp<T>(string spName, CancellationToken cancellationToken)
        {
            var result = _dbConnection.Query<T>(
                new CommandDefinition(spName
                    , commandType: CommandType.StoredProcedure));
            return result;
        }

        public int ExecuteSpSingle(string spName, IEnumerable<Parameters> Parameter)
        {
            var valueParameter = new DynamicParameters();

            foreach (Parameters item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    if (!string.Equals(item.ParameterValue.ToString(), "null"))
                        valueParameter.Add(item.ParameterName, item.ParameterValue, item.Type, item.Direction);
                }
            }

            return _dbConnection.Execute(new CommandDefinition(spName, parameters: valueParameter, commandType: CommandType.StoredProcedure));
        }

        public void ExecuteSpSingleThread(string spName, IEnumerable<Parameters> Parameter)
        {
            var ConectionString = Configuration.GetConnectionString("EstudiantesData");

            SqlConnection sql = new SqlConnection(ConectionString);
            SqlCommand cmd = new SqlCommand(spName, sql)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (Parameters item in Parameter)
            {
                if (item.ParameterValue != null)
                {
                    SqlParameter parm = new SqlParameter
                    {
                        DbType = item.Type
                    };
                    cmd.Parameters.Add(item.ParameterName, parm.SqlDbType).Value = item.ParameterValue;
                }
            }

            sql.Open();
            cmd.ExecuteNonQuery();
            return;

        }

        public int ExecuteSpSingle(string spName)
        {
            return _dbConnection.Execute(new CommandDefinition(spName, commandType: CommandType.StoredProcedure));
        }
    }
}
