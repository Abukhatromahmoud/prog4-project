using D6UWHX_HFT_2021221.Logic.Interfaces;
using D6UWHX_HFT_2021221.Models;
using D6UWHX_HFT_2021221.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6UWHX_HFT_2021221.Logic.Classes
{
    public class ArtistLogic : IArtistLogic<Artist>
    {
        private readonly IArtistRepository<Artist> _artistRepository;
        public ArtistLogic(IArtistRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void CreatArtist(string name, int age, int albumId, int artistId)
        {
            Artist artist = new Artist
            {
                Name = name,
                Age = age,
                Albumid = albumId,
                ArtistId = artistId

            };

            _artistRepository.Create(artist);

        }

        public void DeleteArtist(int artistId)
        {
            Artist artist = _artistRepository.Read(artistId);
            if (artist == null)
            {
                throw new Exception("Not valid artist id");
            }

            _artistRepository.Delete(artistId);
        }

        public Artist GetArtist(int artistId)
        {
            Artist artist = _artistRepository.Read(artistId);
            if (artist == null)
            {
                throw new Exception("Not valid artist id");
            }
            else
                return artist;
        }

        public List<Artist> GetArtists()
        {
            return _artistRepository.GetAll().ToList();
        }

        // with this current update you cannot change the id, because it searches with it 
        public void UpdateArtist(Artist artist)
        {
            Artist currentArtist = _artistRepository
                .Read(artist.ArtistId);
            if (currentArtist == null)
            {

                throw new Exception("Not Existing ");
            }
            currentArtist.Age = artist.Age;
            currentArtist.Album = artist.Album;
            currentArtist.Albumid = artist.Albumid;
            currentArtist.Name = artist.Name;

            _artistRepository.Update(currentArtist);

        }
        public Artist GetYoungestArtist()
        {
            return _artistRepository.GetAll().ToList().OrderBy(x => x.Age).First();
        }
        public Artist GetTheOldestArtist()
        {
            return _artistRepository.GetAll().ToList().OrderByDescending(x => x.Age).First();
        }
        public IEnumerable<Artist> GetCommentNumberPerArtist()
        {
            var qx_sub = from x in _artistRepository.GetAll()
                         group x by x.ArtistId into g
                         select new
                         {
                             Artist_ID = g.Key,
                             Artist_No = g.Count()
                         };

            var qx = from x in _artistRepository.GetAll()
                     join z in qx_sub on x.ArtistId equals z.Artist_ID
                     let joinedItem = new { x.ArtistId, x.Name, z.Artist_No }
                     group joinedItem by joinedItem.Name into grp
                     select new Artist
                     {
                         Name = grp.Key,
                         ArtistId = grp.Sum(x => x.Artist_No)
                     };

            return qx;
        }

        public Artist OldArtist(int id)
        {
            return _artistRepository.GetAll().ToList().OrderByDescending(x => x.Age).First();
        }

        public Artist YoungestArtist(int id)
        {
            return _artistRepository.GetAll().ToList().OrderBy(x => x.Age).First();
        }
    }
}
