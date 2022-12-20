using D6UWHX_HFT_2021221.EndPoint.Services;
using D6UWHX_HFT_2021221.Logic.Interfaces;
using D6UWHX_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace D6UWHX_HFT_2021221.Endpoint.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        IArtistLogic<Artist> artistLogic;
        IHubContext<SignalRHub> hub;

        public ArtistController(IArtistLogic<Artist> artistLogic, IHubContext<SignalRHub> hub)
        {
            this.artistLogic = artistLogic;
            this.hub = hub;
        }

        [HttpPost]
        public void Post([FromBody] Artist value)
        {
            artistLogic.CreatArtist(value.Name, value.Age, value.Albumid.GetValueOrDefault(), value.ArtistId);
        }

        [HttpGet]
        public IEnumerable<Artist> Get()
        {
            return artistLogic.GetArtists();
        }

        [HttpGet("{Artistid}")]
        public Artist Get(int id)
        {
            return artistLogic.GetArtist(id);
        }



        [HttpPut]
        public void Put([FromBody] Artist artist)
        {
            artistLogic.UpdateArtist(artist);
            this.hub.Clients.All.SendAsync("ArtistUpdated", artist);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var artistToDelete = this.artistLogic.GetArtist(id);
            artistLogic.DeleteArtist(id);
            this.hub.Clients.All.SendAsync("artistDeleted", artistToDelete);
        }

    }
}
