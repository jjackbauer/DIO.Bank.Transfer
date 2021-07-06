using System.Collections.Generic;

namespace DIO.Bank
{
    public interface iRepository<T>
    {
        List<T> GetList();
        T Get(int Id);
        void Insert(T Entity);
        void Remove(int Id);
        void Update(int Id, T Entity);
        int GetNextId();

    }
}