using HotelManagement.Models;
using HotelManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HotelManagement.Controllers.API
{
    public class EmptyRoomController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [ResponseType(typeof(RoomView))]
        public IHttpActionResult Get(int id)
        {
            var ls = Repositories.GetRoomEmptyByIDCate(id).Select(s => new RoomView { IDRoom = s.IDRoom, Empty = s.Empty, IDCateRoom = s.IDCateRoom, NameCateRoom = s.CategoryRoom.NameCateRoom, NameRoom=s.NameRoom });
            return Ok(ls);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            Repositories.UpdateRoomEmpty(id);
            return Ok();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}