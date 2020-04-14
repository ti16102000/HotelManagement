using HotelManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers.MVC
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var res = GlobalVariables.client.GetAsync("Booking?newBooking=true").Result;
            var ls = res.Content.ReadAsAsync<IEnumerable<BookingView>>().Result;
            return View(ls);
        }
        #region Category Room
        public ActionResult ListCateRoom()
        {
            IEnumerable<CategoryRoomView> lsCate = null;
            HttpResponseMessage res = GlobalVariables.client.GetAsync("CategoryRoom").Result;
            lsCate = res.Content.ReadAsAsync<IEnumerable<CategoryRoomView>>().Result;
            return View(lsCate);
        }
        public ActionResult CategoryRoom()
        {
            return View();
        }
        public ActionResult CreateCateRoom(CategoryRoomView cr)
        {
            HttpResponseMessage res = GlobalVariables.client.PostAsJsonAsync("CategoryRoom", cr).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new category successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Create new category failed!";
            return RedirectToAction("CreateCateRoom");
        }
        public ActionResult GetCateRoom(int id)
        {
            var res = GlobalVariables.client.GetAsync("CategoryRoom/" + id.ToString()).Result;
            return View(res.Content.ReadAsAsync<CategoryRoomView>().Result);
        }
        public ActionResult UpdateCateRoom(CategoryRoomView cr)
        {
            var res = GlobalVariables.client.PutAsJsonAsync("CategoryRoom/" + cr.IDCateRoom.ToString(), cr).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Update category successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Update new category failed!";
            return RedirectToAction("GetCateRoom", new { id = cr.IDCateRoom });
        }
        public ActionResult DelCateRoom(int id)
        {
            var res = GlobalVariables.client.DeleteAsync("CategoryRoom/" + id.ToString()).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Delete successfully!";
            }
            else
            {
                TempData["error"] = "Delete failed!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Room
        public ActionResult RoomList()
        {
            IEnumerable<RoomView> lsRoom = null;
            var res = GlobalVariables.client.GetAsync("Room").Result;
            lsRoom = res.Content.ReadAsAsync<IEnumerable<RoomView>>().Result;
            return View(lsRoom);
        }
        public ActionResult Room()
        {
            HttpResponseMessage resCbb = GlobalVariables.client.GetAsync("CategoryRoom").Result;
            return View(resCbb.Content.ReadAsAsync<IEnumerable<CategoryRoomView>>().Result);
        }
        public ActionResult CreateRoom(RoomView rv)
        {
            var res = GlobalVariables.client.PostAsJsonAsync("Room", rv).Result;
            TempData["success"] = "Create new room successfully!";
            return RedirectToAction("RoomList");
        }
        public ActionResult GetRoom(int id)
        {
            HttpResponseMessage resCbb = GlobalVariables.client.GetAsync("CategoryRoom").Result;
            TempData["cbb"] = resCbb.Content.ReadAsAsync<IEnumerable<CategoryRoomView>>().Result;

            var res = GlobalVariables.client.GetAsync("Room/" + id.ToString()).Result;
            return View(res.Content.ReadAsAsync<RoomView>().Result);
        }
        public ActionResult UpdateRoom(RoomView rv)
        {
            var res = GlobalVariables.client.PutAsJsonAsync("Room/" + rv.IDRoom.ToString(), rv).Result;
            TempData["success"] = "Update room successfully!";
            return RedirectToAction("RoomList");
        }
        public ActionResult DelRoom(int id)
        {
            var res = GlobalVariables.client.DeleteAsync("Room/" + id.ToString()).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Delete successfully!";
            }
            else
            {
                TempData["error"] = "Delete failed!";
            }
            return RedirectToAction("RoomList");
        }
        #endregion

        #region Category Service
        public ActionResult ListCateSer()
        {
            IEnumerable<CategoryServiceView> ls = null;
            HttpResponseMessage res = GlobalVariables.client.GetAsync("CategoryService").Result;
            ls = res.Content.ReadAsAsync<IEnumerable<CategoryServiceView>>().Result;
            return View(ls);
        }
        public ActionResult CategoryService()
        {
            return View();
        }
        public ActionResult CreateCateService(CategoryServiceView csv)
        {
            var res = GlobalVariables.client.PostAsJsonAsync("CategoryService", csv).Result;
            TempData["success"] = "Create new category successfully!";
            return RedirectToAction("Index");
        }
        public ActionResult GetCateService(int id)
        {
            var res = GlobalVariables.client.GetAsync("CategoryService/" + id.ToString()).Result;
            return View(res.Content.ReadAsAsync<CategoryServiceView>().Result);
        }
        public ActionResult UpdateCateService(CategoryServiceView csv)
        {
            var res = GlobalVariables.client.PutAsJsonAsync("CategoryService/" + csv.IDCateSer.ToString(), csv).Result;
            TempData["success"] = "Update category successfully!";
            return RedirectToAction("Index");
        }
        public ActionResult DelCateService(int id)
        {
            var res = GlobalVariables.client.DeleteAsync("CategoryService/" + id.ToString()).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Delete successfully!";
            }
            else
            {
                TempData["error"] = "Delete failed!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Service
        public ActionResult ServiceList()
        {
            IEnumerable<ServiceView> ls = null;
            var res = GlobalVariables.client.GetAsync("Service").Result;
            ls = res.Content.ReadAsAsync<IEnumerable<ServiceView>>().Result;
            return View(ls);
        }
        public ActionResult Service()
        {
            HttpResponseMessage res = GlobalVariables.client.GetAsync("CategoryService").Result;
            return View(res.Content.ReadAsAsync<IEnumerable<CategoryServiceView>>().Result);
        }
        public ActionResult CreateService(ServiceView sv)
        {
            var res = GlobalVariables.client.PostAsJsonAsync("Service", sv).Result;
            TempData["success"] = "Create new service successfully!";
            return RedirectToAction("ServiceList");
        }
        public ActionResult GetService(int id)
        {
            var res = GlobalVariables.client.GetAsync("Service/" + id.ToString()).Result;
            var item = res.Content.ReadAsAsync<ServiceView>().Result;
            HttpResponseMessage resCbb = GlobalVariables.client.GetAsync("CategoryService?idCateSer=" + item.IDCateSer.ToString()).Result;
            TempData["cbb"] = resCbb.Content.ReadAsAsync<IEnumerable<CategoryServiceView>>().Result;
            return View(item);
        }
        public ActionResult UpdateService(ServiceView sv)
        {
            var res = GlobalVariables.client.PutAsJsonAsync("Service/" + sv.IDService.ToString(), sv).Result;
            TempData["success"] = "Update service successfully!";
            return RedirectToAction("ServiceList");
        }
        public ActionResult DelService(int id)
        {
            var res = GlobalVariables.client.DeleteAsync("Service/" + id.ToString()).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Delete successfully!";
            }
            else
            {
                TempData["error"] = "Delete failed!";
            }
            return RedirectToAction("ServiceList");
        }
        #endregion

        #region Booking
        public ActionResult ReadNewBooking(int id,string token)
        {
            var res = GlobalVariables.client.GetAsync("Booking/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingDetail", new { token = token });
            }
            return RedirectToAction("Index");
        }
        public ActionResult BookingDetail(string token)
        {
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            HttpResponseMessage resHis = GlobalVariables.client.GetAsync("HistoryBooking/" + book.IDBooking).Result;
            TempData["his"] = resHis.Content.ReadAsAsync<IEnumerable<HistoryBookingView>>().Result;
            HttpResponseMessage resRB = GlobalVariables.client.GetAsync("RoomBooking/" + book.IDBooking).Result;
            TempData["room"] = resRB.Content.ReadAsAsync<IEnumerable<RoomBookingView>>().Result;
            HttpResponseMessage resOrd = GlobalVariables.client.GetAsync("OrderService?idBook=" + book.IDBooking).Result;
            TempData["ord"] = resOrd.Content.ReadAsAsync<IEnumerable<OrderDetailView>>().Result;
            HttpResponseMessage resTotal = GlobalVariables.client.GetAsync("OrderService/" + book.IDBooking).Result;
            if (resTotal.IsSuccessStatusCode)
            {
                TempData["total"] = (resTotal.Content.ReadAsAsync<OrderServiceView>().Result).Total;
            }
            return View(book);
        }
        #endregion
    }
}