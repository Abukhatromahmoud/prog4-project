using D6UWHX_HFT_2021221.Logic.Interfaces;
using D6UWHX_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace D6UWHX_HFT_2021221.EndPoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        IAlbumLogic<Album> _albumLogic;
        IArtistLogic<Artist> _artistLogic;
        ITrackLogic<Track> _trackLogic;

        public QueryController(IAlbumLogic<Album> albumLogic, IArtistLogic<Artist> artistLogic, ITrackLogic<Track> trackLogic)
        {
            _albumLogic = albumLogic;
            _artistLogic = artistLogic;
            _trackLogic = trackLogic;
        }

        [HttpGet]
        public IEnumerable<string> AVGPriceByAlbums()
        {
            return (IEnumerable<string>)_albumLogic.AVGPriceByAlbums();
        }
        [HttpGet]
        public double AlbumAVGPrice()
        {
            return _albumLogic.AVGPrice();
        }
        [HttpGet("{id}")]
        public Artist Youngest(int id)
        {
            return _artistLogic.YoungestArtist(id);
        }
        [HttpGet]
        public IEnumerable<Track> Tracks()
        {
            return _trackLogic.GetTracks();
        }
        [HttpGet]
        public Track LongestTrack()
        {
            return _trackLogic.HighestLength();
        }
        [HttpGet]
        public Track GetShortestTrack()
        {
            return _trackLogic.GetShortestTrack();
        }
    }
}
