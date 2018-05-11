using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;

namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_account_t_station
    {
        /// <summary>
        /// 查询车站信息
        /// </summary>
        /// <param name="StationID"></param>
        /// <returns></returns>
        public account_t_station Get_Station(string StationID)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    account_t_station s = db.account_t_station.Where(n => n.StationID == StationID).FirstOrDefault();
                    return s;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public account_t_station Get_Station_ByIP(string IP)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    account_t_station s = db.account_t_station.Where(n => n.ConnectString == IP).FirstOrDefault();
                    return s;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        public List<account_t_station> Get_All_Station()
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    List<account_t_station> list = db.account_t_station.OrderBy(n => n.OrderNum).ToList();
                    return list;
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
