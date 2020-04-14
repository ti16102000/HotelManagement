using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class CategoryRoomView
    {
        public int IDCateRoom { get; set; }
        public string NameCateRoom { get; set; }
        public int PriceCateRoom { get; set; }
        public int CountRoom { get; set; }
    }
}