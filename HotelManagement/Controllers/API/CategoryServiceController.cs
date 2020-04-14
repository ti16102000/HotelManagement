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
    public class CategoryServiceController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls= Repositories.GetAllCateService().Select(s => new CategoryServiceView { IDCateSer = s.IDCateSer, NameCateSer = s.NameCateSer });
            return Ok(ls);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(CategoryServiceView))]
        public IHttpActionResult Get(int id)
        {
            var item = Repositories.GetCateSeerviceByID(id);
            var csv = new CategoryServiceView { IDCateSer = item.IDCateSer, NameCateSer = item.NameCateSer };
            return Ok(csv);
        }
        public IHttpActionResult GetCateSer(int idCateSer)
        {
            var ls = Repositories.GetCateServiceCBB(idCateSer).Select(s => new CategoryServiceView { IDCateSer = s.IDCateSer, NameCateSer = s.NameCateSer });
            return Ok(ls);
        }
        // POST api/<controller>
        public IHttpActionResult Post(CategoryServiceView csv)
        {
            var item = new CategoryService { NameCateSer = csv.NameCateSer };
            if (Repositories.CreateCateService(item) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, CategoryServiceView csv)
        {
            var item = new CategoryService { IDCateSer = csv.IDCateSer, NameCateSer = csv.NameCateSer };
            if (Repositories.UpdateCateService(item) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            if (Repositories.DelCateService(id) == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}