using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.Interface.IGeneric
{
    public interface IVigoGeneric<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetPaging(IEnumerable<Expression<Func<T, bool>>>? where, int pageIndex, int pageSize);
        Task<T> GetById(int id);
        void Create(T entity);
        void CreateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task DeleteRangeById(List<int> ids);
        Task<int> Count(IEnumerable<Expression<Func<T, bool>>> where);
    }
}
