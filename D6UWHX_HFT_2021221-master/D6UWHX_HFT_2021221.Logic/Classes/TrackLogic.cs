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
    public class TrackLogic : ITrackLogic<Track>
    {
        private ITrackRepository<Track> _trackRepository;
        public TrackLogic(ITrackRepository<Track> trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public void CreatTrack(int trackId, string namePlace, int length)
        {
            Track track = new Track
            {
                TrackId = trackId,
                NamePlace = namePlace,
                Length = length

            };
            _trackRepository.Create(track);
        }

        public void DeleteTrack(int trackId)
        {
            Track track = _trackRepository.Read(trackId);
            if (track == null)
            {
                throw new Exception("NOt valid Track Id ");
            }
            _trackRepository.Delete(trackId);
        }


        public Track GetTrack(int TrackId)
        {
            Track track = _trackRepository.Read(TrackId);
            if (track == null)
            {
                throw new Exception("Not Valid Artist Id ");
            }
            else
                return track;
        }

        public List<Track> GetTracks()
        {
            return _trackRepository.GetAll().ToList();
        }

        public void UpdateTrack(Track track)
        {
            Track currentTrack = _trackRepository.Read(track.TrackId);
            if (currentTrack == null)
            {
                throw new Exception("Not Existing ");
            }
            currentTrack.AlbumId = track.AlbumId;
            currentTrack.Length = track.Length;
            currentTrack.NamePlace = track.NamePlace;
            _trackRepository.Update(currentTrack);
        }
        public Track GetLongestTrack()
        {
            return _trackRepository.GetAll().ToList().OrderByDescending(x => x.Length).First();

        }
        public Track GetShortestTrack()
        {
            return _trackRepository.GetAll().ToList().OrderBy(x => x.Length).First();
        }
        public Track GiveMeTheLastTrack()
        {
            return _trackRepository.GetAll().ToList().OrderBy(x => x.Length).Last();
        }
        public Track GiveMeTheLastTrackWithNamePlace()
        {
            return _trackRepository.GetAll().ToList().OrderBy(x => x.NamePlace).Last();
        }
        public Track GiveMeTheLastTrackWithID()
        {
            return _trackRepository.GetAll().ToList().OrderByDescending(x => x.TrackId).Last();
        }

        public Track HighestLength()
        {
            return _trackRepository.GetAll().ToList().OrderByDescending(x => x.Length).First();
        }
    }
}
