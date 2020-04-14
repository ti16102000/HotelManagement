using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class CategoryRoomDAO
    {
        public static bool CreateCateRoom(CategoryRoom cr)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            hm.CategoryRooms.Add(cr);
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static IEnumerable<CategoryRoom> GetAllCateRoom()
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.CategoryRooms.ToList();
        }
        public static IEnumerable<CategoryRoom> GetCateRoomCBB(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.CategoryRooms.Where(w => w.IDCateRoom != id).ToList();
        }
        public static CategoryRoom GetCateRoomByID(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.CategoryRooms.Find(id);
        }
        public static bool UpdateCateRoom(CategoryRoom cr)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.CategoryRooms.SingleOrDefault(s => s.IDCateRoom == cr.IDCateRoom);
            item.NameCateRoom = cr.NameCateRoom;
            item.PriceCateRoom = cr.PriceCateRoom;
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool DelCateRoom(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            try
            {
                hm.CategoryRooms.Remove(hm.CategoryRooms.Find(id));
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