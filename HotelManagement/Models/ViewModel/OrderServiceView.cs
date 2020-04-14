using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class OrderServiceView
    {
        public int IDOrd { get; set; }
        public int IDBooking { get; set; }
        public decimal Total { get; set; }
        public Nullable<bool> Payment { get; set; }
    }
}