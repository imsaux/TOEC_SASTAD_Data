using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;
namespace TOEC_SASTAD_Data.BLL
{
    /// <summary>
    /// 图像统计表
    /// 每条记录表示该趟车已经入库完毕
    /// </summary>
    public class BLL_Statistics_Image
    {
        public bool TrainPassed(DateTime TrainComeDate, int LineID)
        {
            using (sartas3 db = new sartas3())
            {
                statistics_image old = db.statistics_image.FirstOrDefault(n => n.LineID == LineID && n.TrainComeDate == TrainComeDate);
                return old != null ? true : false;
            }
        }
    }
}
