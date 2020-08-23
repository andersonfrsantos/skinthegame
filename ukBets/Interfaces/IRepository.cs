using System.Collections.Generic;
namespace ukBets.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Adicionar(List<T> Objeto);
    }
}