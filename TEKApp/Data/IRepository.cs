using System.Collections.Generic;

namespace TEKApp.Data
{
    public interface IRepository<T>
    {
        void Add(T item);
        IList<T> GetAll();
    }
}
