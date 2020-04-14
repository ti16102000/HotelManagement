using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class OrderDetailDAO
    {
        public static void CreateOrdDetail(OrderDetail od,int idBook)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var rs = hm.OrderServices.Any(a => a.IDBooking == idBook);
            od.DayCreateOrdD = DateTime.Now;
            if (rs==false)
            {
                int idOrd = OrderServiceDAO.CreateOrd(new OrderService { IDBooking = idBook });
                od.IDOrd = idOrd;
            }
            else
            {
                var ord = OrderServiceDAO.GetOrdService(idBook);
                od.IDOrd = ord.IDOrd;
            }
            hm.OrderDetails.Add(od);
            hm.SaveChanges();
            OrderServiceDAO.UpdateTotal(idBook);
        }
        public static IEnumerable<OrderDetail> GetAllOrdDetail(int idBook)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.OrderDetails.Where(w => w.OrderService.IDBooking == idBook).ToList();
        }
    }
}