using D6UWHX_HFT_2021221.EndPoint.Services;
using D6UWHX_HFT_2021221.Logic.Interfaces;
using D6UWHX_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace D6UWHX_HFT_2021221.Endpoint.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        ITrackLogic<Track> t1;
        IHubContext<SignalRHub> _hub;

        public TrackController(ITrackLogic<Track> t1, IHubContext<SignalRHub> hub)
        {
            this.t1 = t1;
            _hub = hub;

        }
        [HttpGet]
        public IEnumerable<Track> Get()
        {
            return t1.GetTracks();
        }

        // GET /brand/5
        [HttpGet("{id}")]
        public Track Get(int TrackId)
        {
            return t1.GetTrack(TrackId);
        }

        // POST /brand
        [HttpPost]
        public void Post([FromBody] Track value)
        {
            t1.CreatTrack(value.TrackId, value.NamePlace, value.Length);
        }

        // PUT /brand
        [HttpPut]
        public void Put([FromBody] Track value)
        {
            t1.UpdateTrack(value);
        }

        // DELETE /brand/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            t1.DeleteTrack(id);
        }
    }
}
