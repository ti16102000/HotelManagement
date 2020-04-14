using HotelManagement.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.DAO
{
    public class ServiceDAO
    {
        public static bool CreateService(Service s)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            hm.Services.Add(s);
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static IEnumerable<Service> GetAllService()
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Services.ToList();
        }
        public static IEnumerable<Service> GetServiceByIDCate(int idCate)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Services.Where(w => w.IDCateSer == idCate).ToList();
        }
        public static Service GetServiceByID(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            return hm.Services.Find(id);
        }
        public static bool UpdateService(Service s)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            var item = hm.Services.SingleOrDefault(s1 => s1.IDService == s.IDService);
            item.NameService = s.NameService;
            item.PriceService = s.PriceService;
            item.IDCateSer = s.IDCateSer;
            if (hm.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool DelService(int id)
        {
            HotelAPIManagementEntities hm = new HotelAPIManagementEntities();
            try
            {
                hm.Services.Remove(hm.Services.Find(id));
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