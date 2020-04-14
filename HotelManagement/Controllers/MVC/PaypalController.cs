using DemoPaypal.Models;
using HotelManagement.Models.ViewModel;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers.MVC
{
    public class PaypalController : Controller
    {
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }
        //work with paypal payment
        private Payment payment;

        //create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var token = Session["token"] as string;
            HttpResponseMessage resBooking = GlobalVariables.client.GetAsync("Booking?token=" + token).Result;
            BookingView book = resBooking.Content.ReadAsAsync<BookingView>().Result;
            HttpResponseMessage resOrd = GlobalVariables.client.GetAsync("OrderService?idBook=" + book.IDBooking).Result;
            var lsOrd = resOrd.Content.ReadAsAsync<IEnumerable<OrderDetailView>>().Result;
            HttpResponseMessage resTotal = GlobalVariables.client.GetAsync("OrderService/" + book.IDBooking).Result;
            var total = (resTotal.Content.ReadAsAsync<OrderServiceView>().Result).Total;

            var lsItem = new ItemList() { items = new List<Item>() };

            foreach (var item in lsOrd)
            {
                lsItem.items.Add(new Item {
                    name=item.NameService,
                    currency="USD",
                    price=item.Amount.ToString(),
                    quantity="1",
                    sku="sku"
                });
            }

            var payer = new Payer()
            {
                payment_method = "paypal",
                payer_info = new PayerInfo
                {
                    email = book.EmailCus
                }

            };
            var redictUrl = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            var detail = new Details() { tax = "1", shipping = "1", subtotal = total.ToString() }; //subtotal : total order, note: sum(price*quantity)
            var amount = new Amount() { currency = "USD", details = detail, total = Convert.ToString(total+2) }; //total= tax + shipping + subtotal
            var transList = new List<Transaction>();
            transList.Add(new Transaction
            {
                description = "Hotel Management using Paypal",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = lsItem,

            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transList,
                redirect_urls = redictUrl
            };
            return this.payment.Create(apiContext);
        }
        //create execute payment method
        private Payment ExecutePayment(APIContext apiContext, string payerID, string paymentID)
        {
            var paymentExecute = new PaymentExecution() { payer_id = payerID };
            this.payment = new Payment() { id = paymentID };
            return this.payment.Execute(apiContext, paymentExecute);
        }
        //create method
        public ActionResult PaymentWithPaypal()
        {
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerID = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerID))
                {
                    //create a payment
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPaypal?guid=";
                    string guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseUri + guid);

                    var link = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (link.MoveNext())
                    {
                        Links links = link.Current;
                        if (links.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = links.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerID, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        TempData["error"] = "Thanh toán bằng Paypal lỗi";
                        return RedirectToAction("InformationBooking", "Booking", new { token = Session["token"].ToString() });
                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                Console.WriteLine(ex);
                TempData["error"] = "Thanh toán bằng Paypal lỗi";
                return RedirectToAction("InformationBooking", "Booking", new { token = Session["token"].ToString() });
            }
            HttpResponseMessage res = GlobalVariables.client.GetAsync("CheckOut?token=" + Session["token"].ToString() + "&payment=" + false).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Thanh toán bằng Paypal thành công";
            }
            else
            {
                TempData["error"] = "Loi check out!";
            }
            return RedirectToAction("InformationBooking", "Booking", new { token = Session["token"].ToString() });
        }

    }
}