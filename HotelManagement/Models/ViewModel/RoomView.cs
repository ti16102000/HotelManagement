using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class RoomView
    {
        public int IDRoom { get; set; }
        public string NameRoom { get; set; }
        public int IDCateRoom { get; set; }
        public string NameCateRoom { get; set; }
        public bool Empty { get; set; }
    }
}