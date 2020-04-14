using HotelManagement.Models.DAO;
using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Business
{
    public class RoomBUS
    {
        #region Category Room
        public static bool CreateCateRoom(CategoryRoom cr)
        {
            return CategoryRoomDAO.CreateCateRoom(cr);
        }
        public static IEnumerable<CategoryRoom> GetAllCateRoom()
        {
            return CategoryRoomDAO.GetAllCateRoom();
        }
        public static IEnumerable<CategoryRoom> GetCateRoomCBB(int id)
        {
            return CategoryRoomDAO.GetCateRoomCBB(id);
        }
        public static CategoryRoom GetCateRoomByID(int id)
        {
            return CategoryRoomDAO.GetCateRoomByID(id);
        }
        public static bool UpdateCateRoom(CategoryRoom cr)
        {
            return CategoryRoomDAO.UpdateCateRoom(cr);
        }
        public static bool DelCateRoom(int id)
        {
            return CategoryRoomDAO.DelCateRoom(id);
        }
        #endregion

        #region Room
        public static bool CreateRoom(Room r)
        {
            return RoomDAO.CreateRoom(r);
        }
        public static IEnumerable<Room> GetAllRoom()
        {
            return RoomDAO.GetAllRoom();
        }
        public static IEnumerable<Room> GetRoomEmptyByIDCate(int idCateRoom)
        {
            return RoomDAO.GetRoomEmptyByIDCate(idCateRoom);
        }
        public static Room GetRoomByID(int id)
        {
            return RoomDAO.GetRoomByID(id);
        }
        public static bool UpdateRoom(Room r)
        {
            return RoomDAO.UpdateRoom(r);
        }
        public static bool UpdateRoomEmpty(int id)
        {
            return RoomDAO.UpdateRoomEmpty(id);
        }
        public static bool DelRoom(int id)
        {
            return RoomDAO.DelRoom(id);
        }
        #endregion
    }
}