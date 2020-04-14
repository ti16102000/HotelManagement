using HotelManagement.Models.Business;
using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class Repositories
    {
        #region Category Room
        public static bool CreateCateRoom(CategoryRoom cr)
        {
            return RoomBUS.CreateCateRoom(cr);
        }
        public static IEnumerable<CategoryRoom> GetAllCateRoom()
        {
            return RoomBUS.GetAllCateRoom();
        }
        public static IEnumerable<CategoryRoom> GetCateRoomCBB(int id)
        {
            return RoomBUS.GetCateRoomCBB(id);
        }
        public static CategoryRoom GetCateRoomByID(int id)
        {
            return RoomBUS.GetCateRoomByID(id);
        }
        public static bool UpdateCateRoom(CategoryRoom cr)
        {
            return RoomBUS.UpdateCateRoom(cr);
        }
        public static bool DelCateRoom(int id)
        {
            return RoomBUS.DelCateRoom(id);
        }
        #endregion

        #region Room
        public static bool CreateRoom(Room r)
        {
            return RoomBUS.CreateRoom(r);
        }
        public static IEnumerable<Room> GetAllRoom()
        {
            return RoomBUS.GetAllRoom();
        }
        public static IEnumerable<Room> GetRoomEmptyByIDCate(int idCateRoom)
        {
            return RoomBUS.GetRoomEmptyByIDCate(idCateRoom);
        }
        public static Room GetRoomByID(int id)
        {
            return RoomBUS.GetRoomByID(id);
        }
        public static bool UpdateRoom(Room r)
        {
            return RoomBUS.UpdateRoom(r);
        }
        public static bool UpdateRoomEmpty(int id)
        {
            return RoomBUS.UpdateRoomEmpty(id);
        }
        public static bool DelRoom(int id)
        {
            return RoomBUS.DelRoom(id);
        }
        #endregion

        #region Category Service
        public static bool CreateCateService(CategoryService cs)
        {
            return ServiceBUS.CreateCateService(cs);
        }
        public static IEnumerable<CategoryService> GetAllCateService()
        {
            return ServiceBUS.GetAllCateService();
        }
        public static IEnumerable<CategoryService> GetCateServiceCBB(int id)
        {
            return ServiceBUS.GetCateServiceCBB(id);
        }
        public static CategoryService GetCateSeerviceByID(int id)
        {
            return ServiceBUS.GetCateServiceByID(id);
        }
        public static bool UpdateCateService(CategoryService cs)
        {
            return ServiceBUS.UpdateCateService(cs);
        }
        public static bool DelCateService(int id)
        {
            return ServiceBUS.DelCateService(id);
        }
        #endregion

        #region Service
        public static bool CreateService(Service s)
        {
            return ServiceBUS.CreateService(s);
        }
        public static IEnumerable<Service> GetAllService()
        {
            return ServiceBUS.GetAllService();
        }
        public static IEnumerable<Service> GetServiceByIDCate(int idCate)
        {
            return ServiceBUS.GetServiceByIDCate(idCate);
        }
        public static Service GetServiceByID(int id)
        {
            return ServiceBUS.GetServiceByID(id);
        }
        public static bool UpdateService(Service s)
        {
            return ServiceBUS.UpdateService(s);
        }
        public static bool DelService(int id)
        {
            return ServiceBUS.DelService(id);
        }
        #endregion

        #region Booking
        public static bool CreateBooking(Booking b)
        {
            return BookingBUS.CreateBooking(b);
        }
        public static IEnumerable<Booking> GetAllBooking(bool newBooking)
        {
            return BookingBUS.GetAllBooking(newBooking);
        }
        public static Booking GetBookingByToken(string token)
        {
            return BookingBUS.GetBookingByToken(token);
        }
        public static bool UpdateBooking(Booking b)
        {
            return BookingBUS.UpdateBooking(b);
        }
        public static bool ReadBookingNew(int id)
        {
            return BookingBUS.ReadBookingNew(id);
        }

        #endregion

        #region Customer
        public static int CreateCus(Customer cus)
        {
            return BookingBUS.CreateCus(cus);
        }
        public static IEnumerable<Customer> GetAllCus()
        {
            return BookingBUS.GetAllCus();
        }
        public static Customer GetCusByID(int id)
        {
            return BookingBUS.GetCusByID(id);
        }
        public static bool UpdateCus(Customer cus, int idBooking)
        {
            return BookingBUS.UpdateCus(cus, idBooking);
        }
        #endregion

        #region History Booking
        public static bool CreateHisBook(HistoryBooking hb)
        {
            return BookingBUS.CreateHisBook(hb);
        }
        public static bool CheckHisBook(int idBook,string value)
        {
            return BookingBUS.CheckHisBook(idBook,value);
        }
        public static IEnumerable<HistoryBooking> GetHisBookByID(int id)
        {
            return BookingBUS.GetHisBookByID(id);
        }
        #endregion

        #region RoomBooking
        public static bool CreateRB(RoomBooking rb)
        {
            return BookingBUS.CreateRB(rb);
        }
        public static IEnumerable<RoomBooking> GetRB(int id)
        {
            return BookingBUS.GetRB(id);
        }
        public static bool UpdateRB(RoomBooking rb,string reason)
        {
            return BookingBUS.UpdateRB(rb,reason);
        }
        public static bool CheckRoomBook(int id)
        {
            return BookingBUS.CheckRoomBook(id);
        }
        public static RoomBooking RoomChange(int id)
        {
            return BookingBUS.RoomChange(id);
        }
        #endregion

        #region Order Service
        public static int CreateOrd(OrderService os)
        {
            return OrderBUS.CreateOrd(os);
        }
        public static OrderService GetOrdServiceByID(int idBook)
        {
            return OrderBUS.GetOrdServiceByID(idBook);
        }
        public static bool UpdateTotal(int idBook)
        {
            return OrderBUS.UpdateTotal(idBook);
        }
        public static bool UpdatePayment(int idBook, bool payment)
        {
            return OrderBUS.UpdatePayment(idBook, payment);
        }
        #endregion

        #region Order Detail
        public static void CreateOrdDetail(OrderDetail od, int idBook)
        {
            OrderBUS.CreateOrdDetail(od, idBook);
        }
        public static IEnumerable<OrderDetail> GetAllOrdDetail(int idBook)
        {
            return OrderBUS.GetAllOrdDetail(idBook);
        }
        #endregion

        #region
        public static bool CheckOut(string token, bool payment)
        {
            return CheckOutBUS.ExecuteCheckOut(token, payment);
        }
        #endregion
    }
}