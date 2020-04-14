using HotelManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers.MVC
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(string token)
        {
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            var resCate = GlobalVariables.client.GetAsync("CategoryService/1").Result;
            TempData["cate"]=resCate.Content.ReadAsAsync<CategoryServiceView>().Result;
            HttpResponseMessage resCbb = GlobalVariables.client.GetAsync("CategoryService?idCateSer=1").Result;
            TempData["cbb"] = resCbb.Content.ReadAsAsync<IEnumerable<CategoryServiceView>>().Result;
            var resService = GlobalVariables.client.GetAsync("Service?idCate=1").Result;
            TempData["ser"] = resService.Content.ReadAsAsync<IEnumerable<ServiceView>>().Result;
            return View(book);
        }
        public ActionResult OrderService(string token,int idSer)
        {
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            var res = GlobalVariables.client.GetAsync("Service/" + idSer).Result;
            TempData["ser"] = res.Content.ReadAsAsync<ServiceView>().Result;
            return View(book);
        }
        [HttpPost]
        public JsonResult AjaxService(int idCate)
        {
            var resService = GlobalVariables.client.GetAsync("Service?idCate="+idCate).Result;
            var ls = resService.Content.ReadAsAsync<IEnumerable<ServiceView>>().Result;
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(ls), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateOrd(OrderDetailView odv,int idCate,int idBook,string token)
        {
            if (idCate == 1)
            {
                odv.NameService = odv.NameService + "(+5%/meal)";
                odv.Amount = odv.Quantity * (odv.PriceOrdD+Convert.ToDecimal(odv.PriceOrdD)*5/100);
            }
            else
            {
                odv.Amount = odv.Quantity *odv.PriceOrdD;
            }
            var resOrd = GlobalVariables.client.PostAsJsonAsync("OrderService?idBook=" + idBook, odv);
            return RedirectToAction("Index", new { token = token });
        }
    }
}