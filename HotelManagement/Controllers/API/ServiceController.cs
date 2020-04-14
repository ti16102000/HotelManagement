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
    public class ServiceController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetAllService().Select(s => new ServiceView { IDService = s.IDService, IDCateSer = s.IDCateSer, NameCateSer = s.CategoryService.NameCateSer, NameService = s.NameService, PriceService = s.PriceService });
            return Ok(ls);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(ServiceView))]
        public IHttpActionResult Get(int id)
        {
            var item = Repositories.GetServiceByID(id);
            var service = new ServiceView { IDService = item.IDService, IDCateSer = item.IDCateSer, NameCateSer = item.CategoryService.NameCateSer, NameService = item.NameService, PriceService = item.PriceService };
            return Ok(service);
        }
        public IHttpActionResult GetService(int idCate)
        {
            var ls = Repositories.GetServiceByIDCate(idCate).Select(s=>new ServiceView { IDCateSer=s.IDCateSer,IDService=s.IDService,NameService=s.NameService,PriceService=s.PriceService});
            return Ok(ls);
        }
        // POST api/<controller>
        public IHttpActionResult Post(ServiceView sv)
        {
            var newItem = new Service { NameService = sv.NameService, IDCateSer = sv.IDCateSer, PriceService = sv.PriceService };
            Repositories.CreateService(newItem);
            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, ServiceView sv)
        {
            var item = new Service { IDService = sv.IDService, IDCateSer = sv.IDCateSer, NameService = sv.NameService, PriceService = sv.PriceService };
            Repositories.UpdateService(item);
            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            if (Repositories.DelService(id) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}