using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class BookingView
    {
        public int IDBooking { get; set; }
        public int IDCus { get; set; }
        public int IDCateRoom { get; set; }
        public int NumberRoom { get; set; }
        public System.DateTime DateIn { get; set; }
        public System.DateTime DateOut { get; set; }
        public int DurationStay { get; set; }
        public System.DateTime DayCreateBooking { get; set; }
        public bool NewBooking { get; set; }
        public string TokenBooking { get; set; }
        public int CountRoomBook { get; set; }

        //Customer
        public string NameCus { get; set; }
        public string PhoneCus { get; set; }
        public string EmailCus { get; set; }
        public string AddressCus { get; set; }
        public System.DateTime DayCreateCus { get; set; }

        //Category Room
        public string NameCateRoom { get; set; }
        public int PriceCateRoom { get; set; }
    }
}