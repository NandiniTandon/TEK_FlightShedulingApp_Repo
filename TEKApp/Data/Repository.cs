using System;
using System.Collections.Generic;

namespace TEKApp.Data
{
    public class Repository<T> : IRepository<T>
    {
        private readonly IList<T> _items = new List<T>();
        public void Add(T item)
        {
            _items.Add(item);
        }

        public IList<T> GetAll()
        {
            return _items;
        }
    }
}
