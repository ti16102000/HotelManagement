using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagement.Models.DAO
{
    public class RoomBookingDAO
    {
        public static bool CreateRB(RoomBooking rb)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            hm.RoomBookings.Add(rb);
            if (hm.SaveChanges() > 0)
            {
                RoomDAO.UpdateRoomEmpty(rb.IDRoom);
                return true;
            }
            return false;
        }
        public static IEnumerable<RoomBooking> GetRB(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.RoomBookings.Where(w => w.IDBook == id).ToList();
        }
        public static bool UpdateRB(RoomBooking rb,string reason)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.RoomBookings.SingleOrDefault(s => s.IDRoomBook == rb.IDRoomBook);
            var idRoomOld = item.IDRoom;
            var roomOld = item.Room.NameRoom;
            var roomNew = hm.Rooms.SingleOrDefault(w => w.IDRoom == rb.IDRoom).NameRoom;
            item.IDRoom = rb.IDRoom;
            if (hm.SaveChanges() > 0)
            {
                var his = new HistoryBooking { IDBook = rb.IDBook, NameHisBook = "Đổi phòng thành công ("+roomOld+" -> "+roomNew+": "+reason+")", DayCreateHisBook = DateTime.Now };
                HistoryBookingDAO.CreateHisBook(his);
                RoomDAO.UpdateRoomEmpty(idRoomOld);
                RoomDAO.UpdateRoomEmpty(rb.IDRoom);
                return true;
            }
            return false;
        }
        public static RoomBooking RoomChange(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.RoomBookings.Find(id);
        }
    }
}