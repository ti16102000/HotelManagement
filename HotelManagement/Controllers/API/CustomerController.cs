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
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [ResponseType(typeof(CustomerView))]
        public IHttpActionResult Get(int id)
        {
            var c = Repositories.GetCusByID(id);
            var cus = new CustomerView { IDCus = c.IDCus, AddressCus = c.AddressCus, DayCreateCus = c.DayCreateCus, EmailCus = c.EmailCus, NameCus = c.NameCus, PhoneCus = c.PhoneCus };
            return Ok(cus);
        }

        // POST api/<controller>
        public IHttpActionResult Post(CustomerView cv)
        {
            var cus = new Customer { NameCus = cv.NameCus, PhoneCus = cv.PhoneCus, AddressCus = cv.AddressCus, EmailCus = cv.EmailCus };
            int idCus=Repositories.CreateCus(cus);
            return Ok(idCus);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, BookingView cv)
        {
            var cus = new Customer { NameCus = cv.NameCus, PhoneCus = cv.PhoneCus, AddressCus = cv.AddressCus, EmailCus = cv.EmailCus,IDCus=cv.IDCus };
            Repositories.UpdateCus(cus,id);
            return Ok();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}