using HotelManagement.Models;
using HotelManagement.Models.EntityModel;
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
    public class RoomController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetAllRoom().Select(s => new RoomView { IDRoom = s.IDRoom, IDCateRoom = s.IDCateRoom, NameCateRoom = s.CategoryRoom.NameCateRoom, NameRoom = s.NameRoom, Empty = s.Empty });
            return Ok(ls);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(RoomView))]
        public IHttpActionResult Get(int id)
        {
            var item = Repositories.GetRoomByID(id);
            var rv = new RoomView { IDRoom = item.IDRoom, IDCateRoom = item.IDCateRoom, NameCateRoom = item.CategoryRoom.NameCateRoom, NameRoom = item.NameRoom, Empty = item.Empty };
            return Ok(rv);
        }

        // POST api/<controller>
        public IHttpActionResult Post(RoomView rv)
        {
            var newItem = new Room { IDCateRoom = rv.IDCateRoom, NameRoom = rv.NameRoom };
            Repositories.CreateRoom(newItem);
            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, RoomView rv)
        {
            var item = new Room { IDRoom = rv.IDRoom, IDCateRoom = rv.IDCateRoom, NameRoom = rv.NameRoom, Empty = rv.Empty };
            Repositories.UpdateRoom(item);
            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            if (Repositories.DelRoom(id) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}