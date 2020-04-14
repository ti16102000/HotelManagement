using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModel
{
    public class ServiceView
    {
        public int IDService { get; set; }
        public string NameService { get; set; }
        public int PriceService { get; set; }
        public int IDCateSer { get; set; }
        public string NameCateSer { get; set; }
    }
}