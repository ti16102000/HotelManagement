using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class HistoryBookingView
    {
        public int IDHisBook { get; set; }
        public int IDBook { get; set; }
        public string NameHisBook { get; set; }
        public System.DateTime DayCreateHisBook { get; set; }
    }
}