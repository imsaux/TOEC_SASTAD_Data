using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;

namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_hotwheel_axle
    {

        public List<hotwheel_axle> Get_HotWheel_Axle(string CarID)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    return db.hotwheel_axle.Where(n => n.TrainDetail_ID.ToString() == CarID).OrderBy(n => n.Axle_OrderNum).ToList();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
