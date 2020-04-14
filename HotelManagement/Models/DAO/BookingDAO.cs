using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class BookingDAO
    {
        public static bool CreateBooking(Booking b)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            b.NewBooking = true;
            b.DayCreateBooking = DateTime.Now;
            hm.Bookings.Add(b);
            if (hm.SaveChanges() > 0)
            {
                var his = new HistoryBooking { IDBook = b.IDBooking, NameHisBook = "Đặt phòng thành công(CBS)", DayCreateHisBook = b.DayCreateBooking };
                HistoryBookingDAO.CreateHisBook(his);
                return true;
            }
            return false;
        }
        public static IEnumerable<Booking> GetAllBooking(bool newBooking)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Bookings.Where(w => w.NewBooking == newBooking).OrderByDescending(o => o.DayCreateBooking).ToList();
        }
        public static Booking GetBookingByToken(string token)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Bookings.SingleOrDefault(s=>s.TokenBooking==token);
        }
        public static bool UpdateBooking(Booking b)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.Bookings.SingleOrDefault(s => s.IDBooking == b.IDBooking);
            item.NumberRoom = b.NumberRoom;
            item.DateIn = b.DateIn;
            item.DateOut = b.DateOut;
            item.DurationStay = b.DurationStay;
            item.IDCateRoom = b.IDCateRoom;
            if (hm.SaveChanges() > 0)
            {
                var his = new HistoryBooking { IDBook = b.IDBooking, NameHisBook = "Cập nhật thông tin phòng", DayCreateHisBook = DateTime.Now};
                HistoryBookingDAO.CreateHisBook(his);
                return true;
            }
            return false;
        }
        public static bool ReadBookingNew(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.Bookings.SingleOrDefault(s => s.IDBooking==id);
            item.NewBooking = false;
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool CheckRoomBook(int idBook)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var book = hm.Bookings.SingleOrDefault(w => w.IDBooking == idBook);
            var numberBook = book.NumberRoom;
            var numberRoomBook = RoomBookingDAO.GetRB(idBook).Count();
            if (numberBook == numberRoomBook)
            {
                return true;
            }
            return false;
        }
        
    }
}