namespace Utilitary.Core.Common.Interfaces.Persistence
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Domine.Common;
    public interface IProceduresDataEstudiantes
    {
        Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ExecuteSpAsync<T>(string spName, IEnumerable<Parameters> Parameter, CancellationToken cancellationToken);
        Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, IEnumerable<Parameters> Parameter, CancellationToken cancellationToken);
        Task<T> ExecuteFirstOrDefaultSpAsync<T>(string spName, CancellationToken cancellationToken);
        Task<int> ExecuteSpSingleAsync(string spName, IEnumerable<Parameters> Parameter, CancellationToken cancellationToken);
        Task<int> ExecuteSpSingleWithOutPutAsync(string spName, IEnumerable<Parameters> Parameter, string output, CancellationToken cancellationToken);
        Task<int> ExecuteSpSingleAsync(string spName, CancellationToken cancellationToken);
        IEnumerable<T> ExecuteSp<T>(string spName, IEnumerable<Parameters> Parameter);
        IEnumerable<T> ExecuteSp<T>(string spName, CancellationToken cancellationToken);
        int ExecuteSpSingle(string spName, IEnumerable<Parameters> Parameter);
        void ExecuteSpSingleThread(string spName, IEnumerable<Parameters> Parameter);
        int ExecuteSpSingle(string spName);
    }
}
