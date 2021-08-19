using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,new() //referans tipte olan ıentity implemente edilmiş newlenebilen nesne
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter=null);

      //  IQueryable<T> Find(Expression<Func<T, bool>> predicate); //liq sorgularının gönderileceği metot
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        //IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);






    }

}
