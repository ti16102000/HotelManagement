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
    public class RoomBookingController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [ResponseType(typeof(RoomBookingView))]
        public IHttpActionResult Get(int id)
        {
            var ls = Repositories.GetRB(id).Select(s=>new RoomBookingView { IDRoom=s.IDRoom,IDBook=s.IDBook,IDRoomBook=s.IDRoomBook,NameRoom=s.Room.NameRoom});
            return Ok(ls);
        }
        [ResponseType(typeof(RoomBookingView))]
        public IHttpActionResult GetRB(int idRB)
        {
            var item = Repositories.RoomChange(idRB);
            var rbv = new RoomBookingView { IDRoom = item.IDRoom, IDBook = item.IDBook, IDRoomBook = item.IDRoomBook, NameRoom = item.Room.NameRoom };
            return Ok(rbv);
        }
        // POST api/<controller>
        public IHttpActionResult Post(RoomBookingView rbv)
        {
            var rb = new RoomBooking { IDRoom = rbv.IDRoom, IDBook = rbv.IDBook };
            Repositories.CreateRB(rb);
            return Ok(rb);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int idRB,string reason,RoomBookingView rbv)
        {
            var rb = new RoomBooking { IDBook = rbv.IDBook, IDRoom = rbv.IDRoom, IDRoomBook = rbv.IDRoomBook };
            Repositories.UpdateRB(rb,reason);
            return Ok();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}