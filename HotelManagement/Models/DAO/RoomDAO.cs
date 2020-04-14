using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class RoomDAO
    {
        public static bool CreateRoom(Room r)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            r.Empty = true;
            hm.Rooms.Add(r);
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static IEnumerable<Room> GetAllRoom()
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Rooms.ToList();
        }
        public static Room GetRoomByID(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Rooms.Find(id);
        }
        public static IEnumerable<Room> GetRoomEmptyByIDCate(int idCateRoom)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Rooms.Where(w => w.IDCateRoom == idCateRoom && w.Empty == true).ToList();
        }
        public static bool UpdateRoom(Room r)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.Rooms.SingleOrDefault(s => s.IDRoom == r.IDRoom);
            item.NameRoom = r.NameRoom;
            item.IDCateRoom = r.IDCateRoom;
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool UpdateRoomEmpty(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.Rooms.SingleOrDefault(s => s.IDRoom == id);
            if (item.Empty == true)
            {
                item.Empty = false;
            }
            else
            {
                item.Empty = true;
            }
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool DelRoom(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            try
            {
                hm.Rooms.Remove(hm.Rooms.Find(id));
                if (hm.SaveChanges() > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}