using D6UWHX_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6UWHX_HFT_2021221.Logic.Interfaces
{
    public interface IAlbumLogic<T> where T : class
    {
        //crud
        T GetAlbum(int ID);

        void CreateAlbum(int ID, string title, double basePrice);

        void UpdateAlbum(Album album);

        void DeleteAlbum(int ID);
        
        //non crud
        double AVGPrice();
        IEnumerable<KeyValuePair<string, double>> AVGPriceByAlbums();
        List<T> GetAlbums();
    }
}
