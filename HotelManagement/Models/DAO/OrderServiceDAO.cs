using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class OrderServiceDAO
    {
        public static int CreateOrd(OrderService os)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            os.Total = 0;
            hm.OrderServices.Add(os);
            hm.SaveChanges();
            return os.IDOrd;
        }
        public static OrderService GetOrdService(int idBook)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.OrderServices.SingleOrDefault(s => s.IDBooking == idBook);
        }
        public static bool UpdateTotal(int idBook)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item=hm.OrderServices.SingleOrDefault(s => s.IDBooking == idBook);
            item.Total = hm.OrderDetails.Where(w => w.OrderService.IDBooking == idBook).Sum(s => s.Amount);
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool UpdatePayment(int idBooking,bool payment)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.OrderServices.SingleOrDefault(s => s.IDBooking ==idBooking);
            item.Payment = payment;
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

    }
}