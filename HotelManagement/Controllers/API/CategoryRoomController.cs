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
    public class CategoryRoomController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls= Repositories.GetAllCateRoom().Select(s => new CategoryRoomView { IDCateRoom = s.IDCateRoom, NameCateRoom = s.NameCateRoom, PriceCateRoom = s.PriceCateRoom,CountRoom=Repositories.GetRoomEmptyByIDCate(s.IDCateRoom).Count() });
            return Ok(ls);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(CategoryRoomView))]
        public IHttpActionResult Get(int id)
        {
            var item = Repositories.GetCateRoomByID(id);
            var cr = new CategoryRoomView { IDCateRoom = item.IDCateRoom, NameCateRoom = item.NameCateRoom, PriceCateRoom = item.PriceCateRoom };
            return Ok(cr);
        }

        // POST api/<controller>
        public IHttpActionResult Post(CategoryRoomView cr)
        {
            var newItem = new CategoryRoom { NameCateRoom = cr.NameCateRoom, PriceCateRoom = cr.PriceCateRoom };
            if (Repositories.CreateCateRoom(newItem) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, CategoryRoomView cr)
        {
            var item = new CategoryRoom { IDCateRoom = cr.IDCateRoom, NameCateRoom = cr.NameCateRoom, PriceCateRoom = cr.PriceCateRoom };
            if (Repositories.UpdateCateRoom(item) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            if (Repositories.DelCateRoom(id) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}