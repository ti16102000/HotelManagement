using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class HistoryBookingDAO
    {
        public static bool CreateHisBook(HistoryBooking hb)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            hm.HistoryBookings.Add(hb);
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool CheckHisBook(int idBook,string value)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var rs= hm.HistoryBookings.SingleOrDefault(a => a.IDBook==idBook && a.NameHisBook.Contains(value));
            if (rs == null)
            {
                return true;
            }
            return false;
        }
        public static IEnumerable<HistoryBooking> GetHisBookByID(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.HistoryBookings.Where(w => w.IDBook == id).ToList();
        }
    }
    
}