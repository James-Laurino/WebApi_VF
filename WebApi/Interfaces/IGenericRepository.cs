using System;

namespace WebApi.Interfaces
{
    public interface IGenericRepository <T> where T : class
    {
        T Create(T sp);
        List<T> Read();
        T ReadOne(int id);
        T Delete(int id);
        T Update(T item);

    }
}