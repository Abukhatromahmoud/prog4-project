using D6UWHX_HFT_2021221.Data;
using D6UWHX_HFT_2021221.Models;
using D6UWHX_HFT_2021221.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D6UWHX_HFT_2021221.Repository.Classes
{
    public class TrackRepository : ITrackRepository<Track>
    {
        protected readonly MusicLibraryContext db;
        public TrackRepository(MusicLibraryContext db)
        {
            this.db = db;
        }
        public void Create(Track track)
        {
            db.Tracks.Add(track);
            db.SaveChanges();
        }
        public Track Read(int Trackid)
        {
            return
                db.Tracks.FirstOrDefault(t => t.TrackId == Trackid);
        }
        public IQueryable<Track> GetAll()
        {
            return db.Tracks;
        }
        public void Delete(int Trackid)
        {
            var TrackToDelete = Read(Trackid);
            db.Tracks.Remove(TrackToDelete);
            db.SaveChanges();
        }
        public void Update(Track track)
        {
            var TrackToUpdate = Read(track.TrackId);
            TrackToUpdate.NamePlace = track.NamePlace;
            db.SaveChanges();
        }
    }
}
