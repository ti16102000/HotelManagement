using HotelManagement.Models.DAO;
using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Business
{
    public class OrderBUS
    {
        #region Order Service
        public static int CreateOrd(OrderService os)
        {
            return OrderServiceDAO.CreateOrd(os);
        }
        public static OrderService GetOrdServiceByID(int idBook)
        {
            return OrderServiceDAO.GetOrdService(idBook);
        }
        public static bool UpdateTotal(int idBook)
        {
            return OrderServiceDAO.UpdateTotal(idBook);
        }
        public static bool UpdatePayment(int idBook,bool payment)
        {
            return OrderServiceDAO.UpdatePayment(idBook, payment);
        }
        #endregion

        #region Order Detail
        public static void CreateOrdDetail(OrderDetail od,int idBook)
        {
            OrderDetailDAO.CreateOrdDetail(od, idBook);
        }
        public static IEnumerable<OrderDetail> GetAllOrdDetail(int idBook)
        {
            return OrderDetailDAO.GetAllOrdDetail(idBook);
        }
        #endregion
    }
}