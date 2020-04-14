using HotelManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Mvc;

namespace HotelManagement.Controllers.MVC
{
    public class BookingController : Controller
    {
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    object o = filterContext.HttpContext.Session["book"];
        //    //var actionName = filterContext.RouteData.Values["action"];
        //    //var ControllerName = filterContext.RouteData.Values["controller"];
        //    if (o == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Booking" }, { "Action", "Index" } }); //back to public page
        //    }

        //}
        // GET: Booking
        public ActionResult Index()
        {
            if (Session["book"] != null)
            {
                BookingView book = new BookingView();
                book = (BookingView)Session["book"];
                TempData["room"] = book.NumberRoom;
                TempData["stay"] = book.DurationStay;
                TempData["in"] = book.DateIn.Year + "-" + (book.DateIn.Month < 10 ? ("0" + Convert.ToString(book.DateIn.Month)) : Convert.ToString(book.DateIn.Month)) + "-" + (book.DateIn.Day < 10 ? ("0" + Convert.ToString(book.DateIn.Day)) : Convert.ToString(book.DateIn.Day));
                TempData["out"] = book.DateOut.Year + "-" + (book.DateOut.Month < 10 ? ("0" + Convert.ToString(book.DateOut.Month)) : Convert.ToString(book.DateOut.Month)) + "-" + (book.DateOut.Day < 10 ? ("0" + Convert.ToString(book.DateOut.Day)) : Convert.ToString(book.DateOut.Day));

            }
            IEnumerable<CategoryRoomView> ls = null;
            HttpResponseMessage res = GlobalVariables.client.GetAsync("CategoryRoom").Result;
            ls = res.Content.ReadAsAsync<IEnumerable<CategoryRoomView>>().Result;
            return View(ls);
        }
        public ActionResult SaveBooking(BookingView bv)
        {
            if (Session["book"] != null)
            {
                Session.Remove("book");
            }
            Session["book"] = bv as BookingView;
            return RedirectToAction("RegisterCustomer");
        }
        public ActionResult RegisterCustomer()
        {
            return View();
        }
        public ActionResult InformationBooking(string token)
        {
            if (token == "131696")
            {
                Session["emp"] = "emp";
                return RedirectToAction("Index");
            }
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            if (resBooking.IsSuccessStatusCode)
            {
                BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
                HttpResponseMessage resHis = GlobalVariables.client.GetAsync("HistoryBooking/" + book.IDBooking).Result;
                TempData["his"] = resHis.Content.ReadAsAsync<IEnumerable<HistoryBookingView>>().Result;
                HttpResponseMessage resHisCancel = GlobalVariables.client.GetAsync("CheckHistory?idBook="+book.IDBooking+"&value=CCBS").Result;
                HttpResponseMessage resHisCheckOut = GlobalVariables.client.GetAsync("CheckHistory?idBook=" + book.IDBooking + "&value=PC").Result;
                if (resHisCancel.IsSuccessStatusCode || resHisCheckOut.IsSuccessStatusCode)
                {
                    TempData["cancel"] = 1;
                }
                if (book.NumberRoom == book.CountRoomBook )
                {
                    TempData["checkroom"] = 1;
                }
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
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CreateBooking(CustomerView cv)
        {
            HttpResponseMessage resCus = GlobalVariables.client.PostAsJsonAsync("Customer", cv).Result;
            int idCus = resCus.Content.ReadAsAsync<int>().Result;
            string tokenBooking = LibraryHelper.Tokenizer.Generate(5);
            BookingView book = new BookingView();
            book = (BookingView)Session["book"];
            book.IDCus = idCus;
            book.TokenBooking = tokenBooking;
            HttpResponseMessage resBook = GlobalVariables.client.PostAsJsonAsync("Booking", book).Result;
            if (resBook.IsSuccessStatusCode)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        MailAddress senderEmail = new MailAddress("dtmt1610@gmail.com", "ABCXYZ Hotel");
                        MailAddress receiverEmail = new MailAddress(cv.EmailCus, cv.NameCus);
                        string password = "dtmt16101302";
                        string sub = "Booking Successfully";
                        string body = "Hello, " + cv.NameCus + "!\n" +
                                    "Thank you for your reservation to stay at the ABCXYZ hotel.\n" +
                                    "Your booking code: " + tokenBooking + " .\n" +
                                    "Link leading to your booking information: http://localhost:53561/Booking/InformationBooking?token=" + tokenBooking;
                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 25,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (MailMessage mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                        TempData["success"] = "Booking Successfully! Check your mail";
                        return RedirectToAction("InformationBooking", new { token = tokenBooking });
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Error = "Some Error";
                    Console.WriteLine(e.Message);
                }
            }
            TempData["error"] = "Booking error!";
            return RedirectToAction("RegisterCustomer");
        }
        public ActionResult CancelBooking(int idBooking)
        {
            HistoryBookingView his = new HistoryBookingView { IDBook = idBooking, NameHisBook = "Đặt phòng đã đc hủy(CCBS)", DayCreateHisBook = DateTime.Now };
            HttpResponseMessage res = GlobalVariables.client.PostAsJsonAsync("HistoryBooking", his).Result;
            TempData["success"] = "Cancel booking successfully";
            return RedirectToAction("Index");
        }
        public ActionResult EditBooking(string token)
        {
            HttpResponseMessage res = GlobalVariables.client.GetAsync("CategoryRoom").Result;
            TempData["cate"] = res.Content.ReadAsAsync<IEnumerable<CategoryRoomView>>().Result;
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            TempData["in"] = book.DateIn.Year + "-" + (book.DateIn.Month < 10 ? ("0" + Convert.ToString(book.DateIn.Month)) : Convert.ToString(book.DateIn.Month)) + "-" + (book.DateIn.Day < 10 ? ("0" + Convert.ToString(book.DateIn.Day)) : Convert.ToString(book.DateIn.Day));
            TempData["out"] = book.DateOut.Year + "-" + (book.DateOut.Month < 10 ? ("0" + Convert.ToString(book.DateOut.Month)) : Convert.ToString(book.DateOut.Month)) + "-" + (book.DateOut.Day < 10 ? ("0" + Convert.ToString(book.DateOut.Day)) : Convert.ToString(book.DateOut.Day));
            return View(book);
        }
        public ActionResult UpdateBooking(BookingView bv)
        {
            HttpResponseMessage res = GlobalVariables.client.PutAsJsonAsync("Booking/" + bv.IDBooking.ToString(), bv).Result;
            TempData["success"] = "Update booking successfully!";
            return RedirectToAction("InformationBooking", new { token = bv.TokenBooking });
        }
        public ActionResult EditInfor(string token)
        {
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            return View(book);
        }
        public ActionResult UpdateInfor(BookingView bv)
        {
            HttpResponseMessage res = GlobalVariables.client.PutAsJsonAsync("Customer/" + bv.IDBooking.ToString(), bv).Result;
            TempData["success"] = "Update information successfully!";
            return RedirectToAction("InformationBooking", new { token = bv.TokenBooking });
        }
        public ActionResult TakeRoomForm(string token)
        {
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            if (book.NumberRoom > book.CountRoomBook)
            {
                TempData["diff"] = book.NumberRoom - book.CountRoomBook;
            }
            HttpResponseMessage resRoom = GlobalVariables.client.GetAsync("EmptyRoom/" + book.IDCateRoom.ToString()).Result;
            TempData["room"] = resRoom.Content.ReadAsAsync<IEnumerable<RoomView>>().Result;
            return View(book);
        }
        [HttpPost]
        public JsonResult TakeRoom(RoomBookingView rbv)
        {
            HttpResponseMessage res = GlobalVariables.client.PostAsJsonAsync("RoomBooking", rbv).Result;

            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckIn(int idBooking, string tokenBook, int number)
        {
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + tokenBook).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            OrderDetailView ord = new OrderDetailView { NameService = number + " phòng (" + book.NameCateRoom + ") x " + book.DurationStay + " day(s)", PriceOrdD = book.PriceCateRoom, Quantity = number, Amount = book.PriceCateRoom * number * book.DurationStay };
            System.Threading.Tasks.Task<HttpResponseMessage> resOrd = GlobalVariables.client.PostAsJsonAsync("OrderService?idBook=" + book.IDBooking, ord);
            if (book.NumberRoom == book.CountRoomBook + number || book.NumberRoom == number)
            {
                HistoryBookingView full = new HistoryBookingView { IDBook = idBooking, NameHisBook = "Check in đủ số lượng phòng đã đặt(CI)", DayCreateHisBook = DateTime.Now };
                HttpResponseMessage resHisfull = GlobalVariables.client.PostAsJsonAsync("HistoryBooking", full).Result;
            }
            else
            {
                HistoryBookingView less = new HistoryBookingView { IDBook = idBooking, NameHisBook = "Check in chưa đủ số lượng phòng đã đặt(CI)", DayCreateHisBook = DateTime.Now };
                HttpResponseMessage resHisless = GlobalVariables.client.PostAsJsonAsync("HistoryBooking", less).Result;
            }
            return RedirectToAction("InformationBooking", new { token = tokenBook });
        }
        public ActionResult ChangeRoom(int idRB, string token)
        {
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            HttpResponseMessage resRoom = GlobalVariables.client.GetAsync("EmptyRoom/" + book.IDCateRoom.ToString()).Result;
            TempData["room"] = resRoom.Content.ReadAsAsync<IEnumerable<RoomView>>().Result;
            TempData["token"] = token;
            TempData["idBook"] = book.IDBooking;
            HttpResponseMessage resRB = GlobalVariables.client.GetAsync("RoomBooking?idRB=" + idRB.ToString()).Result;
            RoomBookingView rb = resRB.Content.ReadAsAsync<RoomBookingView>().Result;
            return View(rb);
        }
        public ActionResult UpdateRB(RoomBookingView rbv, string reason, string token)
        {
            HttpResponseMessage res = GlobalVariables.client.PutAsJsonAsync("RoomBooking?idRB=" + rbv.IDRoomBook + "&reason=" + reason, rbv).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Room change successful";
            }
            return RedirectToAction("InformationBooking", new { token = token });
        }

        public ActionResult CheckOut(string token, bool payment)
        {
            if (payment == true)
            {
                HttpResponseMessage res = GlobalVariables.client.GetAsync("CheckOut?token=" + token + "&payment=" + payment).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["success"] = "Thanh toán bằng tiền mặt thành công";
                }
                else
                {
                    TempData["error"] = "Thanh toán bằng tiền mặt lỗi";
                }
                return RedirectToAction("InformationBooking", new { token = token });
            }
            else
            {
                Session["token"] = token;
                return RedirectToAction("PaymentWithPaypal", "Paypal");
            }
        }
        public ActionResult LogOut()
        {
            Session.Remove("emp");
            return RedirectToAction("Index");
        }
    }
}