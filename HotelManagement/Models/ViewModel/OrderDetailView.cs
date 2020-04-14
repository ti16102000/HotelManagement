using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class OrderDetailView
    {
        public int IDOrdD { get; set; }
        public int IDOrd { get; set; }
        public string NameService { get; set; }
        public int Quantity { get; set; }
        public int PriceOrdD { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime DayCreateOrdD { get; set; }
    }
}