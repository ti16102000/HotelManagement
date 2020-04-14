using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class CustomerDAO
    {
        public static int CreateCus(Customer cus)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            cus.DayCreateCus = DateTime.Now;
            hm.Customers.Add(cus);
            hm.SaveChanges();
            return cus.IDCus;
        }
        public static IEnumerable<Customer> GetAllCus()
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Customers.OrderByDescending(o => o.DayCreateCus).ToList();
        }
        public static Customer GetCusByID(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Customers.Find(id);
        }
        public static bool UpdateCus(Customer cus,int idBooking)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.Customers.SingleOrDefault(s => s.IDCus == cus.IDCus);
            item.NameCus = cus.NameCus;
            item.PhoneCus = cus.PhoneCus;
            item.AddressCus = cus.AddressCus;
            item.EmailCus = cus.EmailCus;
            if (hm.SaveChanges() > 0)
            {
                var his = new HistoryBooking { IDBook = idBooking, NameHisBook = "Cập nhật thông tin cá nhân", DayCreateHisBook = DateTime.Now };
                HistoryBookingDAO.CreateHisBook(his);
                return true;
            }
            return false;
        }
    }
}