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
    public class OrderServiceController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult GetList(int idBook)
        {
            var ls = Repositories.GetAllOrdDetail(idBook).Select(s => new OrderDetailView { IDOrd = s.IDOrd, Amount = s.Amount, DayCreateOrdD = s.DayCreateOrdD, IDOrdD = s.IDOrdD, NameService = s.NameService, PriceOrdD = s.PriceOrdD, Quantity = s.Quantity });
            return Ok(ls);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(OrderServiceView))]
        public IHttpActionResult Get(int id)
        {
            var ord = Repositories.GetOrdServiceByID(id);
            if (ord == null)
            {
                return InternalServerError();
            }
            var osv = new OrderServiceView { IDBooking = ord.IDBooking, IDOrd = ord.IDOrd, Payment = ord.Payment, Total = ord.Total };
            return Ok(osv);
        }
        public IHttpActionResult GetPayment(int idBook,bool payment)
        {
            Repositories.UpdatePayment(idBook, payment);
            return Ok();
        }
        // POST api/<controller>
        public IHttpActionResult Post(int idBook,OrderDetailView odv)
        {
            var od = new OrderDetail { NameService = odv.NameService, PriceOrdD = odv.PriceOrdD, Quantity = odv.Quantity, Amount = odv.Amount };
            Repositories.CreateOrdDetail(od, idBook);
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