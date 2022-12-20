using D6UWHX_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6UWHX_HFT_2021221.Repository.Interfaces
{
    public interface IAlbumRepository<T> where T : class
    {
        void Create(T e);
        void Delete(int id);
        IQueryable<T> GetAll();
        T Read(int id);
        void Update(T e);
    }
}
