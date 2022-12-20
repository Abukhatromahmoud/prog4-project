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
    public class AlbumController : ControllerBase
    {
        private IAlbumLogic<Album> a1;
        private IHubContext<SignalRHub> _hub;

        public AlbumController(IAlbumLogic<Album> a1, IHubContext<SignalRHub> hub)
        {
            this.a1 = a1;
            _hub = hub;
        }

        [HttpGet]
        public IEnumerable<Album> Get()
        {
            return a1.GetAlbums();
        }

        // GET /brand/5
        [HttpGet("{id}")]
        public Album Get(int AlbumId)
        {
            return a1.GetAlbum(AlbumId);
        }

        // POST /brand
        [HttpPost]
        public void Post([FromBody] Album value)
        {
            a1.CreateAlbum(value.AlbumID, value.Title, value.BasePrice);
        }

        // PUT /brand
        [HttpPut]
        public void Put([FromBody] Album value)
        {
            a1.UpdateAlbum(value);
        }

        // DELETE /brand/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            a1.DeleteAlbum(id);
        }
    }
}