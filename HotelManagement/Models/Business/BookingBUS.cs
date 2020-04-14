using HotelManagement.Models.DAO;
using HotelManagement.Models.EntityModel;
using System.Collections.Generic;

namespace HotelManagement.Models.Business
{
    public class BookingBUS
    {
        #region Booking
        public static bool CreateBooking(Booking b)
        {
            return BookingDAO.CreateBooking(b);
        }
        public static IEnumerable<Booking> GetAllBooking(bool newBooking)
        {
            return BookingDAO.GetAllBooking(newBooking);
        }
        public static Booking GetBookingByToken(string token)
        {
            return BookingDAO.GetBookingByToken(token);
        }
        public static bool UpdateBooking(Booking b)
        {
            return BookingDAO.UpdateBooking(b);
        }
        public static bool ReadBookingNew(int id)
        {
            return BookingDAO.ReadBookingNew(id);
        }

        #endregion

        #region Customer
        public static int CreateCus(Customer cus)
        {
            return CustomerDAO.CreateCus(cus);
        }
        public static IEnumerable<Customer> GetAllCus()
        {
            return CustomerDAO.GetAllCus();
        }
        public static Customer GetCusByID(int id)
        {
            return CustomerDAO.GetCusByID(id);
        }
        public static bool UpdateCus(Customer cus, int idBooking)
        {
            return CustomerDAO.UpdateCus(cus, idBooking);
        }
        #endregion

        #region History Booking
        public static bool CreateHisBook(HistoryBooking hb)
        {
            return HistoryBookingDAO.CreateHisBook(hb);
        }
        public static bool CheckHisBook(int idBook,string value)
        {
            return HistoryBookingDAO.CheckHisBook(idBook,value);
        }
        public static IEnumerable<HistoryBooking> GetHisBookByID(int id)
        {
            return HistoryBookingDAO.GetHisBookByID(id);
        }
        #endregion

        #region RoomBooking
        public static bool CreateRB(RoomBooking rb)
        {
            return RoomBookingDAO.CreateRB(rb);
        }
        public static IEnumerable<RoomBooking> GetRB(int id)
        {
            return RoomBookingDAO.GetRB(id);
        }
        public static bool UpdateRB(RoomBooking rb,string reason)
        {
            return RoomBookingDAO.UpdateRB(rb,reason);
        }
        public static bool CheckRoomBook(int id)
        {
            return BookingDAO.CheckRoomBook(id);
        }
        public static RoomBooking RoomChange(int id)
        {
            return RoomBookingDAO.RoomChange(id);
        }
        #endregion
    }
}