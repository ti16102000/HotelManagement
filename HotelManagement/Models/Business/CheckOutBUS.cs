using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Business
{
    public class CheckOutBUS
    {
        public static bool ExecuteCheckOut(string token,bool payment)
        {
            var book = BookingBUS.GetBookingByToken(token);
            var lsRoomBook = BookingBUS.GetRB(book.IDBooking);
            foreach (var item in lsRoomBook)
            {
                RoomBUS.UpdateRoomEmpty(item.IDRoom);
            }
            if (OrderBUS.UpdatePayment(book.IDBooking, payment) == true)
            {
                var paytype = payment == true ? "(Cash)" : "(Paypal)";
                var hisBook = new HistoryBooking { IDBook = book.IDBooking, NameHisBook = "Thanh toán "+paytype+" và trả phòng thành công(PC)", DayCreateHisBook = DateTime.Now };
                BookingBUS.CreateHisBook(hisBook);
                return true;
            }
            return false;
        }
    }
}