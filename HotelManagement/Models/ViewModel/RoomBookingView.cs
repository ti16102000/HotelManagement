using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class RoomBookingView
    {
        public int IDRoomBook { get; set; }
        public int IDBook { get; set; }
        public int IDRoom { get; set; }
        public string NameRoom { get; set; }
    }
}