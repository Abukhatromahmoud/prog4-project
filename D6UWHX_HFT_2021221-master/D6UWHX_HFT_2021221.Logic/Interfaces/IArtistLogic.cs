using D6UWHX_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6UWHX_HFT_2021221.Logic.Interfaces
{
    public interface IArtistLogic<T> where T : class
    {
        //non crud
        List<T> GetArtists();
        T OldArtist(int id);
        T YoungestArtist(int id);   

        //crud
        void CreatArtist(string name, int age, int albumId, int artistId);
        T GetArtist(int ArtistId);
        void UpdateArtist(Artist artist);
        void DeleteArtist(int ArtistId);

    }
}
