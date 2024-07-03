using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface IBaseService<T>
    {
        Task<T?> Get(string id);
        Task<List<T>?> Get();
        Task<T?> Create(T entity);
        Task<T?> Update(T entity);
        Task<T?> Delete(string id);
    }
}
