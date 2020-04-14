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
    public class HistoryBookingController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [ResponseType(typeof(HistoryBookingView))]
        public IHttpActionResult Get(int id)
        {
            var ls = Repositories.GetHisBookByID(id).Select(s => new HistoryBookingView { IDBook = s.IDBook, IDHisBook = s.IDHisBook, DayCreateHisBook = s.DayCreateHisBook, NameHisBook = s.NameHisBook });
            return Ok(ls);
        }

        
        // POST api/<controller>
        public IHttpActionResult Post(HistoryBookingView hb)
        {
            var his = new HistoryBooking { IDBook = hb.IDBook, NameHisBook = hb.NameHisBook, DayCreateHisBook = DateTime.Now };
            Repositories.CreateHisBook(his);
            return Ok();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}