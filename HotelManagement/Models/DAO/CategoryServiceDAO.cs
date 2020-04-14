using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class CategoryServiceDAO
    {
        public static bool CreateCateService(CategoryService cs)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            hm.CategoryServices.Add(cs);
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static IEnumerable<CategoryService> GetAllCateService()
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.CategoryServices.ToList();
        }
        public static CategoryService GetCateServiceByID(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.CategoryServices.Find(id);
        }
        public static bool UpdateCateService(CategoryService cs)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.CategoryServices.SingleOrDefault(s=>s.IDCateSer==cs.IDCateSer);
            item.NameCateSer = cs.NameCateSer;
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool DelCateService(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            try
            {
                hm.CategoryServices.Remove(hm.CategoryServices.Find(id));
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
        public static IEnumerable<CategoryService> GetCateServiceCBB(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.CategoryServices.Where(w => w.IDCateSer != id).ToList();
        }
    }
}