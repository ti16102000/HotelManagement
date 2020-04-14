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
    public class BookingController : ApiController
    {
        

        // GET api/<controller>
        public IHttpActionResult Get(bool newBooking)
        {
            var ls = Repositories.GetAllBooking(newBooking).Select(item => new BookingView { IDBooking = item.IDBooking, IDCus = item.IDCus, DateIn = item.DateIn, DateOut = item.DateOut, DayCreateBooking = item.DayCreateBooking, DurationStay = item.DurationStay, IDCateRoom = item.IDCateRoom, NumberRoom = item.NumberRoom, NewBooking = item.NewBooking, TokenBooking = item.TokenBooking, NameCus = item.Customer.NameCus, AddressCus = item.Customer.AddressCus, EmailCus = item.Customer.EmailCus, PhoneCus = item.Customer.PhoneCus, DayCreateCus = item.Customer.DayCreateCus, NameCateRoom = item.CategoryRoom.NameCateRoom, CountRoomBook = Repositories.GetRB(item.IDBooking).Count() });
            return Ok(ls);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(BookingView))]
        public IHttpActionResult Get(string token)
        {
            var item = Repositories.GetBookingByToken(token);
            if (item != null)
            {
                var booking = new BookingView { IDBooking = item.IDBooking, IDCus = item.IDCus, DateIn = item.DateIn, DateOut = item.DateOut, DayCreateBooking = item.DayCreateBooking, DurationStay = item.DurationStay, IDCateRoom = item.IDCateRoom, NumberRoom = item.NumberRoom, NewBooking = item.NewBooking, TokenBooking = item.TokenBooking, NameCus = item.Customer.NameCus, AddressCus = item.Customer.AddressCus, EmailCus = item.Customer.EmailCus, PhoneCus = item.Customer.PhoneCus, DayCreateCus = item.Customer.DayCreateCus, NameCateRoom = item.CategoryRoom.NameCateRoom, CountRoomBook = Repositories.GetRB(item.IDBooking).Count(), PriceCateRoom = item.CategoryRoom.PriceCateRoom };
                return Ok(booking);
            }
            return InternalServerError();
        }
        public IHttpActionResult Get(int id)
        {
            if (Repositories.ReadBookingNew(id) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
        // POST api/<controller>
        public IHttpActionResult Post(BookingView bv)
        {
            var booking = new Booking { NumberRoom = bv.NumberRoom, DateIn = bv.DateIn, DateOut = bv.DateOut, DurationStay = bv.DurationStay, IDCateRoom = bv.IDCateRoom, IDCus = bv.IDCus, TokenBooking = bv.TokenBooking };
            Repositories.CreateBooking(booking);
            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, BookingView item)
        {
            var booking = new Booking { IDBooking = item.IDBooking, IDCus = item.IDCus, DateIn = item.DateIn, DateOut = item.DateOut, DayCreateBooking = item.DayCreateBooking, DurationStay = item.DurationStay, IDCateRoom = item.IDCateRoom, NumberRoom = item.NumberRoom, NewBooking = item.NewBooking, TokenBooking = item.TokenBooking };
            Repositories.UpdateBooking(booking);
            return Ok();
        }
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}