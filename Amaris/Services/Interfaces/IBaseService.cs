using Amaris.Models.General;

namespace Amaris.Services.Interfaces
{
    public interface IBaseService<T> where T : new()
    {

        public Task<Response<T>> GetItem(Int64 id);
        public Task<Response<T>> GetItems(string param);
    }
}
