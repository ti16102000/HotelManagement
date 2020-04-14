using HotelManagement.Models.DAO;
using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Business
{
    public class ServiceBUS
    {
        #region Category Service
        public static bool CreateCateService(CategoryService cs)
        {
            return CategoryServiceDAO.CreateCateService(cs);
        }
        public static IEnumerable<CategoryService> GetAllCateService()
        {
            return CategoryServiceDAO.GetAllCateService();
        }
        public static IEnumerable<CategoryService> GetCateServiceCBB(int id)
        {
            return CategoryServiceDAO.GetCateServiceCBB(id);
        }
        public static CategoryService GetCateServiceByID(int id)
        {
            return CategoryServiceDAO.GetCateServiceByID(id);
        }
        public static bool UpdateCateService(CategoryService cs)
        {
            return CategoryServiceDAO.UpdateCateService(cs);
        }
        public static bool DelCateService(int id)
        {
            return CategoryServiceDAO.DelCateService(id);
        }
        #endregion

        #region Service
        public static bool CreateService(Service s)
        {
            return ServiceDAO.CreateService(s);
        }
        public static IEnumerable<Service> GetAllService()
        {
            return ServiceDAO.GetAllService();
        }
        public static IEnumerable<Service> GetServiceByIDCate(int idCate)
        {
            return ServiceDAO.GetServiceByIDCate(idCate);
        }
        public static Service GetServiceByID(int id)
        {
            return ServiceDAO.GetServiceByID(id);
        }
        public static bool UpdateService(Service s)
        {
            return ServiceDAO.UpdateService(s);
        }
        public static bool DelService(int id)
        {
            return ServiceDAO.DelService(id);
        }
        #endregion
    }
}