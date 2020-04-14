using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class CustomerView
    {
        public int IDCus { get; set; }
        public string NameCus { get; set; }
        public string PhoneCus { get; set; }
        public string EmailCus { get; set; }
        public string AddressCus { get; set; }
        public System.DateTime DayCreateCus { get; set; }
    }
}